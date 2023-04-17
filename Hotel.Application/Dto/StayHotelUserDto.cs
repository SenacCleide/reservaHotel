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
    public class StayHotelUserDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string IdStayHotel { get; set; }
        public string Tennant { get; set; }
        public int Value { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public StayHotelUserDto() { }

        public StayHotelUserDto(StayHotelUserDto stayUser)
        {
            Id = stayUser.Id;
            Name= stayUser.Name;
            IdStayHotel = stayUser.IdStayHotel;
            Tennant = stayUser.Tennant;
            Value = stayUser.Value;
            Status = stayUser.Status;
            CreatedAt = stayUser.CreatedAt;
            Checkin= stayUser.Checkin;
            Checkout= stayUser.Checkout;
        }
    }
}
