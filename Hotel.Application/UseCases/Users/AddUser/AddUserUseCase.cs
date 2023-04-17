using Hotel.Application.Dto;
using Hotel.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.Application.Helper.Helper;
using System.Xml.Linq;

namespace Hotel.Application.UseCases.Users.AddUser
{
    public sealed class AddUserUseCase : IAddUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;
        public AddUserUseCase(IUserRepository userRepository, ILogRepository logRepository)
        {
            _userRepository = userRepository;
            _logRepository = logRepository;
        }
        public async Task<ResponseDto> Execute(UserDto user)
        {
            var result = new ResponseDto();
            try
            {
                var isEmailName =  validarEmailServices.IsValidEmail(user.Email) && NameServices.IsValid(user.Name);

                if(!isEmailName)
                {
                    return result = new ResponseDto
                    {
                        Message = "Nome ou email, são inválidos!",
                        Success = false,
                    };
                }

                var responseUser = await _userRepository.AddUser(user);

                return result = new ResponseDto
                {
                    Message = string.IsNullOrEmpty(responseUser.Id) ? "Sucesso" : "Por favor tente novamente!",
                    Success = string.IsNullOrEmpty(responseUser.Id) ? true : false,
                };
            }
            catch (Exception ex)
            {
                var logs = Log(ex.Message + "Email do Usuário: " + user.Email, "AddUserCase");
                await _logRepository.Add(logs);
                return result = new ResponseDto
                {
                    Message = ex.Message,
                    Success = false,
                };
            }
        }
    }
}
