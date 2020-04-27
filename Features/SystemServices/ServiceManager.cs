using System.Collections.Generic;
using System.Linq;
using PiController.ShellCommands;
using PiController.SSH;
using PiController.Menu;
using PiController.Settings;

namespace PiController.Features.SystemServices
{
    public class ServiceManager : MenuBase<SystemService>, IFeature, IMenuItem
    {
        private const string _name = "System Services";
        public override string GetName() => _name;
        private readonly CommandFactory _commandFactory;
        private readonly Client _client;
        private readonly StatusOptionMenu _statusOptionMenu;
        private readonly IList<string> _systemServices;

        public ServiceManager(AppSettings appSettings, CommandFactory commandFactory, Client client, StatusOptionMenu statusOptionMenu)
        {
            _commandFactory = commandFactory;
            _client = client;
            _statusOptionMenu = statusOptionMenu;
            _systemServices = appSettings.SystemServices;
            _rawMenuOptions = InitialiseServices().ToList();
        }

        private IEnumerable<SystemService> InitialiseServices()
        {
            foreach (var service in _systemServices)
            {
                yield return new SystemService(service, _commandFactory, _client);
            } 
        }

        protected override void UseValidInput(SystemService systemService)
        {
            _statusOptionMenu.DefaultStart();
            var serviceAction = _statusOptionMenu.Output;
            systemService.ChangeStatus(serviceAction);
        }

        public void Start()
        {
            this.DefaultStart();
        }
    }
}
