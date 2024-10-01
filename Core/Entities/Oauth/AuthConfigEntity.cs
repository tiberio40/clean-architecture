using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace Core.Entities.Oauth
{
    public class AuthConfigEntity

    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId AuthConfigId { get; set; }

        [BsonElement("DataOrigin")]
        public string DataOrigin { get; set; }

        [BsonElement("Version")]
        public string Version { get; set; }

        [BsonElement("ConnectionName")]
        public string ConnectionName { get; set; }

        [BsonElement("Auth")]
        public IDictionary<string, object> Auth { get; set; }

    }
}
