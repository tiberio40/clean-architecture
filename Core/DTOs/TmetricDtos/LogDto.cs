using Core.DTOs.TmetricDtos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Core.DTOs.TmetricDtos
{

    public class LogDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}