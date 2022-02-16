using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using RosaBot.Application.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RosaBot.Application.Worker
{
    public class BotWorker : BackgroundService
    {
        private readonly DiscordSocketClient _client;
        private readonly BotEvents _events;

        public BotWorker(DiscordSocketClient client, BotEvents events)
        {
            _client = client;
            _events = events;

            _client.Log += _events.LogAsync;
            _client.Ready += _events.ReadyAsync;
            _client.UserJoined += _events.JoinedUser;
            _client.MessageReceived += _events.MessageReceivedAsync;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                try
                {
                    _events.StartAsync()
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}
