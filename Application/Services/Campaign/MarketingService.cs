using Amazon.Auth.AccessControlPolicy;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Application.Interfaces.Campaign;
using AutoMapper;
using Azure.Core;
using Common.Exceptions;
using Common.Resources;
using Common.Utility;
using Core.DTOs;
using Core.DTOs.Campaign.Marketing;
using Core.DTOs.OAuth;
using Core.DTOs.TmetricDtos;
using Core.Entities;
using Core.Entities.Campaign;
using Core.Entities.Oauth;
using Core.Interfaces;
using Infrastructure.Common;
using Infrastructure.Options;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Common.Constant.Const;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services.Campaign
{
    public class MarketingService : IMarketingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthConfigService _AuthConfigService;
        private readonly IExternalRequestClient _externalRequestClient;
        private readonly IMapper _mapper;
        private readonly WhatsAppConfig _whatsAppConfig;



        public MarketingService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthConfigService
            authConfigService,
            IExternalRequestClient externalRequestClient,
            WhatsAppConfig whatsAppConfig)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _AuthConfigService = authConfigService;
            _externalRequestClient = externalRequestClient;
            _whatsAppConfig = whatsAppConfig;
        }

        public IEnumerable<MarketingCardDto> GetAllMarketings()
        {

            return _unitOfWork.MarketingRepository
                .GetAll(x => x.MarketingStatusEntity)
                .OrderBy(x => x.Name)
                .Select(x => new MarketingCardDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Cover = x.Cover,
                    Status = x.MarketingStatusEntity.Name
                });
        }

        public MarketingCardDto GetMarketingById(int id)
        {
            MarketingCardDto result = new MarketingCardDto();

            var query = _unitOfWork.MarketingRepository.FirstOrDefault(x => x.Id == id, x => x.MarketingStatusEntity);
            if (query != null)
            {
                result = new MarketingCardDto()
                {
                    Id = query.Id,
                    Name = query.Name,
                    Cover = query.Cover,
                    Status = query.MarketingStatusEntity.Name,
                    CodeStatus = query.MarketingStatusEntity.Code,
                    HasCredentials = query.OAuthId == "" ? false : true
                };
            }

            return result;
        }

        public IEnumerable<MarketingCampaignEntity> GetMarketingCampaigns(MarketingCampaignDto model)
        {
            return _unitOfWork.MarketingCampaignRepository
                .FindAll(x => x.MarketingId == model.MarketingId.GetValueOrDefault())
                .Skip(model.Start.GetValueOrDefault()).Take(model.End.GetValueOrDefault());
        }

        public IEnumerable<MarketingCampaignEntity> GetMarketingCampaigns(int marketingId)
        {
            return _unitOfWork.MarketingCampaignRepository.FindAll(x => x.MarketingId == marketingId);
        }

        public MarketingCampaignEntity GetMarketingCampaign(int id)
        {
            return _unitOfWork.MarketingCampaignRepository.FirstOrDefault(x => x.Id == id) ?? new MarketingCampaignEntity();
        }

        public IEnumerable<MarketingUserEntity> GetMarketingUsers(MarketingUserDto model)
        {
            return _unitOfWork.MarketingUserRepository
                .FindAll(x => x.MarketingCampaignId == model.MarketingCampaignId.GetValueOrDefault())
                .Skip(model.Start.GetValueOrDefault()).Take(model.End.GetValueOrDefault());
        }

        public IEnumerable<MarketingUserEntity> GetMarketingUsers(int marketingCampaignId)
        {
            return _unitOfWork.MarketingUserRepository.FindAll(x => x.MarketingCampaignId == marketingCampaignId);
        }

        public async Task<MarketingEntity> CreateMarketing(CreateMarketingDto model)
        {
            MarketingEntity entity = _mapper.Map<MarketingEntity>(model);
            entity.MarketingStatusId = _unitOfWork.MarketingStatusRepository.FirstOrDefault(x => x.Code == "created").Id;
            _unitOfWork.MarketingRepository.Insert(entity);
            await _unitOfWork.Save();
            return entity;
        }

        public async Task<MarketingEntity> EditMarketing(int id, CreateMarketingDto model)
        {
            var entity = _unitOfWork.MarketingRepository.FirstOrDefault(x => x.Id == id);
            entity = _mapper.Map<CreateMarketingDto, MarketingEntity>(model, entity);
            _unitOfWork.MarketingRepository.Update(entity);
            await _unitOfWork.Save();

            return entity;
        }

        public async Task<bool> SetOAuthSource(int id, OAuth1Dto model)
        {
            bool result = false;
            using (var db = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var marketing = GetMarketing(id);

                    var status = _unitOfWork.MarketingStatusRepository.FirstOrDefault(x => x.Code == "active");

                    marketing.MarketingStatusId = status.Id;

                    AuthConfigEntity oauth = new AuthConfigEntity()
                    {
                        DataOrigin = ClientConst.NetSuite,
                        ConnectionName = ClientConst.NetSuite,
                        Version = OAuthTypeConst.OAuth1,
                        Auth = model.AsDictionary()
                    };

                    if (marketing?.OAuthId == "")
                    {
                        await _AuthConfigService.CreateAuthConfigAsync(oauth);
                        marketing.OAuthId = oauth.AuthConfigId.ToString();
                    }
                    else
                    {
                        oauth.AuthConfigId = ObjectId.Parse(marketing.OAuthId);
                        await _AuthConfigService.UpdateAuthConfigAsync(oauth.AuthConfigId, oauth);
                    }

                    _unitOfWork.MarketingRepository.Update(marketing);
                    await _unitOfWork.Save();
                    await db.CommitAsync();

                    result = true;
                }
                catch (BusinessException ex)
                {
                    await db.RollbackAsync();

                    throw ex;
                }
                catch (Exception ex)
                {
                    await db.RollbackAsync();
                    throw new Exception(GeneralMessages.Error500, ex);
                }
            }

            return result;
        }

        public async Task<OAuth1Dto> GetOAuthSource(int id)
        {
            var marketing = GetMarketing(id);

            var result = await _AuthConfigService.GetAuthConfigByIdAsync(ObjectId.Parse(marketing.OAuthId));

            return result.Auth.ToObject<OAuth1Dto>();
        }

        public async Task<ResponseDto> LoadMarketingCampaign(int id)
        {
            MarketingEntity marketing = _unitOfWork.MarketingRepository.FirstOrDefault(x => x.OAuthId != string.Empty && x.Id == id);
            MarketingStatusEntity status = GetStatus("syncing");

            if (marketing.MarketingStatusId == status.Id)
            {
                return new ResponseDto() { IsSuccess = false, Message = GeneralMessages.SyncingInProcess };
            }

            marketing.MarketingStatusId = status.Id;
            _unitOfWork.MarketingRepository.Update(marketing);
            await _unitOfWork.Save();

            ObjectId oauthId = ObjectId.Parse(marketing.OAuthId);
            AuthConfigEntity configurations = await _AuthConfigService.GetAuthConfigByIdAsync(oauthId);



            var filteredUsers =  await ExtractUserFromNetSuite(marketing, configurations.Auth.ToObject<OAuth1Dto>());
            await CreateTemlateForFirstTime(marketing, filteredUsers);

        
            if (marketing.MarketingStatusId != status.Id)
            {
                return new ResponseDto() { IsSuccess = false, Message = GeneralMessages.SyncingWithProblems };
            }


            status = GetStatus("active");



            marketing.MarketingStatusId = status.Id;

            _unitOfWork.MarketingRepository.Update(marketing);
            await _unitOfWork.Save();

            return new ResponseDto() { IsSuccess = true, Message = GeneralMessages.FinishedSyncing };
        }

        public ValuesForTemplateDetailDto GetMarketingTemplate(int marketingCampaignId)
        {
            var result = _unitOfWork.MarketingCampaignRepository.GetAll(x => x.MarketingTemplateEntities).FirstOrDefault(x => x.Id == marketingCampaignId);
            if (result is not null)
            {
                var marketingTemplat = result.MarketingTemplateEntities.First();

                if (marketingTemplat.FormValues == string.Empty)
                {
                    return new ValuesForTemplateDetailDto();
                }

                var values = JsonConvert.DeserializeObject<ValuesForTemplateDetailDto>(marketingTemplat.FormValues);
                values.Status = marketingTemplat.Status;
                values.Name = marketingTemplat.TitleTemplateMeta;

                return values;
            }

            return new ValuesForTemplateDetailDto();
        }

        public async Task UpdateTemplateStatus()
        {
            BearerAuthorizationDto token = new BearerAuthorizationDto() { TokenSecret = _whatsAppConfig.Token };
            var templates = _unitOfWork.MarketingTemplateRepository.FindAll(x => x.Status != MetaStatusConst.Approved).ToList();

            if (templates.Count() == 0)
            {
                return;
            }

            WSATemplatesResposeDto templatesFromMeta = await _externalRequestClient.Get<WSATemplatesResposeDto, BearerAuthorizationDto>($"https://graph.facebook.com/v20.0/{_whatsAppConfig.WhatsAppBusinessAccountId}/message_templates", "", token);


            for (int index = 0; index < templates.Count(); index++)
            {
                var result = templatesFromMeta.Data.FirstOrDefault(x => x.Id == templates[index].TemplateMetaId);

                if (result is null)
                {
                    templates[index].Status = MetaStatusConst.NoFound;
                    _unitOfWork.MarketingTemplateRepository.Update(templates[index]);
                }
                else if (templates[index].Status != result.Status)
                {
                    templates[index].Status = result.Status;
                    _unitOfWork.MarketingTemplateRepository.Update(templates[index]);
                }
            }

            await _unitOfWork.Save();
        }

        public async Task SendingMessages()
        {
            
            string hour = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}";

            var marketingCampaigns = _unitOfWork.MarketingCampaignRepository
                .GetAll([x => x.MarketingUserEntities, y => y.MarketingTemplateEntities])
                //.Where(x => x.HourForSending == hour)
                .Where(x => x.MarketingTemplateEntities.Any(y => y.Status == MetaStatusConst.Approved))
                .Where(x => x.MarketingUserEntities.Count() > 0);

            try
            {
                foreach (var marketingCampaign in marketingCampaigns)
                {
                    BearerAuthorizationDto token = new BearerAuthorizationDto();
                    List<string> messages = new List<string>();
                    OAuthWhatsAppDto whatsAppConfig = await GetMetaToken(marketingCampaign);

                    token.TokenSecret = whatsAppConfig.Token;

                    var template = marketingCampaign.MarketingTemplateEntities.First();
                    foreach (var contact in marketingCampaign.MarketingUserEntities)
                    {
                        string message = template.Message.Replace("{to}", contact.ContactPhone);
                        messages.Add(message);
                    }

                    foreach (var message in messages)
                    {
                        var resulxt = await _externalRequestClient.Post<string, WSAResponseSendingDto, BearerAuthorizationDto>(message, $"https://graph.facebook.com/v20.0/{whatsAppConfig.PhoneNumberId}/messages", "", token);
                    }
                }

            }
            catch (Exception ex) { 
            
            }
        }

        public async Task<UploadFileMetaResponseDto> UploadFileForTemplate(IFormFile file, int marketingCampaignId)
        {

            OAuthWhatsAppDto whatsAppConfig = await GetMetaTokenBymarketingCampaignId(marketingCampaignId);

            BearerAuthorizationDto token = new BearerAuthorizationDto() { TokenSecret = whatsAppConfig.Token };

            OAuthAuthorizationDto token2 = new OAuthAuthorizationDto() { TokenSecret = whatsAppConfig.Token };

            var task1 = Task.Run(() =>
            {
                string baseUrl = $"https://graph.facebook.com/v20.0/{whatsAppConfig.AppId}/uploads/";
                string apiEndPoint = $"?file_length={file.Length}&file_type={file.ContentType}";

                var sessionMeta = _externalRequestClient.Post<object, SessionMetaResponseDto, BearerAuthorizationDto>
                (new object { }, baseUrl, apiEndPoint, token).Result;

                string baseUrl2 = $"https://graph.facebook.com/v20.0/{sessionMeta.Id}";
                var headers = new Dictionary<string, string> { { "file_offset", "0" } };

                return _externalRequestClient.Post<HeaderHandleMetaResponseDto, OAuthAuthorizationDto>(file, baseUrl2, headers, token2);
            });

            var task2 = Task.Run(() =>
            {
                string baseUrl3 = $"https://graph.facebook.com/v20.0/{whatsAppConfig.PhoneNumberId}/media";
                var multipartContent = new MultipartFormDataContent();
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
                    byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                    multipartContent.Add(byteArrayContent, "file", file.FileName);
                    multipartContent.Add(new StringContent("whatsapp"), "messaging_product");

                    return _externalRequestClient.Post<SessionMetaResponseDto, BearerAuthorizationDto>(multipartContent, baseUrl3, new Dictionary<string, string>(), token);
                }
            });

            var tasks = new Task[] {
                 task1,
                 task2
             };
            await Task.WhenAll(tasks);
            var result1 = await task1;
            var result2 = await task2;

            return new UploadFileMetaResponseDto()
            {
                H = result1.H,
                MediaId = result2.Id
            };
        }

        public async Task EditTemplate(CreateTemplateDto model)
        {
            try
            {
                BearerAuthorizationDto token = new BearerAuthorizationDto();
                OAuthWhatsAppDto whatsAppConfig = await GetMetaTokenBymarketingCampaignId(model.MarketingCampaignId);
                token.TokenSecret = whatsAppConfig.Token;

                string[] valuesArray = ["VIDEO", "DOCUMENT", "IMAGE"];

                WSAValuesForTemplateDto values = new WSAValuesForTemplateDto()
                {
                    BodyText = model.BodyText,
                    FooterText = model.FooterText,
                    MetaId = model.MetaId,
                };

                string name = Guid.NewGuid().ToString().Replace("-", "");
                WSACreateTemplateDto WSATemplateDto = new WSACreateTemplateDto();
                WSATemplateDto.Name = name;

                MarketingTemplateEntity marketingTemplate = _unitOfWork.MarketingCampaignRepository
                    .GetAll(x => x.MarketingTemplateEntities)
                    .FirstOrDefault(x => x.Id == model.MarketingCampaignId).MarketingTemplateEntities.First();

                if (model.HeaderType.Equals("text", StringComparison.OrdinalIgnoreCase))
                {
                    WSATextHeaderComponentDto header = new WSATextHeaderComponentDto()
                    {
                        Type = "HEADER",
                        Format = "TEXT",
                        Text = model.HeaderValue
                    };

                    WSATemplateDto.Components.Add(header);

                    values.HeaderType = model.HeaderType.ToUpper();

                }
                else if (model.HeaderType != string.Empty)
                {
                    WSAMediaHeaderComponentDto header = new WSAMediaHeaderComponentDto()
                    {
                        Type = "HEADER",
                        Format = model.HeaderType.ToUpper(),
                        Example = new WSAExampleHeaderComponentDto() { HeaderHandle = [model.HeaderValue] }
                    };

                    WSATemplateDto.Components.Add(header);

                    values.HeaderType = model.HeaderType.ToUpper();
                }

                WSABodyComponentDto body = new WSABodyComponentDto()
                {
                    Type = "BODY",
                    Text = model.BodyText
                };

                WSATemplateDto.Components.Add(body);

                if (model.FooterText != string.Empty)
                {
                    WSABodyComponentDto footer = new WSABodyComponentDto()
                    {
                        Type = "FOOTER",
                        Text = model.FooterText
                    };

                    WSATemplateDto.Components.Add(footer);
                }


                var result = await SaveTemplateToMeta(WSATemplateDto);


                if (valuesArray.Contains(model.HeaderType.ToUpper()))
                {
                    string url = $"https://graph.facebook.com/v20.0/{whatsAppConfig.BusinessAccountId}/message_templates?name={name}";

                    WSATemplatesResposeDto templatesFromMeta = await _externalRequestClient
                        .Get<WSATemplatesResposeDto, BearerAuthorizationDto>(url, "", token);

                    string mediaUrl = templatesFromMeta.Data.First().Components.Where(x => x.Type.ToUpper() == "HEADER").First().Example.HeaderHandle.FirstOrDefault() ?? "";
                    values.HeaderValue = model.HeaderValue;
                    values.Link = mediaUrl;
                    //string mediaUrl

                }


                string json = JsonConvert.SerializeObject(values);


                marketingTemplate.Status = result.Status;
                marketingTemplate.TemplateMetaId = result.Id;
                marketingTemplate.TitleTemplateMeta = name;
                marketingTemplate.FormValues = json;

                long metaId = 0;

                long.TryParse(model.MetaId, out metaId);

                marketingTemplate.Message = CreateMessageForRequest(WSATemplateDto, metaId);

                _unitOfWork.MarketingTemplateRepository.Update(marketingTemplate);
                await _unitOfWork.Save();

            }
            catch (NullReferenceException ex)
            {
                throw new Exception(GeneralMessages.ItemNoFound, ex);
            }


        }

        public async Task<int> SetRecurring(RecurringDto model)
        {
            var data = _unitOfWork.MarketingCampaignRepository.FirstOrDefault(x => x.Id == model.CampaignId);

            if (data != null)
            {
                data.HourForSending = model.Hour;

                _unitOfWork.MarketingCampaignRepository.Update(data);

                return await _unitOfWork.Save();
            }

            return 0;
        }

        public async Task<bool> SetMetaCredentials(int id, MetaCretentialDto model) { 
            var metaConfiguration = _unitOfWork.MetaConfigurationRepository.FirstOrDefault(x => x.Id == model.MetaConfigurationId);
            var marketing = _unitOfWork.MarketingRepository.FirstOrDefault(x => x.Id == id);

            if (metaConfiguration is not null && marketing is not null) {
                marketing.MarketingStatusId = metaConfiguration.Id;

                _unitOfWork.MarketingRepository.Update(marketing);

                return await _unitOfWork.Save() == 1 ? true: false;
            }

            return false;
        }


        #region Private

        private MarketingEntity GetMarketing(int id)
        {
            var marketing = _unitOfWork.MarketingRepository.FirstOrDefault(x => x.Id == id);

            if (marketing is null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Configuración de Marketing");
                throw new BusinessException(message);
            }


            return marketing;
        }

        private async Task<List<NetSuteUserResponseDto>> ExtractUserFromNetSuite(MarketingEntity marketing, OAuth1Dto oauthCredeentials)
        {
            List<NetSuteUserResponseDto> filteredUsers = new List<NetSuteUserResponseDto>();

            try
            {
                bool bucle = true;
                int start = 0, end = 999;
                List<NetSuteUserResponseDto> usersFromNetSuite = new List<NetSuteUserResponseDto>();
                OAuth1AuthorizationDto oauthCredentials = _mapper.Map<OAuth1AuthorizationDto>(oauthCredeentials);
                string apiEndPoint = $"?script={oauthCredentials.ScriptId}&deploy={oauthCredentials.DeployId}";

                while (bucle)
                {
                    List<NetSuiteRequestDto> requestDtos = new List<NetSuiteRequestDto>();
                    requestDtos.Add(new NetSuiteRequestDto()
                    {
                        action = "_search",
                        searchId = "customsearch_gd_wapp_camp_events",
                        start = start,
                        end = end,
                    });

                    start += 1000;
                    end += 1000;

                    var result = await _externalRequestClient.Post<List<NetSuiteRequestDto>, NetSuteBodyResponseDto, OAuth1AuthorizationDto>
                        (requestDtos, oauthCredentials.BaseUrl, apiEndPoint, oauthCredentials);

                    if (result.AmountUsers != end)
                    {
                        bucle = false;
                    }
                    usersFromNetSuite.AddRange(result.Users);
                }

                var marketingCampaigns = _unitOfWork.MarketingCampaignRepository.FindAll(x => x.MarketingId == marketing.Id).Select(x => x.OriginId).ToArray();
                return usersFromNetSuite.Where(x => !marketingCampaigns.Contains(x.IDCampania)).ToList();               
            }
            catch (Exception ex)
            {
                if (ex is ExternalRequestException)
                {
                    var badCredential = GetStatus("badCredential");
                    marketing.MarketingStatusId = badCredential.Id;
                    _unitOfWork.MarketingRepository.Update(marketing);
                    await _unitOfWork.Save();

                    return filteredUsers;
                }
                else {
                    throw new Exception(GeneralMessages.Error500, ex);
                }               
            }           
        }


        private async Task CreateTemlateForFirstTime(MarketingEntity marketing, List<NetSuteUserResponseDto> filteredUsers) {
            try
            {
                var rowForRecording = CreateCampaignTree(marketing.Id, filteredUsers);

                foreach (var row in rowForRecording)
                {
                    _unitOfWork.MarketingCampaignRepository.Insert(row);
                }

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                if (ex is ExternalRequestException)
                {
                    var badCredential = GetStatus("badMetaCredential");
                    marketing.MarketingStatusId = badCredential.Id;
                    _unitOfWork.MarketingRepository.Update(marketing);
                    await _unitOfWork.Save();
                }
                else
                {
                    throw new Exception(GeneralMessages.Error500, ex);
                }
            }            
        }

        public List<MarketingCampaignEntity> CreateCampaignTree(int marketingId, List<NetSuteUserResponseDto> users)
        {
            var recurringType = _unitOfWork.RecurringTypeRepository.FirstOrDefault(x => x.Code == "once");
            
            List<MarketingCampaignEntity> marketingCampaigs = new List<MarketingCampaignEntity>();

            List<MarketingTemplateEntity> marketingTemplates = new List<MarketingTemplateEntity>();

            var SyncedAt = DateTime.Now;

            marketingCampaigs = users
                .GroupBy(x => new { x.IDCampania, x.Titulo, x.FechaCreacion })
                .Select(g =>
                {
                    return new MarketingCampaignEntity
                    {
                        OriginId = g.Key.IDCampania,
                        Title = g.Key.Titulo,
                        CreatedAt = g.First().CreatedAt,
                        SyncedAt = SyncedAt,
                        MarketingId = marketingId,
                        HourForSending = g.First().HourForSending,
                        RecurringTypeId = recurringType.Id,
                        IndefiniteEndDate = false,
                        StarDateRecurring = g.First().TimeExecute,
                        EndDateRecurring =  g.First().TimeExecute,
                        MarketingUserEntities = g.Select(y => new MarketingUserEntity
                        {
                            ContactId = y.ContactId,
                            ContactName = y.ContactName,
                            ContactPhone = string.Concat(y.ContactPhone.Where(c => !char.IsWhiteSpace(c))),
                        }).ToList(),
                        MarketingTemplateEntities = SetMarketingTemplate(g.First())
                    };
                })
                .ToList();


            return marketingCampaigs;
        }

        private List<MarketingTemplateEntity> SetMarketingTemplate(NetSuteUserResponseDto value)
        {
            string nameOfTemplate = Guid.NewGuid().ToString().Replace("-", "");

            WSATemplateDto WSATemplateDto = new WSATemplateDto();
            WSATemplateDto.Name = nameOfTemplate;

            WSATemplateDto.SetHeader(RemoveAllHtmlTags(value.Titulo));
            WSATemplateDto.SetBody(RemoveAllHtmlTags(value.WappTemplateMessage));




            MarketingTemplateEntity whatsapp = new MarketingTemplateEntity
            {
                Status = MetaStatusConst.Pending,
                Message = JsonConvert.SerializeObject(WSATemplateDto),
                TitleTemplateMeta = nameOfTemplate
            };

            var result = SendWSATemplateToMeta(WSATemplateDto).Result;

            WSAValuesForTemplateDto WSAValuesForTemplateDto = new WSAValuesForTemplateDto()
            {
                BodyText = RemoveAllHtmlTags(value.WappTemplateMessage),
                HeaderType = "TEXT",
                HeaderValue = value.Titulo,
            };

            WSACreateTemplateDto message = new WSACreateTemplateDto();
            message.Name = nameOfTemplate;


            whatsapp.Status = result.Status;
            whatsapp.TemplateMetaId = result.Id;
            whatsapp.FormValues = JsonConvert.SerializeObject(WSAValuesForTemplateDto);
            whatsapp.Message = CreateMessageForRequest(message, 0);


            return new List<MarketingTemplateEntity> { whatsapp };
        }

        private async Task<WSATemplateResponseDto> SendWSATemplateToMeta(WSATemplateDto body)
        {
            BearerAuthorizationDto token = new BearerAuthorizationDto() { TokenSecret = _whatsAppConfig.Token };
            var result = await _externalRequestClient.Post<WSATemplateDto, WSATemplateResponseDto, BearerAuthorizationDto>(body, $"https://graph.facebook.com/v20.0/{_whatsAppConfig.WhatsAppBusinessAccountId}/message_templates", "", token);

            return result;
        }

        private async Task<WSATemplateResponseDto> SaveTemplateToMeta(WSACreateTemplateDto body)
        {
            BearerAuthorizationDto token = new BearerAuthorizationDto() { TokenSecret = _whatsAppConfig.Token };
            var result = await _externalRequestClient.Post<WSACreateTemplateDto, WSATemplateResponseDto, BearerAuthorizationDto>(body, $"https://graph.facebook.com/v20.0/{_whatsAppConfig.WhatsAppBusinessAccountId}/message_templates", "", token);

            return result;
        }

        public string RemoveAllHtmlTags(string text)
        {
            string pattern = @"\{.*?\}|\{\{.*?\}\}";
            text = Regex.Replace(text, pattern, string.Empty);

            string bold = @"<\/?b>";
            text = Regex.Replace(text, bold, string.Empty);

            string italic = @"<\/?i>";
            text = Regex.Replace(text, italic, string.Empty);

            string strikethrough = @"<\/?s>";
            text = Regex.Replace(text, strikethrough, string.Empty);

            string htmlTag = "<.*?>";
            text = Regex.Replace(text, htmlTag, string.Empty);

            return text;
        }

        public string CreateMessageForRequest(WSACreateTemplateDto WSATemplateDto, Int64 metaId)
        {
            var message = new WSASendMessageDto();
            message.Template.Name = WSATemplateDto.Name;

            var header = WSATemplateDto.Components.Where(x => x.GetType() == typeof(WSAMediaHeaderComponentDto)).FirstOrDefault();

            if (header is not null)
            {
                WSAMediaHeaderComponentDto wSATextHeaderComponentDto = header as WSAMediaHeaderComponentDto;


                if (wSATextHeaderComponentDto.Format.ToUpper() != "TEXT")
                {
                    WSAComponentBodyMessageDto component = new WSAComponentBodyMessageDto();
                    component.Type = "header";

                    if (wSATextHeaderComponentDto.Format.ToUpper() == "IMAGE")
                    {
                        component.Parameters.Add(new WSAImageMessageDto()
                        {
                            Type = "image",
                            Image = new MediaDto()
                            {
                                Id = metaId
                            }
                        });
                    }

                    if (wSATextHeaderComponentDto.Format.ToUpper() == "VIDEO")
                    {
                        component.Parameters.Add(new WSAVideoMessageDto()
                        {
                            Type = "video",
                            Video = new MediaDto()
                            {
                                Id = metaId
                            }
                        });
                    }

                    if (wSATextHeaderComponentDto.Format.ToUpper() == "DOCUMENT")
                    {
                        component.Parameters.Add(new WSADocumentMessageDto()
                        {
                            Type = "Document",
                            Document = new MediaDto()
                            {
                                Id = metaId
                            }
                        });
                    }

                    message.Template.Components.Add(component);
                }
            }

            return JsonConvert.SerializeObject(message);

        }

        public MarketingStatusEntity GetStatus(string code) {

            return _unitOfWork.MarketingStatusRepository.FirstOrDefault(x => x.Code == code);
        }

        private async Task<OAuthWhatsAppDto> GetMetaToken(MarketingCampaignEntity marketingCampaign) {
            var metaConfiguration = _unitOfWork.MetaConfigurationRepository.GetAll(x => x.MarketingEntities).First(x => x.MarketingEntities.Any(y => y.Id == marketingCampaign.MarketingId));
            ObjectId oauthId = ObjectId.Parse(metaConfiguration.OAuthId);
            AuthConfigEntity configurations = await _AuthConfigService.GetAuthConfigByIdAsync(oauthId);
            return configurations.Auth.ToObject<OAuthWhatsAppDto>();
        }

        private async Task<OAuthWhatsAppDto> GetMetaTokenBymarketingCampaignId(int marketingCampaignId)
        {
            var marketingCampaign = _unitOfWork.MarketingCampaignRepository.FirstOrDefault(x => x.Id == marketingCampaignId);
            return await GetMetaToken(marketingCampaign);
        }



        #endregion
    }
}
