using Hotel.Application.Dto;
using Hotel.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.Application.Helper.Helper;

namespace Hotel.Application.UseCases.StayHotel.AddStayHotelUseCase
{
    public sealed class AddStayHotelUseCase : IAddStayHotelUseCase
    {
        private readonly IStayHotelRepository _stayHotelRepository;
        private readonly ILogRepository _logRepository;
        public AddStayHotelUseCase(IStayHotelRepository stayHotelRepository,
            ILogRepository logRepository)
        {
            _stayHotelRepository = stayHotelRepository;
            _logRepository = logRepository;
        }
        public async Task<ResponseDto> Execute(StayHotelDto stay)
        {
            var result = new ResponseDto();
            try
            {
                var responseStayHotel = await _stayHotelRepository.Add(stay);

                return result = new ResponseDto
                {
                    Message = !string.IsNullOrEmpty(responseStayHotel.Id) ? "Sucesso" : "Por favor tente novamente!",
                    Success = !string.IsNullOrEmpty(responseStayHotel.Id) ? true : false,
                };
            }
            catch (Exception ex)
            {
                var logs = Log(ex.Message + "Erro para criar reserva ", "AddStayHotel");
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
