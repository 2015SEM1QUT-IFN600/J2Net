using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2Net.Tests
{
    class TestingProgram
    {

        static void Main(string[] args)
        {

            Dictionary<CommandArgumentType, string> commands = CommandHelper.ParseCommandArgs(args);

            try
            {
                Compiler compiler = new Compiler(commands[CommandArgumentType.Input]);
                compiler.Compile(commands[CommandArgumentType.OutputDirectory]);

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}: {1}", MessagesHelper.UnhandledError, ex);
                Console.WriteLine("{0}", MessagesHelper.HelpUsage);
            }
        }

    }
}