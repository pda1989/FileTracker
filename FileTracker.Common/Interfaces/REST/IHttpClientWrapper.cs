using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FileTracker.Common.Interfaces.REST
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage input, CancellationToken token);
    }
}