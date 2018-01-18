using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CorkCollector.Data;
using Raven.Client.Documents;

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
