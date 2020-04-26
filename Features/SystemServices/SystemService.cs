using System.Linq;
using PiController.Menu;
using PiController.ShellCommands;
using PiController.SSH;

namespace PiController.Features.SystemServices
{
    public class SystemService : IMenuItem
    {
        private readonly Client _client;
        private readonly CommandFactory _commandFactory;
        private readonly string _name;
        private readonly string _displayName;
        public string Status = "Unknown";

        public string GetName()
        {
            return $"{_displayName} ({Status})";
        }

        public SystemService(string name, CommandFactory commandFactory, Client client)
        {
            _name = name;
            _displayName = name.Split('.').Last();
            _commandFactory = commandFactory;
            _client = client;
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            var cmd = _commandFactory.CreateServiceStatusQueryCommand(_name); 
            var result = _client.DispatchCommand(cmd);
            Status = result.Result.Substring(0,result.Result.Length -1);
        }

        public void ChangeStatus(ServiceState serviceState)
        {
            var cmd = _commandFactory.CreateServiceStatusCommand(_name, serviceState);
            _client.DispatchCommand(cmd);
        }
    }
}
