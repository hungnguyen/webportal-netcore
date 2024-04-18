using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebPortal.Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string from, string to, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", from, to, message);
        }
    }
}
