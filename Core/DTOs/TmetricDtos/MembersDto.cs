using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.TmetricDtos
{
    public class MembersDto
    {
        public int AccountMemberId { get; set; }
        public int UserProfileId { get; set; }
        public UserProfileDTO UserProfile { get; set; }
        public string RoleKey { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public int AccountId { get; set; }
    }

    public class UserProfileDTO
    {
        public int UserProfileId { get; set; }
        public int ActiveAccountId { get; set; }
        public string UserName { get; set; }
        public bool IsRegistered { get; set; }
        public string Email { get; set; }
        public bool OptinEmail { get; set; }
    }
}
