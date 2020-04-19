using System;
using PiController.ShellCommands;

namespace PiController.Features.SystemServices
{
    public class SystemService
    {
        private readonly string _name;
        private readonly CommandFactory _commandFactory;

        public string GetName()
        {
            return _name;
        }

        public SystemService(string name, CommandFactory commandFactory)
        {
            _name = name;
            _commandFactory = commandFactory;
        }

        public void Use()
        {
            throw new NotImplementedException();
        }
    }

    public enum ServiceState
    {
        start,
        stop,
        restart
    }
}
