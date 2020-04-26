using PiController.Features.SystemServices;

namespace PiController.ShellCommands
{
    public class CommandFactory
    {
        public string CreateServiceStatusCommand(string systemService, ServiceState serviceState)
        {
            return $"sudo /bin/systemctl {serviceState} {systemService}.service";
        }

        public string CreateServiceStatusQueryCommand(string systemService)
        {
            return $"sudo /bin/systemctl is-active {systemService}.service";
        }
    }
}
