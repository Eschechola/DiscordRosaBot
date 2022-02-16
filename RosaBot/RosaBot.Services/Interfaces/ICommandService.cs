namespace RosaBot.Services.Interfaces
{
    public interface ICommandService
    {
        Task<string> GetCommandResponseAsync(string command, string parammeter);
    }
}
