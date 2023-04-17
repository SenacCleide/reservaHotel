using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.AuthUse
{
    public interface IAuthUseUseCase
    {
        Task<ResponseDto<UserDto>> Execute(AuthUserDto authUser);
    }
}
