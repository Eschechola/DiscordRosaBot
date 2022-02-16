using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using RosaBot.Application.Events;
using RosaBot.IoC.Provider;
using System;

namespace RosaBot
{
    internal class Program
    {
        private static readonly DiscordSocketClient _client;
        private static readonly BotEvents _events;

        static Program()
        {
            var serviceProvider = ServicesProvider.GetServiceProvider();

            _client = serviceProvider.GetService<DiscordSocketClient>();
            _events = new BotEvents(_client);

            _client.Log += _events.LogAsync;
            _client.Ready += _events.ReadyAsync;
            _client.UserJoined += _events.JoinedUser;
            _client.MessageReceived += _events.MessageReceivedAsync;
        }

        static void Main(string[] args)
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
