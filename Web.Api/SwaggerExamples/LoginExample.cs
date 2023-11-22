using Swashbuckle.AspNetCore.Filters;
using Web.Model.Dtos.User.Request;
using Web.Model.EnumerationTypes;

namespace Web.Api.SwaggerExamples
{
    public class LoginExample : IExamplesProvider<SignInRequestDto>
    {
        public SignInRequestDto GetExamples()
        {
            return new SignInRequestDto() { Username="Teacher1", Password="Password@1"};
        }
    }
}
