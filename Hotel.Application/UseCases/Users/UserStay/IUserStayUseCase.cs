using Hotel.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.Users.UserStay
{
    public interface IUserStayUseCase
    {
        Task<ActionResult> Execute(StayHotelUserDto stayUser);
    }
}
