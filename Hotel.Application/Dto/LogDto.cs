using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hotel.Application.Dto
{
    public class LogDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Error { get; set; }
        public string Method { get; set; }
        public DateTime CreatedAt { get; set; }
        public LogDto() { }
        public LogDto(LogDto user)
        {
            Id = user.Id;
            Error = user.Error;
            Method = user.Method;
            CreatedAt = user.CreatedAt;
        }
    }
}
