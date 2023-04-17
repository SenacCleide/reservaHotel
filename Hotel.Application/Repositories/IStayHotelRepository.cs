using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Repositories
{
    public interface IStayHotelRepository
    {
        Task<List<StayHotelDto>> ListStayHotel(bool isReserved);
        Task<StayHotelDto> GetStayHotel(string id, bool isReserved);
        Task<StayHotelDto> Add(StayHotelDto stay);
        Task<bool> Update(StayHotelDto stayHotel, StayHotelUserDto stay);
        Task<StayHotelUserDto> AddUserStay(StayHotelUserDto stay);
    }
}
