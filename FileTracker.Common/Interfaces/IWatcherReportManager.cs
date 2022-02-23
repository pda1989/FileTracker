using FileTracker.Common.Models;
using System.Threading.Tasks;

namespace FileTracker.Common.Interfaces
{
    public interface IWatcherReportManager
    {
        Task SendReportAsync(Report report);
    }
}