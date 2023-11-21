using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Model.Dtos.User.Response;

namespace Web.Application.Helpers.GenerateJWT
{
    public class TokenHandler
    {
        public static string CreateToken(UserResponseDto userResponseDto, int expireDate, IConfiguration configuration)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userResponseDto.Id.ToString()),
                new Claim(ClaimTypes.Name, userResponseDto.Fullname),
                new Claim(ClaimTypes.Role, userResponseDto.UserProfile.Type.ToString()),
                new Claim(ClaimTypes.DateOfBirth, userResponseDto.DateOfBirth.ToString()),
                new Claim(ClaimTypes.Gender, userResponseDto.Gender),
                new Claim(ClaimTypes.MobilePhone, userResponseDto.Phone),
                new Claim(ClaimTypes.StreetAddress, userResponseDto.Address),
            };

            var token = new JwtSecurityToken
            (
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expireDate),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
