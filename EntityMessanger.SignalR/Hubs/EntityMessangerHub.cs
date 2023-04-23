using EntityMessanger.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace EntityMessanger.SignalR.Hubs
{
    public class EntityMessangerHub : Hub
    {
        public async Task SendEntityMessage(Entity entity)
        {
            await Clients.All.SendAsync("ReceiveEntityMessage", entity);
        }
    }
}
