using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Data;
namespace SimpleFleetManager.Services.Interfaces
{
    public interface IForkliftConnection
    {
        Task<bool> Connect(Forklift forklift);
        Task HandleDataExchange(Forklift forklift, LogsDataService logDataService);
        Task<bool> Disconnect(Forklift forklift);
        Task<bool> Reconnect(Forklift forklift, LogsDataService logDataService, int retryInterval, int maxRetries);
    }
}
