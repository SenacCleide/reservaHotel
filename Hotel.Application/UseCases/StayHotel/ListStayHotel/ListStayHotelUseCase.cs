using Hotel.Application.Dto;
using Hotel.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.Application.Helper.Helper;

namespace Hotel.Application.UseCases.StayHotel.ListStayHotel
{
    public sealed class ListStayHotelUseCase : IListStayHotelUseCase
    {
        private readonly IStayHotelRepository _stayHotelRepository;
        private readonly ILogRepository _logRepository;
        public ListStayHotelUseCase(IStayHotelRepository stayHotelRepository,
            ILogRepository logRepository) 
        {
            _stayHotelRepository = stayHotelRepository;
            _logRepository = logRepository;
        }
        public async Task<List<StayHotelDto>> Execute(bool isReserved)
        {
            var result = new List<StayHotelDto>();
            try
            {
                return await _stayHotelRepository.ListStayHotel(isReserved);
            }
            catch(Exception ex)
            {
                var logs = Log(ex.Message + "Erro para Listar reserva ", "ListStayHotel");
                await _logRepository.Add(logs);
                return result;
            }
        }
    }
}
