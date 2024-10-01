using Common.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpCompress.Common;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.Common
{
    public class ExternalRequestClient(AuthorizationServiceFactory authorizationServiceFactory, ILogger<ExternalRequestClient> logger) : IExternalRequestClient
    {
        #region Requests

        public async Task<TResponse> GetRequest<TResponse, TOAuthType>(string url, string apiEndpoint, TOAuthType token = default)
        {
            var uri = new Uri(url);
            var baseUrl = uri.GetLeftPart(UriPartial.Authority);
            var pathAndQuery = uri.PathAndQuery;
            var endPoint = string.IsNullOrEmpty(pathAndQuery.Trim()) ? apiEndpoint : $"{pathAndQuery}{apiEndpoint}";
            try
            {
                using var handler = new HttpClientHandler();
                using var client = new HttpClient(handler);

                client.BaseAddress = new Uri($"{baseUrl}/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                logger.LogInformation($"Requesting external service: {url}{endPoint}");

                if (token != null)
                {
                    var authorizationService = authorizationServiceFactory.Create(token);
                    var authentication = authorizationService.Authorize();
                    client.DefaultRequestHeaders.Authorization = authentication;
                }

                var response = await client.GetAsync(endPoint.Replace("//", "/"));

                var jsonResult = await response.Content.ReadAsStringAsync();

                logger.LogInformation($"Response from external service: {jsonResult}");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<TResponse>(jsonResult);
                        return result;
                    }
                    catch (JsonSerializationException jsonEx)
                    {
                        logger.LogError(jsonEx, $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                        throw new ExternalRequestException(
                            $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                    }
                }
            }
            catch (HttpRequestException e)
            {
                logger.LogError(e, $"Connection error: {e.Message}");
                throw new ExternalRequestException($"Connection error: {e.Message}");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw new ExternalRequestException(e.Message);
            }

            return default;
        }



        public async Task<TResponse> PostRequest<TRequest, TResponse, TOAuthType>(
            TRequest requestDto,
            string url,
            string apiEndpoint,
            TOAuthType token = default
            )
        {
            try
            {
                var uri = new Uri(url);
                var baseUrl = uri.GetLeftPart(UriPartial.Authority);
                var pathAndQuery = uri.PathAndQuery;
                var endPoint = string.IsNullOrEmpty(pathAndQuery.Trim()) || pathAndQuery.Equals("/")
                    ? apiEndpoint
                    : $"{pathAndQuery}{apiEndpoint}";

                string json = string.Empty;


                logger.LogInformation($"Requesting external service: {url}{endPoint}");


                if (typeof(TRequest) == typeof(string))
                {
                    json = requestDto.ToString() ?? "";
                }
                else {
                    json = JsonConvert.SerializeObject(requestDto);
                }
                
                

                logger.LogInformation($"Request body: {json}");


                using var handler = new HttpClientHandler();
                using var client = new HttpClient(handler);

                client.BaseAddress = new Uri($"{baseUrl}/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                if (token != null)
                {
                    var authorizationService = authorizationServiceFactory.Create(token);
                    var authentication = authorizationService.Authorize();
                    client.DefaultRequestHeaders.Authorization = authentication;
                }

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endPoint.Replace("//", "/"), stringContent);
                var jsonResult = await response.Content.ReadAsStringAsync();


                logger.LogInformation($"Response from external service: {jsonResult}");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<TResponse>(jsonResult);
                        return result ?? throw new ExternalRequestException("Response is null");
                    }
                    catch (JsonSerializationException jsonEx)
                    {
                        logger.LogError(jsonEx, $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                        throw new ExternalRequestException(
                            $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                    }
                }

                logger.LogError($"StatusCode {response.StatusCode} ReasonPhrase {response.ReasonPhrase} - Message: {jsonResult} - url: {url}{apiEndpoint}");
                throw new ExternalRequestException(
                    $"StatusCode {response.StatusCode} ReasonPhrase {response.ReasonPhrase} - Message: {jsonResult} - url: {url}{apiEndpoint}");
            }
            catch (HttpRequestException e)
            {
                logger.LogError(e, $"Connection error: {e.Message}");
                throw new ExternalRequestException($"Connection error: {e.Message}");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw new ExternalRequestException(e.Message);
            }
        }

        private async Task<TResponse> PostBinaryRequest<TResponse, TOAuthType>(
            string url, 
            IFormFile file, 
            MultipartFormDataContent multipartContent,
            Dictionary<string, string> headers, 
            TOAuthType token)
        {
            try
            {
                var uri = new Uri(url);
                var baseUrl = uri.GetLeftPart(UriPartial.Authority);
                var pathAndQuery = uri.PathAndQuery;

                logger.LogInformation($"Requesting external service: {url}");

                using var handler = new HttpClientHandler();
                using var client = new HttpClient(handler);


                var request = new HttpRequestMessage(HttpMethod.Post, uri);

                foreach (KeyValuePair<string, string> entry in headers)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }

                if (file is not null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        request.Content = new ByteArrayContent(memoryStream.ToArray());
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    }
                }
                else {
                    request.Content = multipartContent;
                }
                

                if (token != null)
                {
                    var authorizationService = authorizationServiceFactory.Create(token);
                    var authentication = authorizationService.Authorize();
                    request.Headers.Authorization = authentication;
                }

                var response = await client.SendAsync(request);

                logger.LogInformation($"Response from external service: {response}");

                var jsonResult = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"Response from external service: {jsonResult}");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<TResponse>(jsonResult);
                        return result ?? throw new ExternalRequestException("Response is null");
                    }
                    catch (JsonSerializationException jsonEx)
                    {
                        logger.LogError(jsonEx, $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                        throw new ExternalRequestException(
                            $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                    }
                }

                logger.LogError($"StatusCode {response.StatusCode} ReasonPhrase {response.ReasonPhrase} - Message: {jsonResult} - url: {url}");
                throw new ExternalRequestException(
                    $"StatusCode {response.StatusCode} ReasonPhrase {response.ReasonPhrase} - Message: {jsonResult} - url: {url}");

            }
            catch (HttpRequestException e)
            {
                logger.LogError(e, $"Connection error: {e.Message}");
                throw new ExternalRequestException($"Connection error: {e.Message}");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw new ExternalRequestException(e.Message);
            }
        }

        public async Task<TResponse> PatchRequest<TRequest, TResponse, TOAuthType>(TRequest requestDto, string url, string apiEndpoint, TOAuthType token = default)
        {
            try
            {
                var uri = new Uri(url);
                var baseUrl = uri.GetLeftPart(UriPartial.Authority);
                var pathAndQuery = uri.PathAndQuery;
                var endPoint = string.IsNullOrEmpty(pathAndQuery.Trim()) || pathAndQuery.Equals("/")
                    ? apiEndpoint
                    : $"{pathAndQuery}{apiEndpoint}";

                logger.LogInformation($"Requesting external service: {url}{endPoint}");

                var json = JsonConvert.SerializeObject(requestDto);

                logger.LogInformation($"Request body: {json}");


                using var handler = new HttpClientHandler();
                using var client = new HttpClient(handler);

                client.BaseAddress = new Uri($"{baseUrl}/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (token != null)
                {
                    var authorizationService = authorizationServiceFactory.Create(token);
                    var authentication = authorizationService.Authorize();
                    client.DefaultRequestHeaders.Authorization = authentication;
                }

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PatchAsync(endPoint.Replace("//", "/"), stringContent);
                logger.LogInformation($"Response from external service: {response}");

                var jsonResult = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"Response from external service: {jsonResult}");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<TResponse>(jsonResult);
                        return result ?? throw new ExternalRequestException("Response is null");
                    }
                    catch (JsonSerializationException jsonEx)
                    {
                        logger.LogError(jsonEx, $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                        throw new ExternalRequestException(
                            $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                    }
                }

                logger.LogError($"StatusCode {response.StatusCode} ReasonPhrase {response.ReasonPhrase} - Message: {jsonResult} - url: {url}{apiEndpoint}");
                throw new ExternalRequestException(
                    $"StatusCode {response.StatusCode} ReasonPhrase {response.ReasonPhrase} - Message: {jsonResult} - url: {url}{apiEndpoint}");


            }
            catch (HttpRequestException e)
            {
                logger.LogError(e, $"Connection error: {e.Message}");
                throw new ExternalRequestException($"Connection error: {e.Message}");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw new ExternalRequestException(e.Message);
            }

        }
        #endregion



        #region Methods

        public async Task<TResponse> Get<TResponse>(string url, string apiEndpoint)
        {
            return await GetRequest<TResponse, object>(url, apiEndpoint, default);
        }

        public async Task<TResponse> Get<TResponse, TOAuthType>(string url, string apiEndpoint, TOAuthType token)
        {
            return await GetRequest<TResponse, TOAuthType>(url, apiEndpoint, token);
        }

        public async Task<TResponse> Post<TRequest, TResponse>(TRequest requestDto, string url, string apiEndpoint)
        {
            return await PostRequest<TRequest, TResponse, object>(requestDto, url, apiEndpoint, default);
        }

        public async Task<TResponse> Post<TRequest, TResponse, TOAuthType>(TRequest requestDto, string url, string apiEndpoint, TOAuthType token)
        {
            return await PostRequest<TRequest, TResponse, TOAuthType>(requestDto, url, apiEndpoint, token);
        }

        public async Task<TResponse> Post<TResponse, TOAuthType>(IFormFile media, string url, Dictionary<string, string> headers, TOAuthType token)
        {
            return await PostBinaryRequest<TResponse, TOAuthType>(url, media, null, headers, token);
        }

        public async Task<TResponse> Post<TResponse, TOAuthType>(MultipartFormDataContent multipartContent, string url, Dictionary<string, string> headers, TOAuthType token)
        {
            return await PostBinaryRequest<TResponse, TOAuthType>(url, null, multipartContent, headers, token);
        }

        public async Task<TResponse> Patch<TRequest, TResponse, TOAuthType>(TRequest requestDto, string url, string apiEndpoint)
        {
            return await PatchRequest<TRequest, TResponse, object>(requestDto, url, apiEndpoint, default);
        }

        public async Task<TResponse> Patch<TRequest, TResponse, TOAuthType>(TRequest requestDto, string url, string apiEndpoint, TOAuthType token)
        {
            return await PatchRequest<TRequest, TResponse, TOAuthType>(requestDto, url, apiEndpoint, token);
        }

        #endregion
    }


}
