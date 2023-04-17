using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Dto
{
    public sealed class TokenValidationDto
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Claims { get; set; }
        public int Expires { get; set; }
        public string SigningCredentials { get; set; }
        public TokenValidationDto() { }

        public TokenValidationDto(TokenValidationDto tokenValidation)
        {
            Issuer = tokenValidation.Issuer;
            Audience = tokenValidation.Audience;
            Claims = tokenValidation.Claims;
            Expires = tokenValidation.Expires;
            SigningCredentials = tokenValidation.SigningCredentials;
        }
    }
}
