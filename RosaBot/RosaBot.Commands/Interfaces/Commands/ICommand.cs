using System.Threading.Tasks;

namespace RosaBot.Commands.Interfaces.Commands
{
    public interface ICommand
    {
        Task<string> ResultAsync(string commandValue);
    }
}
