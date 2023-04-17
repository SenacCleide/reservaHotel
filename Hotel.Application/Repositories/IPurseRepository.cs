using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Repositories
{
    public interface IPurseRepository
    {
        Task<PurseDto> Get(string usersId);
        Task<int> Add(PurseDto purse);
        Task<int> Update(PurseHistoryDto purse);
    }
}
