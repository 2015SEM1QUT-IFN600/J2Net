using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileName;
            if (args.Count() > 0)
            {
                fileName = Path.GetFullPath(".\\JavaCodeTestFiles\\CLSFractal.java");
            }
            else
            {
                fileName = Path.GetFullPath(".\\JavaCodeTestFiles\\HelloIFN660.java"); // default to hello world file
            }

            //Check to see if file exist
            if (!File.Exists(fileName))
            {
                Console.WriteLine(fileName + " can't be found. Program terminated.");
                Terminate();
            }

            //Read file
            StreamReader inputStream = new StreamReader(fileName);

            //Compile
            StringBuilder compiledCode = J2Net.Compiler.Compile(inputStream);
            if (compiledCode != null)
            {
                Console.WriteLine("Compilation Successful\n\n");
            }
            else
            {
                Console.WriteLine("Compilation Failed, see debug output\n\n");
                Terminate();
            }

            //Write IL file
            string ilFileName = "test.il";
            using (StreamWriter outfile = new StreamWriter(ilFileName))
            {
                outfile.Write(compiledCode.ToString());
            }
            Console.WriteLine(compiledCode.ToString() + "\n\n"); // helpful display of IL output to console

            //Generate EXE from IL
            generateExecutionFile(ilFileName);

            //end
            Terminate();
        }

        private static void Terminate()
        {
            Console.WriteLine("\n\nPress any key to end the program");
            Console.ReadKey();
            Environment.Exit(0);
        }

        //Generate executable file from CIL code
        //Note: Before using this function, please leaving ilasm.exe, fusion.dll with this program in the same folder.
        private static void generateExecutionFile(string fileName)
        {
            string converterName = "ilasm.exe";
            string cmdArgument = "/EXE";

            try
            {
                //System.Diagnostics.Process.Start(String.Format("{0} {1} {2}", converterName, cmdArgument, fileName));
                string strCmdText;
                strCmdText = "/C ilasm test.il";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred: {0}\nPlease check the format of il file is correct.", ex.Message);
            }
        }
    }
}
