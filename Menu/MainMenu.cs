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
       
            var features = new List<IFeature>
            {
                _serviceManager
            };

            CreateMenu(features);
        }

        protected override void UseValidInput(IFeature feature)
        {
            feature.Start();
        }
    }
}
