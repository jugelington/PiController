using System;
using System.Collections.Generic;
using System.Linq;

namespace PiController.Menu
{
    public abstract class MenuBase<T>
    {
        protected IDictionary<int, T> _menuOptions;
        protected abstract String GetMenuName();

        public void DefaultStart()
        {
            DisplayOptions();
            var input = GetInput();
            UseValidInput(input);
        }
        public void DisplayOptions()
        {
            Console.WriteLine(GetMenuName());

            foreach (var option in _menuOptions)
            {
                Console.WriteLine($"{option.Key}) {option.Value}");
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
