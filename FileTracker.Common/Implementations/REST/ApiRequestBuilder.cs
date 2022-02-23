using FileTracker.Common.Interfaces.REST;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FileTracker.Common.Implementations.REST
{
    public class ApiRequestBuilder : IRequestBuilder
    {
        private bool _isDisposed;
        private HttpRequestMessage _req;

        public IRequestBuilder AddFileContent(string fileName, HttpContent content)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new InvalidOperationException("File name cannot be empty");

            if (_req.Content == null)
                _req.Content = new MultipartFormDataContent();

            if (!(_req.Content is MultipartFormDataContent))
                throw new InvalidOperationException("Request content doesn't allow to add file content");

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            content.Headers.ContentDisposition.Name = "\"" + fileName + "\"";
            content.Headers.ContentDisposition.FileName = "\"" + fileName + "\"";
            content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            (_req.Content as MultipartFormDataContent).Add(content, fileName, fileName);

            return this;
        }

        public IRequestBuilder AddJsonContent(HttpContent content)
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _req.Content = content;

            return this;
        }

        public HttpRequestMessage Build()
        {
            return _req;
        }

        public IRequestBuilder CreateRequest(HttpMethod method, string url)
        {
            _req = CreateRequest(url, method);

            return this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _req?.Dispose();
                    _req = null;
                }

                _isDisposed = true;
            }
        }

        private HttpRequestMessage CreateRequest(string url, HttpMethod method)
        {
            var uriBuilder = new UriBuilder(url);

            var req = new HttpRequestMessage { RequestUri = uriBuilder.Uri, Method = method };

            req.Headers.Accept.Clear();
            req.Headers.Add("User-Agent", ".NET Client");

            return req;
        }
    }
}