using FileTracker.Common.Interfaces.REST;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FileTracker.Common.Implementations.REST
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage input, CancellationToken token)
        {
            var response = await _httpClient.SendAsync(input, token);

            return response;
        }
    }
}