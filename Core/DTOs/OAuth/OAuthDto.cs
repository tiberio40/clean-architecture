using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.OAuth
{
    public class OAuthDto<T>
    {
        public ObjectId AuthConfigId { get; set; }

        [Required]
        public string DataOrigin { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string ConnectionName { get; set; }

        [Required]
        public T Auth { get; set; }

    }


    public class OAuth1Dto
    {
        [Required]
        public string BaseUrl { get; set; }

        [Required]
        public string ConsumerKey { get; set; }

        [Required]
        public string ConsumerSecret { get; set; }

        [Required]
        public string TokenKey { get; set; }

        [Required]
        public string TokenSecret { get; set; }

        [Required]
        public string Realm { get; set; }

        [Required]
        public string ScriptId { get; set; }

        [Required]
        public string DeployId { get; set; }

    }

    public class OAuthBearerDto
    {
        [Required]
        public string BaseUrl { get; set; }

        [Required]
        public string TokenSecret { get; set; } = string.Empty;
    }

    public class OAuth1AuthorizationDto
    {
        public string BaseUrl { get; set; }

        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }

        public string TokenKey { get; set; }

        public string TokenSecret { get; set; }

        public string Realm { get; set; }

        public string ScriptId { get; set; }

        public string DeployId { get; set; }

    }

    public class BearerAuthorizationDto
    {
        public string BaseUrl { get; set; }

        public string TokenSecret { get; set; } = string.Empty;
    }

    public class OAuthAuthorizationDto
    {
        public string TokenSecret { get; set; } = string.Empty;
    }

    public class OAuthWhatsAppDto
    {
        public string BusinessAccountId { get; set; } = string.Empty;
        public string AppId { get; set; } = string.Empty;
        public string PhoneNumberId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }


}
