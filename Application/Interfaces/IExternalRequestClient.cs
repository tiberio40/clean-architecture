using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
    public interface IExternalRequestClient
    {
        Task<TResponse> Get<TResponse>(string url, string apiEndpoint);

        Task<TResponse> Get<TResponse, TOAuthType>(string url, string apiEndpoint, TOAuthType token);

        Task<TResponse> Post<TRequest, TResponse>(TRequest requestDto, string url, string apiEndpoint);

        Task<TResponse> Post<TRequest, TResponse, TOAuthType>(TRequest requestDto, string url, string apiEndpoint, TOAuthType token);

        Task<TResponse> Post<TResponse, TOAuthType>(IFormFile media, string url, Dictionary<string, string> headers, TOAuthType token);

        Task<TResponse> Post<TResponse, TOAuthType>(MultipartFormDataContent multipartContent, string url, Dictionary<string, string> headers, TOAuthType token);

        Task<TResponse> Patch<TRequest, TResponse, TOAuthType>(TRequest requestDto, string url, string apiEndpoint);

        Task<TResponse> Patch<TRequest, TResponse, TOAuthType>(TRequest requestDto, string url, string apiEndpoint, TOAuthType token);

    }
}
