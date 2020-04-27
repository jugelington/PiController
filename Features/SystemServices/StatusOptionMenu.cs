using System.Collections.Generic;
using PiController.Menu;

namespace PiController.Features.SystemServices
{
    public class StatusOptionMenu : MenuBase<ServiceState>
    {
        public override string GetName() => "Service Status";
        public ServiceState Output = null;

        public StatusOptionMenu()
        {
            _rawMenuOptions = 
                new List<ServiceState> 
                {
                    new ServiceState("start"),
                    new ServiceState("stop"),
                    new ServiceState("restart")
                };   
        }

        protected override void UseValidInput(ServiceState input)
        {
            Output = input;
        }
    }
}
