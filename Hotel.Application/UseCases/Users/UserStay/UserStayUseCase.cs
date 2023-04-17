using Hotel.Application.Dto;
using Hotel.Application.Repositories;
using Hotel.Application.UseCases.Purse.AddPurse;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Hotel.Application.Helper.Helper;
using Result = Hotel.Application.Dto.ResponseDto;
using static System.Collections.Specialized.BitVector32;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Application.UseCases.Users.UserStay
{
    public class UserStayUseCase : IUserStayUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddPurseUseCase _addPurseUseCase;
        private readonly IStayHotelRepository _stayHotelRepository;
        private readonly ILogRepository _logRepository;
        public UserStayUseCase(IUserRepository userRepository,
            IAddPurseUseCase addPurseUseCase,
            IStayHotelRepository stayHotelRepository,
            ILogRepository logRepository)
        {
            _userRepository = userRepository;
            _addPurseUseCase = addPurseUseCase;
            _stayHotelRepository = stayHotelRepository;
            _logRepository = logRepository;
        }
        public async Task<ActionResult> Execute(StayHotelUserDto stayUser)
        {
            var result = new ResponseDto();
            ActionResult download = null;
            try
            {
                var dataPurse = new PurseRequestDto();
                dataPurse.Value = stayUser.Value;
                dataPurse.Tennant = stayUser.Tennant;

                // validação de data checking e checkout -- busta estadia
                var stay = await _stayHotelRepository.GetStayHotel(stayUser.IdStayHotel, true);

                if (!!stay.IsReserved)
                {
                    if (stayUser.Checkin >= stay.Checkin && stayUser.Checkout <= stay.Checkout)
                    {
                        var purse = await _addPurseUseCase.Execute(dataPurse, stayUser.Tennant, false);

                        if (purse.Success)
                        {
                            var reponseUserStay = await _stayHotelRepository.Update(stay, stayUser);

                            // Criar pdf
                            if (!!reponseUserStay)
                            {

                                CreateStayPdf(stayUser, stay);

                                // Retornar pdf de pagamento confirmado!
                                download = await DownloadPay(stayUser.IdStayHotel + "_Pay");

                                if (download == null)
                                {
                                    return download = new JsonResult(result = new Result
                                    {
                                        Message = "Erro, tente novamente!",
                                        Success = false
                                    });
                                }
                            }
                        }
                        else
                        {
                            return download = new JsonResult(result = new Result
                            {
                                Message = purse.Message,
                                Success = purse.Success
                            });
                        }
                    }
                }
                else
                {
                    return download = new JsonResult(result = new Result
                    {
                        Message = "Por favor verifique sua reserva!",
                        Success = false
                    });
                }
                return download;

            }
            catch (Exception ex)
            {
                var logs = Log(ex.Message + "Add reserva de usuário, userId" + stayUser.Tennant, "UserStayUseCase");
                await _logRepository.Add(logs);
                return download = new JsonResult(result = new Result
                {
                    Message = ex.Message,
                    Success = false,
                });
            }
        }
        public async Task<ActionResult> DownloadPay(string fileName)
        {
            FileStreamResult download = null;
            string path = @"C:\Temp\";

            try
            {
                FileStream file = new FileStream(@"C:\temp\" + fileName + ".pdf", FileMode.Open);
                download = new FileStreamResult(file, "application/pdf");
                download.FileDownloadName = ".pdf";

                return download;
            }
            catch (System.Exception ex)
            {

                return download;
            }
        }
    }
}
