using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace CorkCollector.Utility
{
    class Options
    {
        [Option('h', "headless", Default = false,
            HelpText = "Run application with parameters, without the menu")]
        public bool Headless { get; set; }

        //[Option('r', "read", Required = true,
        //HelpText = "Input file to be processed.")]
        //public string InputFile { get; set; }

        //[Option('v', "verbose", DefaultValue = true,
        //  HelpText = "Prints all messages to standard output.")]
        //public bool Verbose { get; set; }

    }
}
