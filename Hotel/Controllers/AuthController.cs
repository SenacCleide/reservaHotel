using Hotel.Application.Dto;
using Hotel.Application.UseCases.AuthUse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hotel.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUseUseCase _authUseUseCase;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthUseUseCase authUseUseCase, IConfiguration configuration)
        {
            _authUseUseCase = authUseUseCase;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<object> Login(AuthUserDto user)
        {
            ResponseDto<UserDto> result = await _authUseUseCase.Execute(user);

            if (result.Success)
            {
                var tokenValidation = _configuration.GetSection("modules:0:TokenValidationDto").Get<TokenValidationDto>();
                return AuthToken.BuildToken(user.Email, result.Data.Id, tokenValidation);
            }
            else
            {
                return new UserToken.LoginFailureDto
                {
                    Status = false,
                    Message = result.Message
                };
            }
        }
    }
}