using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.StayHotel.AddStayHotelUseCase
{
    public interface IAddStayHotelUseCase
    {
        Task<ResponseDto> Execute(StayHotelDto stay);
    }
}
