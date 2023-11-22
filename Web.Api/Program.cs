using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Web.Api.Helpers;
using Web.Api.Middlewares;
using Web.Api.SwaggerExamples;
using Web.Application;
using Web.Domain.Context;
using Web.Infracturre;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StartupBase>());
builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API document", Version = "v1" });
    config.EnableAnnotations();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    config.OperationFilter<AuthResponsesOperationFilter>();
    config.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateUserExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<UpdateUserExample>();

builder.Services.AddTransient<TokenValidationHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<JwtBearerOptions, TokenValidationHandler>(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

#region Add Infrastructure layer
builder.Services.AddDatabase(builder.Configuration)
                .AddInfrastuctures();
#endregion

#region Add Application layer
builder.Services.AddApplicationServices()
                .AddValidatorsFromAppServices()
                .AddAutoMapperFromAppServices();
#endregion

builder.Services.AddHostedService<CredentialCleanupService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseStaticFiles(new StaticFileOptions { FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")), RequestPath = "/assets" });

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Migrate();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

