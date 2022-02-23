using System;
using System.Net.Http;

namespace FileTracker.Common.Interfaces.REST
{
    public interface IRequestBuilder : IDisposable
    {
        IRequestBuilder AddFileContent(string fileName, HttpContent content);

        IRequestBuilder AddJsonContent(HttpContent content);

        HttpRequestMessage Build();

        IRequestBuilder CreateRequest(HttpMethod method, string url);
    }
}