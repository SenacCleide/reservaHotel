using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.StayHotel.ListStayHotel
{
    public interface IListStayHotelUseCase
    {
        Task<List<StayHotelDto>> Execute(bool isReserved);
    }
}
