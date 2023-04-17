using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Dto
{
    public class PurseRequestDto
    {
        public string Tennant { get; set; }
        public int Value { get; set; }
        public PurseRequestDto() { }

        public PurseRequestDto(PurseRequestDto purse)
        {
            Tennant = purse.Tennant;
            Value = purse.Value;
        }
    }
}
