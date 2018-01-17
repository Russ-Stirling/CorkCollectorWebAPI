using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Utility
{
    class DisplayMenu
    {
        public DisplayMenu()
        {
            
        }

        public bool run(string rootDir)
        {
            bool done = false;
            string subMenu = GetMainMenu();
            string operation = string.Empty;
            switch (subMenu)
            {
                case "1":
                    operation = GetBatchDataMenu();
                    break;
            }
            switch (string.Format("{0}-{1}", subMenu, operation))
            {
                case "1-1":
                    break;
                case "2-1":
                    break;
                case "2-2":
                    //FileOrganizer.
                    break;
                default:
                    done = CheckIfDone();
                    break;
            }
            return done;
        }

        private bool CheckIfDone()
        {
            string[] options = new string[2] { "1", "2" };
            bool correctInput = false;
            string choice = null;

            while (!correctInput)
            {
                Console.WriteLine("1. Return to Main Menu");
                Console.WriteLine("2. Exit Application");

                choice = Console.ReadLine();

                if (options.Contains(choice))
                    correctInput = true;
                else
                    Console.WriteLine("Invalid input. Please select one of the menu options.");
            }

            return choice == "1" ? true : false;

        }

        private string GetMainMenu()
        {
            string[] options = new string[1] { "1" };
            bool correctInput = false;
            string choice = null;

            while (!correctInput)
            {
                Console.WriteLine("1. Batch Data Operations");

                choice = Console.ReadLine();

                if (options.Contains(choice))
                    correctInput = true;
                else
                    Console.WriteLine("Invalid input. Please select one of the menu options.");
            }

            return choice;
        }

        private string GetBatchDataMenu()
        {
            return string.Empty;
        }
    }
}
