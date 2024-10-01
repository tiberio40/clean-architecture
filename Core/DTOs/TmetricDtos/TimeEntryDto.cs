using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.TmetricDtos
{
    public class TimeEntryDto
    {
        public DateTime endTime { get; set; }
        public DateTime startTime { get; set; }
        public bool isBillable { get; set; }
        public bool isInvoiced { get; set; }
        public int timeEntryId { get; set; }
        public TimeEntryDetailsDto details { get; set; }
        public string projectName { get; set; }

        public double durationHours
        {
            get
            {
                return (endTime - startTime).TotalHours;
            }
        }
    }

    public class TimeEntryDetailsDto
    {
        public string description { get; set; }
        public int projectId { get; set; }
    }

    public class TimeZoneDto
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public double winterOffset { get; set; }
        public double summerOffset { get; set; }
        public double currentOffset { get; set; }
    }

    public class UserProfileTimeDto
    {
        public int userProfileId { get; set; }
        public string userName { get; set; }

        public TimeZoneDto timeZone { get; set; }
        public List<TimeEntryDto> entries { get; set; }

        public double totalHours
        {
            get
            {
                double totalHours = 0;
                if (entries.Count > 0)
                {
                    foreach (var entry in entries)
                    {
                        totalHours += entry.durationHours;
                    }
                }
                return totalHours;
            }
        }

    }

    public class UserTimeEntriesDto
    {
        public List<TimeEntryDto>? entries { get; set; }

        public double totalHours
        {
            get
            {
                double totalHours = 0;
                if (entries != null && entries.Count > 0)
                {
                    foreach (var entry in entries)
                    {
                        totalHours += entry.durationHours;
                    }
                }
                return totalHours;
            }
        }
    }
 
}

