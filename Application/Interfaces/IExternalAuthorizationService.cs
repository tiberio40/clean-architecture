using System.Net.Http.Headers;

namespace Core.Interfaces
{
    public interface IExternalAuthorizationService
    {
       AuthenticationHeaderValue Authorize();
    }
}
