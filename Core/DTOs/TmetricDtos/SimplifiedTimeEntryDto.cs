using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.TmetricDtos
{
    
    public class SimplifiedTimeEntryDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? email { get; set; }
        public DateTime endTime { get; set; }
        public DateTime startTime { get; set; }
        public string projectName { get; set; }
        public string description { get; set; }
        public int projectId { get; set; }
        public int userProfileId { get; set; }
        public string userName { get; set; }
        public TimeZoneDto timeZone { get; set; }
        public double durationHours
        {
            get; set;
        }
    
    }
}
