using CommandLine;

namespace Noskito.Toolkit.Command
{
    [Verb("parse", HelpText = "Parse content of files into database")]
    public class ParseCommand
    {
        [Option('p', "path", Required = true, HelpText = "Path to folder used for parsing")]
        public string Path { get; set; }
    }
}