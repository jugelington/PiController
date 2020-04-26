using System;
using System.Collections.Generic;
using PiController.Menu;

namespace PiController.Features.SystemServices
{
    public class StatusOptionMenu : MenuBase<ServiceState>
    {
        public override string GetName() => "Service Status";

        public StatusOptionMenu()
        {
            CreateMenu(new List<ServiceState> 
                    {
                        new ServiceState("start"),
                        new ServiceState("stop"),
                        new ServiceState("restart")
                    });   
        }

        protected override void UseValidInput(ServiceState input)
        {
            // This is deliberately left unused.
            // ServiceManager uses the result.
            throw new NotImplementedException();
        }
    }
}
