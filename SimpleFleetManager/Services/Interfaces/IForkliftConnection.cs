using SimpleFleetManager.Models.Main;

namespace SimpleFleetManager.Services.Interfaces
{
    public interface IForkliftConnection
    {
        Task<bool> Connect(Forklift forklift);
        Task HandleDataExchange(Forklift forklift);
        Task<bool> Disconnect(Forklift forklift);
        Task<bool> Reconnect(Forklift forklift, int retryInterval, int maxRetries);
    }
}
