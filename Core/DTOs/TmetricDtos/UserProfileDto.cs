using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTOs.TmetricDtos
{
    
    public class UserProfileDto
    {
        public int userProfileId { get; set; }
        public int activeAccountId { get; set; }
        public string userName { get; set; }
        public string dateFormat { get; set; }
        public string timeFormat { get; set; }
        public DateTime registrationDate { get; set; }
        public TimeZoneInfoDto timeZoneInfo { get; set; }
        public bool isRegistered { get; set; }
        public string email { get; set; }
        public List<AccountMembershipDto> accountMembership { get; set; }
        public bool optinEmail { get; set; }
        public string avatar { get; set; }
        public string defaultAvatar { get; set; }
        public CultureInfoDto cultureInfo { get; set; }
        public List<object> calendarIntegrations { get; set; }
    }

    public class TimeZoneInfoDto
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public double winterOffset { get; set; }
        public double summerOffset { get; set; }
        public double currentOffset { get; set; }
    }

    public class AccountMembershipDto
    {
        public int accountMemberId { get; set; }
        public AccountDto account { get; set; }
        public int role { get; set; }
        public string roleKey { get; set; }
        public PermissionsDto permissions { get; set; }
    }

    public class AccountDto
    {
        public int accountId { get; set; }
        public string externalAccountId { get; set; }
        public int firstWeekDay { get; set; }
        public string accountName { get; set; }
        public string reportTimeFormat { get; set; }
        public int reportTimeRoundingMode { get; set; }
        public int reportTimeRoundingMinutes { get; set; }
        public int editableDays { get; set; }
        public bool reportDetailedTimeEnabled { get; set; }
        public bool canMembersManagePublicProjects { get; set; }
        public bool canMembersCreateTags { get; set; }
        public bool hasDemoData { get; set; }
        public int inactivityStopMinutes { get; set; }
        public bool blurScreenshots { get; set; }
        public PermissionsDto permissions { get; set; }
        public RequiredFieldsDto requiredFields { get; set; }
        public ActivityCaptureSettingsDto activityCaptureSettings { get; set; }
    }

    public class PermissionsDto
    {
        public bool manualTimeEditing { get; set; }
        public bool mobileTimeTracking { get; set; }
        public bool workingOnWeekendsAndHolidays { get; set; }
    }

    public class RequiredFieldsDto
    {
        public bool description { get; set; }
        public bool project { get; set; }
        public bool tags { get; set; }
        public bool taskLink { get; set; }
    }

    public class ActivityCaptureSettingsDto
    {
        public bool activityLevels { get; set; }
        public bool appsAndSites { get; set; }
        public bool details { get; set; }
        public bool screenshots { get; set; }
    }

    public class CultureInfoDto
    {
        public string id { get; set; }
        public string nativeName { get; set; }
    }
}

