using EntityMessanger.Domain.Models;
using EntityMessanger.Domain.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace EntityMessanger.EntityAPI.Services
{
    public class SignalRMessangerService : IEntityMessangerService
    {
        private readonly HubConnection _connection;

        public event Action<Entity> EntityMessageReceived;

        public bool IsConnected => _connection.State == HubConnectionState.Connected;

        public SignalRMessangerService(HubConnection connection)
        {
            _connection = connection;
            _connection.On<Entity>("ReceiveEntityMessage", (entity) => EntityMessageReceived?.Invoke(entity));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendEntityMessage(Entity entity)
        {
            await _connection.SendAsync("SendEntityMessage", entity);
        }
    }
}
