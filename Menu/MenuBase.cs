using System;
using System.Collections.Generic;
using System.Linq;

namespace PiController.Menu
{
    public abstract class MenuBase<T> : IMenuItem where T :IMenuItem
    {
        protected IDictionary<int, T> _menuOptions;
        public abstract string GetName();

        public void DefaultStart()
        {
            DisplayOptions();
            var input = GetInput();
            UseValidInput(input);
        }

        public void DisplayOptions()
        {
            Console.WriteLine(GetName());

            foreach (var option in _menuOptions)
            {
                Console.WriteLine($"{option.Key}) {option.Value.GetName()}");
            }
        }

        public T GetInput()
        {
            Console.WriteLine("Enter selection:");
            var rawInput = Console.ReadLine();

            int parsedInput;
            
            if(int.TryParse(rawInput, out parsedInput)
                    && _menuOptions.ContainsKey(parsedInput))
            {
                return _menuOptions[parsedInput];
            }
            else
            {
                Console.WriteLine("Invalid input. Try again.");
                return GetInput();
            }
        }

        protected abstract void UseValidInput(T input);

        protected void CreateMenu(IList<T> menuOptions)
        {
            int i = 1;
            _menuOptions = menuOptions.ToDictionary(x => i++);
        }
    }
}
