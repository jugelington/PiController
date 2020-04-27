using System.Collections.Generic;
using PiController.Features;
using PiController.Features.SystemServices;

namespace PiController.Menu
{
    public class MainMenu : MenuBase<IFeature>
    {
        private readonly ServiceManager _serviceManager;
        public override string GetName() => "Main Menu";

        public MainMenu(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager; 
            _rawMenuOptions =
                new List<IFeature>
                {
                    _serviceManager
                };
        }

        protected override void UseValidInput(IFeature feature)
        {
            feature.Start();
        }
    }
}
