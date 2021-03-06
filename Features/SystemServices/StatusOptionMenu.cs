using System;
using System.Collections.Generic;
using PiController.Menu;

namespace PiController.Features.SystemServices
{
    public class StatusOptionMenu : MenuBase<ServiceState>
    {
        protected override string GetMenuName() => "Service Status";

        public StatusOptionMenu()
        {
            CreateMenu(new List<ServiceState> { ServiceState.start, ServiceState.stop, ServiceState.restart });   
        }

        protected override void UseValidInput(ServiceState input)
        {
            // This is deliberately left unused.
            // ServiceManager uses the result.
            throw new NotImplementedException();
        }
    }
}