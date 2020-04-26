using PiController.Menu;

namespace PiController.Features
{
    public interface IFeature : IMenuItem
    {
       void Start(); 
    }
}
