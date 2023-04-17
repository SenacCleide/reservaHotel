using Hotel.Application.Dto;
using Hotel.Application.Repositories;
using static Hotel.Application.Helper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.AuthUse
{
    public sealed class AuthUseUseCase : IAuthUseUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;
        public AuthUseUseCase(IUserRepository userRepository,
            ILogRepository logRepository)
        {
            _userRepository = userRepository;
            _logRepository = logRepository;
        }

        public async Task<ResponseDto<UserDto>> Execute(AuthUserDto authUser)
        {
            var result = new ResponseDto<UserDto>();
            try
            {
                UserDto user = await _userRepository.GetUserEmail(authUser);

                return result = new ResponseDto<UserDto>
                {
                    Message = user != null ? "sucesso" : "Usuário ou senhas inválidos",
                    Success = user != null ? true: false,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                var logs = Log(ex.Message + "Email para consulta: " + authUser.Email, "AuthUseUseCase");
                await _logRepository.Add(logs);

                return result = new ResponseDto<UserDto>
                {
                    Message = "Problemas ao para validar token.",
                    Success = false
                };
            }
        }
    }
}
