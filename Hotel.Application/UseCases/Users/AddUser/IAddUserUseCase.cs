using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.Users.AddUser
{
    public interface IAddUserUseCase
    {
        Task<ResponseDto> Execute(UserDto user);
    }
}
