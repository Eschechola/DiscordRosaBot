using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using ESCHENet.Configuration;
using Microsoft.Extensions.Configuration;
using RosaBot.Application.Factory;
using RosaBot.Commands;
using RosaBot.Commands.Styles;

namespace RosaBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program()
                .MainAsync()
                .GetAwaiter()
                .GetResult();
        }

        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _configuration;

        public Program()
        {
            _client = new DiscordSocketClient();
            _configuration = SettingsInjection.Configuration;

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
            _client.UserJoined += JoinedUser;
            //_discord.UserVoiceStateUpdated += OnVoiceStateUpdated;
        }

        // Função que é executada para poder rodar o bot
        // Verfica a veracidade do token no servidor do discord
        public async Task MainAsync()
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, _configuration["BotToken"]);
                await _client.StartAsync();
                await _client.SetStatusAsync(UserStatus.Online);
                await _client.SetGameAsync("meu cabelo cair", "", ActivityType.Watching);
                

                // Fecha o programa caso de algum erro de conexao
                await Task.Delay(-1);
            }
            catch (Exception)
            {}
        }

        // Caso de alguma execeção na execução do bot exibe no console
        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.Message.ToString());

            return Task.CompletedTask;
        }

        // Função chamada quando o bot está pronto para uso
        private Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");

            return Task.CompletedTask;
        }

        // Função chamada quando alguem entra no servidor
        public async Task JoinedUser(SocketGuildUser user)
        {
            //var message = $"Eai seu filho da puta: {user.Mention} bem vindo nesse caralho fodido, aqui só tem viado, corno e comunista. \nEu, RAFAEL AUGUSTO ROSA BRITO, dou meu CU, é isso mesmo que você leu VADIA, RAFAEL AUGUSTO ROSA BRITO DA O CU.";

            //var channelGeneral = _client.GetChannel(ulong.Parse(Configuration["Channels:Channel1"])) as SocketTextChannel;
            //var channelGeral = _client.GetChannel(ulong.Parse(Configuration["Channels:Channel2"])) as SocketTextChannel;
            //var channelBemVindo = _client.GetChannel(ulong.Parse(Configuration["Channels:Channel2"])) as SocketTextChannel;

            //await channelGeneral.SendMessageAsync(message);
            //await channelGeral.SendMessageAsync(message);
            //await channelBemVindo.SendMessageAsync(message);
        }

        // Função chamada quando alguém utiliza o "@}" para executar algum comando
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            try
            {
                if(message.Content.Length < 2)
                    return;

                //verifica se a mensagem contém o caractere de ativação do bot "@}"
                string botScapeCharacter = message.Content.Substring(0, 2);

                if (botScapeCharacter != "@}")
                    return;

                //comando que veio após o "@}, exemplo @}cotação -> cotação"
                string[] commandList = message.Content
                    .Replace("@}", "")
                    .ToLower()
                    .Split(' ');
                    
                string commandValue = commandList[0];

                //instância do objeto de comando
                Command commandObject = CommandFactory.GetCommand(commandValue);

                //parâmetro que veio após indicação do comando, exemplo @}cotação dolar -> dolar
                string commandParammeter = commandList.Length > 1 ?
                                           commandList[1].ToLower() :
                                           String.Empty;

                //mensagem formatada de acordo com o command
                string finalMessage = commandObject.ReturnMessage(commandParammeter);

                //executa o comando e envia a mensagem para o discord
                
                //ref RESULT TYPE -> enum
                //de acordo com o result type executa uma ação
                //ex: envio de mensagem ou entrar no discord por ex
                
                await message.Channel.SendMessageAsync(TextStyle.BlockedText(finalMessage));
            }
            catch (Exception)
            {
                await message.Channel.SendMessageAsync(_configuration["ErrorMessage"]);
            }
        }
    }
}
