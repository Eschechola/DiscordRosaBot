using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RosaBot.Application.Factory;
using RosaBot.Commands.Styles;
using RosaBot.IoC.Provider;
using RosaBot.Shared.Messages;
using System;
using System.Threading.Tasks;

namespace RosaBot.Application.Events
{
    public class BotEvents
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public BotEvents(DiscordSocketClient client)
        {
            var serviceProvider = ServicesProvider.GetServiceProvider();

            _client = client;
            _configuration = serviceProvider.GetService<IConfiguration>();
            _logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<BotEvents>();
        }

        public async Task StartAsync()
        {
            try
            {
                _logger.LogInformation("Login into discord server...");
                await _client.LoginAsync(TokenType.Bot, _configuration["Bot:Token"]);

                _logger.LogInformation("Login successful!");
                _logger.LogInformation("Starting discord servers connection.");

                await _client.StartAsync();                
                await _client.SetStatusAsync(UserStatus.Online);
                await _client.SetGameAsync(BotMessages.BotActivityMessage(), BotMessages.BotTwitchUrlMessage(), ActivityType.Watching);

                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public Task LogAsync(LogMessage log)
        {
            _logger.LogWarning(log.Message);
            return Task.CompletedTask;
        }

        public async Task ReadyAsync()
        {
            _logger.LogInformation($"{_client.CurrentUser.ToString()} is connected!");
            var readyChannelId = ulong.Parse(_configuration["Server:Channels:Ready"]);
            var message = BotMessages.BotReadyMessage(_client.CurrentUser.ToString());

            await SendMessageToChannel(readyChannelId, message);
        }

        public async Task JoinedUser(SocketGuildUser user)
        {
            _logger.LogInformation($"{user.Mention} joined into server!");

            var message = BotMessages.WelcomeMessage(user.Mention);
            var welcomeChannelId = ulong.Parse(_configuration["Server:Channels:Welcome"]);

            await SendMessageToChannel(welcomeChannelId, message);
        }

        public async Task MessageReceivedAsync(SocketMessage message)
        {
            try
            {
                if (!ValidateSocketMessage(message))
                    return;

                _logger.LogInformation("Message" + message.Content + "\nReceived from " + message.Author + "\nIn channel " + message.Channel);
                var commandValue = GetCommandFromSocketMessage(message);
                var commandParammeter = GetParammeterFromSocketMessage(message);
                var commandObject = BotCommandFactory.GetCommand(commandValue);

                _logger.LogInformation("Executing command: " + commandObject.ToString());
                string commandResult = await commandObject.ResultAsync(commandParammeter);

                _logger.LogInformation("Sending message to server with command result\n" + commandResult);
                await message.Channel.SendMessageAsync(TextStyle.BlockedText(commandResult));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await message.Channel.SendMessageAsync(BotMessages.ErrorMessage());
            }
        }

        private async Task SendMessageToChannel(ulong channelId, string message)
        {
            var channel = _client.GetChannel(channelId) as SocketTextChannel;
            await channel.SendMessageAsync(message);
        }

        private bool ValidateSocketMessage(SocketMessage socketMessage)
        {
            if (socketMessage.Content.Length < 2)
                return false;

            string botScapeCharacter = socketMessage.Content.Substring(0, 2);

            if (botScapeCharacter != "@}")
                return false;

            return true;
        }

        private string[] GetCommandsFromSocketMessage(SocketMessage socketMessage)
        {
            return socketMessage.Content
                    .Replace("@}", "")
                    .ToLower()
                    .TrimStart()
                    .TrimEnd()
                    .Split(' ');
        }

        private string GetCommandFromSocketMessage(SocketMessage socketMessage)
        {
            var commandList = GetCommandsFromSocketMessage(socketMessage); 
            return commandList[0];
        }

        private string GetParammeterFromSocketMessage(SocketMessage socketMessage)
        {
            var commandList = GetCommandsFromSocketMessage(socketMessage);
            
            return commandList.Length > 1
                ? commandList[1].ToLower()
                : string.Empty;
        }
    }
}
