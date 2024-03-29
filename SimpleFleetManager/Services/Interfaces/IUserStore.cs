using SimpleFleetManager.Models.Main;
namespace SimpleFleetManager.Services.Interfaces
{
    public interface IUserStore
    {
        User CurrentUser { get; set; }
        event Action? StateChanged;
    }
}
