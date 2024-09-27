using Microsoft.AspNetCore.SignalR;

namespace GMS.API.Hubs
{
    public class GymHub : Hub
    {
        public async Task NotifyEntityUpdated(string entityName, string entityId)
        {
            await Clients.All.SendAsync($"{entityName}Updated", entityId);
        }

        public async Task NotifyEntityDeleted(string entityName, string entityId)
        {
            await Clients.All.SendAsync($"{entityName}Deleted", entityId);
        }

        public async Task NotifyEntityAdded(string entityName, string entityId)
        {
            await Clients.All.SendAsync($"{entityName}Added", entityId);
        }
    }

}
