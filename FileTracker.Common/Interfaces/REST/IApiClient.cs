using FileTracker.Common.Implementations.REST;
using System.Threading.Tasks;

namespace FileTracker.Common.Interfaces.REST
{
    public interface IApiClient
    {
        Task<ApiResponse> PostAsync<I>(string url, I input, int timeoutMiliseconds = 0);
    }
}