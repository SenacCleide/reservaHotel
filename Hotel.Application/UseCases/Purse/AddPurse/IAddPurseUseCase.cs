using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.Purse.AddPurse
{
    public interface IAddPurseUseCase
    {
        Task<ResponseDto> Execute(PurseRequestDto purse, string idUser, bool addPurse);
    }
}
