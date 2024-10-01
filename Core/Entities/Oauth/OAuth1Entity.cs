using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Oauth
{
    public class OAuth1Entity
    {
        [BsonElement("BaseUrl")]
        public string BaseUrl { get; set; }

        [BsonElement("ConsumerKey")]
        public string ConsumerKey { get; set; }

        [BsonElement("ConsumerSecret")]
        public string ConsumerSecret { get; set; }

        [BsonElement("TokenKey")]
        public string TokenKey { get; set; }

        [BsonElement("TokenSecret")]
        public string TokenSecret { get; set; }

        [BsonElement("Realm")]
        public string Realm { get; set; }

        [BsonElement("ScriptId")]
        public string ScriptId { get; set; }

        [BsonElement("DeployId")]
        public string DeployId { get; set; }

    }
}
