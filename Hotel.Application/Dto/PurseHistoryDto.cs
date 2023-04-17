using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Dto
{
    public class PurseHistoryDto
    {
        public int Id { get; set; }
        public int IdPurse { get; set; }
        public int Value { get; set; }
        public int ValueAdded { get; set; }
        public int WithDrawnAmont { get; set; }
        public int PreviousValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public PurseHistoryDto() { }

        public PurseHistoryDto(PurseHistoryDto purse)
        {
            Id = purse.Id;
            IdPurse = purse.IdPurse;
            Value = purse.Value;
            ValueAdded = purse.ValueAdded;
            WithDrawnAmont = purse.WithDrawnAmont;
            PreviousValue = purse.PreviousValue;
            CreatedAt = purse.CreatedAt;
        }
    }
}
