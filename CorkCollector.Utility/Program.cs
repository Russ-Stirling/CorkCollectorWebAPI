

namespace CorkCollector.Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            if (!options.Headless)
            {
                //string path = System.IO.Directory.GetCurrentDirectory();
                var menu = new DisplayMenu();
                bool done = false;

                while (!done)
                {
                    done = menu.run();
                }
            }

        }
    }
}
