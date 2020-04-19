using PiController.SSH;

namespace PiController.ShellCommands
{
    public class CommandHandler
    {
        private readonly Client _client;

        public CommandHandler(Client client)
        {
            _client = client;
        }
        public void Handle(Command cmd)
        {
            _client.DispatchCommand(cmd);
        }
    }
}