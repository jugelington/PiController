using System.Collections.Generic;
using System.Linq;
using PiController.ShellCommands;
using PiController.SSH;
using PiController.Menu;
using PiController.Settings;

namespace PiController.Features.SystemServices
{
    public class ServiceManager : MenuBase<SystemService>, IFeature
    {
        private const string _name = "System Services";
        protected override string GetMenuName() => _name;
        public string GetName() => _name;
        private readonly CommandFactory _commandFactory;
        private readonly Client _client;
        private readonly StatusOptionMenu _statusOptionMenu;

        public ServiceManager(AppSettings appSettings, CommandFactory commandFactory, Client client, StatusOptionMenu statusOptionMenu)
        {
            _commandFactory = commandFactory;
            _client = client;
            _statusOptionMenu = statusOptionMenu;
            var menuOptions =  InitialiseServices(appSettings.SystemServices);
            CreateMenu(menuOptions.ToList());
        }

        private IEnumerable<SystemService> InitialiseServices(IList<string> systemServices)
        {
            foreach (var service in systemServices)
            {
                yield return new SystemService(service, _commandFactory, _client);
            } 
        }

        protected override void UseValidInput(SystemService systemService)
        {
            _statusOptionMenu.DisplayOptions();
            var serviceAction = _statusOptionMenu.GetInput();
            systemService.ChangeStatus(serviceAction);
        }

        public void Start()
        {
            this.DefaultStart();
        }
    }
}
