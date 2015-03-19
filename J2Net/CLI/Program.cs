using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2Net;
using J2Net.Grammar;
using System.IO;
using Antlr4.Runtime;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {

            //User enter file name
            Console.WriteLine("Please input full file name with extension.");
            String fileName = Console.ReadLine();

            //Check to see if file exist
            if (!File.Exists(fileName))
            {
                Console.WriteLine(fileName + " can't be found. Program terminated.");
                Console.WriteLine("Press any key to end the program");
                do
                {
                    if (Console.KeyAvailable)
                    {
                        Environment.Exit(0);
                    }
                } while (!Console.KeyAvailable);
            }


            //Antlr4 Build Pipeline
            //StreamReader inputStream = new StreamReader(Console.OpenStandardInput());   //stream reader to read input
            StreamReader inputStream = new StreamReader(fileName);                        //read file into stream
            AntlrInputStream input = new AntlrInputStream(inputStream.ReadToEnd());       //pass the string (read input) into an Antlr stream
            JavaLexer lexer = new JavaLexer(input);                                       //lexer is created with Antlr stream
            //CommonTokenStream tokens = new CommonTokenStream(lexer);                    //created lexer is saved in CommonToken

            //Reading in token by token instead of dealing with a large stream
            while (true)
            {
                CommonToken tokens = new CommonToken(lexer.NextToken());
                if (tokens.Type.Equals(JavaLexer.Eof))
                {
                    break;
                }
                Console.WriteLine(tokens.Type + " :: " + tokens.Text);

            }

            /*J2NetParser parser = new J2NetParser(tokens);                             //pass saved token into parser
            IParseTree tree = parser.prog();                                            //rules to run the parser (prog rule). Output of parser is saved in IParseTree
            Console.WriteLine(tree.ToStringTree(parser));
            J2NetVisitor visitor = new J2NetVisitor();                                  //IparseTree evaluation being done in Visitor
            Console.WriteLine(visitor.Visit(tree));*/
        }
    }
}
