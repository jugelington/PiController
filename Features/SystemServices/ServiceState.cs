using PiController.Menu;

namespace PiController.Features.SystemServices
{
    public class ServiceState : IMenuItem
    {
        protected readonly string _state;
        public string GetName() => _state;

        public ServiceState(string state)
        {
            _state = state;
        }
    }
}
