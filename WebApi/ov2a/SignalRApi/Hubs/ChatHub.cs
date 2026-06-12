using System;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi;

public class ChatHub : Hub
{

	public override async Task OnConnectedAsync()
	{
		Console.WriteLine($"New connection: {Context.ConnectionId}");
		await base.OnConnectedAsync();
	}

	public async Task SendMessage(string msg)
	{
		await Clients.All.SendAsync("ReceiveMessage", msg);
	}

}
