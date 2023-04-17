using Hotel.Application.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.WebApi
{
    public class AuthToken
    {
        public static UserToken BuildToken(string userInfo, string userId, TokenValidationDto tokenValidation)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(tokenValidation.SigningCredentials);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.ToString())
                }),
                Expires = DateTime.Now.AddHours(tokenValidation.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.Now,
                Audience = tokenValidation.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserToken()
            {
                Status = true,
                Created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = DateTime.Now.AddHours(tokenValidation.Expires).ToString("yyyy-MM-dd HH:mm:ss"),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "OK",
                Tenant = userId,
            };
        }
    }
}
