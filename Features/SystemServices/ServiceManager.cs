using System;
using System.Collections.Generic;
using PiController.ShellCommands;
using PiController.Menu;
using PiController.Settings;

namespace PiController.Features.SystemServices
{
    public class ServiceManager : MenuBase<string>, IFeature
    {
        protected override String GetMenuName() => "System Services";
        private readonly CommandFactory _commandFactory;
        private readonly CommandHandler _commandHandler;
        private readonly StatusOptionMenu _statusOptionMenu;
        private readonly Dictionary<int, ServiceState> _serviceOptions = new Dictionary<int, ServiceState>
            { { 1, ServiceState.start }, { 2, ServiceState.stop }, { 3, ServiceState.restart } };

        public ServiceManager(AppSettings appSettings, CommandFactory commandFactory, CommandHandler commandHandler, StatusOptionMenu statusOptionMenu)
        {
            _commandFactory = commandFactory;
            _commandHandler = commandHandler;
            _statusOptionMenu = statusOptionMenu;
            CreateMenu(appSettings.SystemServices);
        }

        protected override void UseValidInput(string systemService)
        {
            _statusOptionMenu.DisplayOptions();
            var serviceAction = _statusOptionMenu.GetInput();
            var cmd = _commandFactory.CreateServiceCommand(systemService, serviceAction);
            _commandHandler.Handle(cmd);
        }

        public void Start()
        {
           this.DefaultStart(); 
        }
    }
}