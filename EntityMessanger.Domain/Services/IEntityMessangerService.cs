using EntityMessanger.Domain.Models;

namespace EntityMessanger.Domain.Services
{
    public interface IEntityMessangerService
    {
        event Action<Entity> EntityMessageReceived;
        bool IsConnected { get; }
        Task Connect();
        Task SendEntityMessage(Entity entity);
    }
}
