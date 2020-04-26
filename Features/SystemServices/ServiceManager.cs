using System;
using System.Collections.Generic;
using System.Linq;
using PiController.ShellCommands;
using PiController.SSH;
using PiController.Menu;
using PiController.Settings;

namespace PiController.Features.SystemServices
{
    public class ServiceManager : MenuBase<Tuple<string,string>>, IFeature
    {
        protected override String GetMenuName() => "System Services";
        private readonly CommandFactory _commandFactory;
        private readonly Client _client;
        private readonly StatusOptionMenu _statusOptionMenu;
        private readonly Dictionary<int, ServiceState> _serviceOptions = new Dictionary<int, ServiceState>
            { { 1, ServiceState.start }, { 2, ServiceState.stop }, { 3, ServiceState.restart } };

        public ServiceManager(AppSettings appSettings, CommandFactory commandFactory, Client client, StatusOptionMenu statusOptionMenu)
        {
            _commandFactory = commandFactory;
            _client = client;
            _statusOptionMenu = statusOptionMenu;
            var menuOptions =  GetStatuses(appSettings.SystemServices);
            CreateMenu(menuOptions.ToList());
        }

        protected override void UseValidInput(Tuple<string,string> systemService)
        {
            _statusOptionMenu.DisplayOptions();
            var serviceAction = _statusOptionMenu.GetInput();
            var cmd = _commandFactory.CreateServiceStatusCommand(systemService.Item1, serviceAction);
            _client.DispatchCommand(cmd);
        }

        private IEnumerable<Tuple<string,string>> GetStatuses(IList<string> systemServices)
        {
            foreach (var service in systemServices)
            {
                var statusCmd = _commandFactory.CreateServiceStatusQueryCommand(service);
                var result = _client.DispatchCommand(statusCmd);
                yield return new Tuple<string, string>(service, result.Result.Substring(0,result.Result.Length -1));
            }
        }

        public void Start()
        {
            this.DefaultStart();
        }
    }
}
