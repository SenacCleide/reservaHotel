using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Dto
{
    public class StayHotelDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int Bedrooms { get; set; } 
        public int Value { get; set; }
        public bool IsReserved { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public StayHotelDto() { }

        public StayHotelDto(StayHotelDto purse)
        {
            Id = purse.Id;
            Name = purse.Name;
            Checkin = purse.Checkin;
            Checkout = purse.Checkout;
            Bedrooms = purse.Bedrooms;
            IsReserved = purse.IsReserved;
            CreatedAt = purse.CreatedAt;
            UpdatedAt = purse.UpdatedAt;
            Value = purse.Value;
        }
    }
}
