namespace RosaBot.Shared.Messages
{
    public static class BotMessages
    {
        public static string ErrorMessage()
            => "O comando digitado não foi encontrado, por favor tente novamente.\nPara visualizar todos os comandos digite @}ajuda.";

        public static string BotActivityMessage()
            => "Meu cabelo cair!";

        public static string BotTwitchUrlMessage()
            => "twitch.tv/ImperialismoHeroEuropeu";

        public static string WelcomeMessage(string username)
            => string.Format("Eai seu filho da puta: {0}, Bem vindo nesse caralho fodido, aqui só tem corno e comunista.\nTome cuidado com Rafael Augusto Rosa de Brito!", username);

        public static string BotReadyMessage(string currentUser)
            => string.Format("{0} está ativo e operante!", currentUser);

        public static string QuotationInvalidMessage()
            => "Use @}cotaçao <moeda>";

        public static string QuotationResultMessage(string currencyName, double quotation, string apiUrl, DateTime date)
            => string.Format("[{0}]\nA cotação dessa moeda infeliz está: R${1:0.00}\nFonte: {2}\nCotação do dia: {3}",
                currencyName.ToUpper(),
                quotation,
                apiUrl,
                date.ToString("dd/MM/yyyy hh:mm:ss") + " - Horário de Brasília");
    }
}
