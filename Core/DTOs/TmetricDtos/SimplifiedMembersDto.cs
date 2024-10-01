using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.TmetricDtos
{
    public class SimplifiedMembersDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int AccountMemberId { get; set; }
        public int UserProfileId { get; set; }
        public string RoleKey { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
