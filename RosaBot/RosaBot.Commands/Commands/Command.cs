using RosaBot.Commands.Interfaces.Commands;
using System.Threading.Tasks;

namespace RosaBot.Commands
{
    public abstract class Command : ICommand
    {
        public abstract Task<string> ResultAsync(string commandValue);
    }
}
