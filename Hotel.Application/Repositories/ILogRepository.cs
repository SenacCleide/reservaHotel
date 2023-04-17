using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Repositories
{
    public interface ILogRepository
    {
        Task<LogDto> Add(LogDto log);
    }
}
