using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Dto
{
    public class PurseDto
    {
        public int Id { get; set; }
        public string IdUsers { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PurseDto() { }

        public PurseDto(PurseDto purse)
        {
            Id = purse.Id;
            IdUsers = purse.IdUsers;
            Value = purse.Value;
            CreatedAt = purse.CreatedAt;
            UpdatedAt = purse.UpdatedAt;
        }
    }
}
