using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Oauth
{
    public class OAuthBearerEntity
    {
        [BsonElement("BaseUrl")]
        public string BaseUrl { get; set; }

        [BsonElement("TokenSecret")]
        public string TokenSecret { get; set; }

    }
}
