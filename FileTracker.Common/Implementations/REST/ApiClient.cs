using FileTracker.Common.Interfaces;
using FileTracker.Common.Interfaces.REST;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileTracker.Common.Implementations.REST
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly ILogger _logger;
        private readonly IRequestBuilder _requestBuilder;
        private readonly ISerializer _serializer;

        public ApiClient(
            ILogger logger,
            IHttpClientWrapper httpClientWrapper,
            ISerializer serializer,
            IRequestBuilder requestBuilder)
        {
            _logger = logger;
            _httpClientWrapper = httpClientWrapper;
            _serializer = serializer;
            _requestBuilder = requestBuilder;
        }

        public async Task<ApiResponse> PostAsync<I>(string url, I input, int timeoutMiliseconds = 5000)
        {
            _logger.Debug($"POST request. URL: '{url}'");

            var request = _requestBuilder
                .CreateRequest(HttpMethod.Post, url)
                .AddJsonContent(new StringContent(_serializer.Serialize(input), Encoding.UTF8, "application/json"))
                .Build();

            return await SendAsync(request, timeoutMiliseconds);
        }

        private async Task<ApiResponse> SendAsync(HttpRequestMessage httpRequest, int timeoutMiliseconds = 0)
        {
            try
            {
                if (httpRequest.Content != null)
                {
                    _logger.Debug($"  HTTP request content type: {httpRequest.Content.Headers.ContentType}");
                    _logger.Debug($"  HTTP request content length: {httpRequest.Content.Headers.ContentLength}");

                    if (httpRequest.Content.Headers.ContentType.MediaType == "application/json")
                    {
                        string content = await httpRequest.Content.ReadAsStringAsync();
                        if (content != null)
                            _logger.Debug($"  HTTP request message: {content}");
                    }
                }

                var timeSpan = TimeSpan.FromMilliseconds(timeoutMiliseconds);

                using (httpRequest)
                using (var tokenSource = new CancellationTokenSource(timeSpan))
                using (var httpResponse = await _httpClientWrapper.SendAsync(httpRequest, tokenSource.Token))
                {
                    _logger.Debug($"  HTTP response status: {(int)httpResponse.StatusCode}");

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string content = httpResponse.Content == null ?
                            null :
                            (await httpResponse.Content.ReadAsStringAsync());
                        return ApiResponse.Create(content);
                    }
                    else
                    {
                        return ApiResponse.Create(new HttpRequestException($"Status code: {(int)httpResponse.StatusCode}. {httpResponse.ReasonPhrase}"));
                    }
                }
            }
            catch (Exception exception)
            {
                return ApiResponse.Create(exception);
            }
        }
    }
}