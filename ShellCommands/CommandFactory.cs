using PiController.Features.SystemServices;

namespace PiController.ShellCommands
{
    public class CommandFactory
   {
       public Command CreateServiceCommand(string systemService, ServiceState serviceState)
       {
           var body = $"sudo systemctl {serviceState} {systemService}.service";
           return new Command(body);
       }
   }
}