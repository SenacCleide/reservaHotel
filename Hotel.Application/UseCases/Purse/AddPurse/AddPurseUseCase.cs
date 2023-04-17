using Hotel.Application.Dto;
using Hotel.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.Application.Helper.Helper;

namespace Hotel.Application.UseCases.Purse.AddPurse
{
    public sealed class AddPurseUseCase : IAddPurseUseCase
    {
        private readonly IPurseRepository _purseRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;
        public AddPurseUseCase(IPurseRepository purseRepository,
            IUserRepository userRepository,
            ILogRepository logRepository) 
        {
            _purseRepository = purseRepository;
            _userRepository = userRepository;
            _logRepository = logRepository;
        }

        public async Task<ResponseDto> Execute(PurseRequestDto purse, string idUser, bool addPurse)
        {
            var result = new ResponseDto();
           int responsePurse = 0;
            try
            {
                if(purse.Value != 0)
                {
                    var user = await _purseRepository.Get(idUser);

                    if(user != null)
                    {
                        if(!!addPurse)
                        {
                            var dataPurse = DataPurse(purse, user, addPurse);

                            responsePurse = await _purseRepository.Update(dataPurse);
                        }
                        else if(!addPurse && user.Value >= purse.Value)
                        {
                            var dataPurse = DataPurse(purse, user, addPurse);

                            responsePurse = await _purseRepository.Update(dataPurse);
                        }
                        else
                        {
                            return result = new ResponseDto
                            {
                                Message = "Saldo insuficiente, por favor adicione créditos!",
                                Success = false
                            };
                        }
                        
                    }
                    else
                    {
                        var purseRequest = new PurseDto();
                        purseRequest.IdUsers = idUser;
                        purseRequest.Value = purse.Value;
                        responsePurse = await _purseRepository.Add(purseRequest);
                    }
                }
                return result = new ResponseDto
                {
                    Message = responsePurse != 0 ? "Sucesso" : "Tente novamente!",
                    Success = responsePurse != 0 ? true : false
                };
            }
            catch(Exception ex)
            {
                var logs = Log(ex.Message + "Adicionar Saldo em conta Iduser: " + idUser, "AddPurseUserCase");
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
