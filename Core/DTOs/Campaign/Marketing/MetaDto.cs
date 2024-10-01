using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Constant.Const;

namespace Core.DTOs.Campaign.Marketing
{
    public class WSATemplateDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } = "en";

        [JsonProperty("category")]
        public string Category { get; set; } = "MARKETING";

        [JsonProperty("components")]
        public List<WSATemplateComponentDto> Components { get; set; }

        public void SetHeader(string text)
        {
            WSATemplateComponentDto component = new WSATemplateComponentDto()
            {
                Type = ComponentMetaTypeConst.Header,
                Text = text,
                Format = "TEXT"
            };


            if (Components is null)
            {
                Components = new List<WSATemplateComponentDto> { component };
            }
            else
            {
                int index = Components.FindIndex(x => x.Type == ComponentMetaTypeConst.Header);

                if (index == -1)
                {
                    Components.Add(component);
                }
                else
                {
                    Components[index].Text = text;
                }
            }
        }

        public void SetBody(string text)
        {
            WSATemplateComponentDto component = new WSATemplateComponentDto()
            {
                Type = ComponentMetaTypeConst.Body,
                Text = text
            };

            if (Components is null)
            {
                Components = new List<WSATemplateComponentDto> { component };
            }
            else
            {
                int index = Components.FindIndex(x => x.Type == ComponentMetaTypeConst.Body);

                if (index == -1)
                {
                    Components.Add(component);
                }
                else
                {
                    Components[index].Text = text;
                }
            }
        }

        public void SetFooter(string text)
        {
            WSATemplateComponentDto component = new WSATemplateComponentDto()
            {
                Type = ComponentMetaTypeConst.Footer,
                Text = text
            };

            if (Components is null)
            {
                Components = new List<WSATemplateComponentDto> { component };
            }
            else
            {
                int index = Components.FindIndex(x => x.Type == ComponentMetaTypeConst.Footer);

                if (index == -1)
                {
                    Components.Add(component);
                }
                else
                {
                    Components[index].Text = text;
                }
            }
        }
    }

    public class WSATemplateComponentDto
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("format")]
        public string? Format { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class WSATemplateResponseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }

    public class WSATemplateDetailDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("components")]
        public List<WSAMediaHeaderComponentDto> Components { get; set; }
    }


    public class WSATemplatesResposeDto
    {

        [JsonProperty("data")]
        public List<WSATemplateDetailDto> Data { get; set; }
    }

    public class WSASendMessageDto
    {
        [JsonProperty("to")]
        public string To { get; set; } = "{to}";

        [JsonProperty("type")]
        public string Type { get; set; } = "template";

        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; } = "whatsapp";

        [JsonProperty("recipient_type")]
        public string RecipientType { get; set; } = "individual";

        [JsonProperty("template")]
        public WSASendBodyMessageDto Template { get; set; } = new WSASendBodyMessageDto();

        public void SetNumberAndTemplate(string phone, string template)
        {
            To = phone;
            Template.Name = template;
        }
    }

    public class WSASendBodyMessageDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public WSASLanguajeMessageDto Language { get; set; } = new WSASLanguajeMessageDto();

        [JsonProperty("components")]
        public List<object> Components = new List<object>();
    }

    public class MediaDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class WSAImageMessageDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("image")]
        public MediaDto Image { get; set; }
    }

    public class WSADocumentMessageDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("document")]
        public MediaDto Document { get; set; }
    }

    public class WSAVideoMessageDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("video")]
        public MediaDto Video { get; set; }
    }


    public class WSAComponentBodyMessageDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("parameters")]
        public List<object> Parameters { get; set; } = new List<object> { };
    }

    public class WSASLanguajeMessageDto
    {
        [JsonProperty("code")]
        public string Code { get; set; } = "en";
    }


    public class WSAResponseSendingDto
    {
        [JsonProperty("messages")]
        public List<WSAMessageSendingDto> Messages { get; set; }
    }

    public class WSAMessageSendingDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("message_status")]
        public string MessageStatus { get; set; }
    }


    public class WhatsAppConfig
    {
        public string Token { get; set; }
        public string WhatsAppBusinessAccountId { get; set; }
        public string AppId { get; set; }
        public string PhoneNumberId { get; set; }
    }

    public class CreateTemplateDto : IValidatableObject
    {
        private string[] _listOfValueFromHeader = ["TEXT", "IMAGE", "VIDEO", "DOCUMENT"];

        public int MarketingCampaignId { get; set; }
        public string HeaderType { get; set; } = string.Empty;
        public string HeaderValue { get; set; } = string.Empty;
        public string MetaId { get; set; } = string.Empty;

        [Required]
        [MaxLength(1024)]
        public string BodyText { get; set; }

        [MaxLength(60)]
        public string FooterText { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Array.Exists(_listOfValueFromHeader, v => v.Equals(HeaderType, StringComparison.OrdinalIgnoreCase)))
            {
                if (HeaderValue == string.Empty)
                {
                    results.Add(new ValidationResult("The HeaderValue must contains un value different to empty"));
                }

                if (MetaId == string.Empty && !HeaderType.Equals("text", StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new ValidationResult("The MetaId must contains un value different to empty"));
                }

                if (HeaderType.Equals("text", StringComparison.OrdinalIgnoreCase) && HeaderValue.Count() > 60 ) {
                    results.Add(new ValidationResult("The HeaderValue can have a maximum length of 60 characteres "));
                }
            }
            else if (HeaderType != string.Empty)
            {
                results.Add(new ValidationResult("The value of HeaderType must be text, image, video, document or empty"));
            }

            return results;
        }
    }

    public class SessionMetaResponseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class HeaderHandleMetaResponseDto
    {
        [JsonProperty("h")]
        public string H { get; set; }
    }

    public class UploadFileMetaResponseDto
    {
        public string H { get; set; }

        public string MediaId { get; set; }
    }

    public class WSACreateTemplateDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } = "en";

        [JsonProperty("category")]
        public string Category { get; set; } = "MARKETING";

        [JsonProperty("components")]
        public List<object> Components = new List<object>();
    }

    public abstract class WSABaseHeaderComponentDto
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("format")]
        public string? Format { get; set; }
    }

    public class WSATextHeaderComponentDto : WSABaseHeaderComponentDto
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class WSAMediaHeaderComponentDto : WSABaseHeaderComponentDto
    {
        [JsonProperty("example")]
        public WSAExampleHeaderComponentDto Example { get; set; }
    }

    public class WSAExampleHeaderComponentDto
    {

        [JsonProperty("header_handle")]
        public List<string> HeaderHandle { get; set; }
    }


    public class WSABodyComponentDto
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }


    public class WSAValuesForTemplateDto
    {
        public string HeaderType { get; set; } = string.Empty;
        public string HeaderValue { get; set; } = string.Empty;
        public string MetaId { get; set; } = string.Empty;
        public string BodyText { get; set; } = string.Empty;
        public string FooterText { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }

    public class ValuesForTemplateDetailDto
    {
        public string HeaderType { get; set; } = string.Empty;
        public string HeaderValue { get; set; } = string.Empty;
        public string MetaId { get; set; } = string.Empty;
        public string BodyText { get; set; } = string.Empty;
        public string FooterText { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }

    public class MetasCredentialsDto : IValidatableObject
    {       
        [Required]
        public string MetaType { get; set; } = string.Empty;

        public string BusinessAccountId { get; set; } = string.Empty;

        public string AppId { get; set; } = string.Empty;

        public string PhoneNumberId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            results = MetaType.ToLower() switch
            {
                MetaTypeConst.WhatsApp => CheckWhatsAppValues(),
                _ => MetaTypeNoFound()
            };

            //

            return results;
        }

        private List<ValidationResult> CheckWhatsAppValues() {
            var results = new List<ValidationResult>();

            if (BusinessAccountId == string.Empty)
            {
                results.Add(new ValidationResult("The BusinessAccountId cannot be empty"));
            }

            if (AppId == string.Empty)
            {
                results.Add(new ValidationResult("The AppId cannot be empty"));
            }

            if (PhoneNumberId == string.Empty)
            {
                results.Add(new ValidationResult("The PhoneNumberId cannot be empty"));
            }

            if (Token == string.Empty)
            {
                results.Add(new ValidationResult("The Token cannot be empty"));
            }

            return results;
        }

        private List<ValidationResult> MetaTypeNoFound()
        {
            var results = new List<ValidationResult>();

            results.Add(new ValidationResult($"The MetaType {MetaType} is invalid"));            

            return results;
        }


    }

    public class OAuthWhatsAppResponseDto
    {
        public string MetaType { get; set; } = string.Empty;
        public string BusinessAccountId { get; set; } = string.Empty;
        public string AppId { get; set; } = string.Empty;
        public string PhoneNumberId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class CredentialsResponseDto
    {

        public int Id { get; set; }

        public string MetaType { get; set; }

        public string Code { get; set; }

        public string Status { get; set; } = "Activo";

    }
}
