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
    public class CounterDto
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public Guid ETag { get; set; }
        public int Value { get; set; }
    }
}
