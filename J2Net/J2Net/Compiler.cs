using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2Net.Grammar;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.IO;

namespace J2Net
{
    public static class Compiler
    {
        public static Boolean Compile(StreamReader javaCode)
        {
            //ref: https://theantlrguy.atlassian.net/wiki/display/ANTLR4/Parse+Tree+Listeners
            JavaLexer lexer = new JavaLexer(new AntlrInputStream(javaCode));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            JavaParser parser = new JavaParser(tokens);
            ParserRuleContext tree = parser.compilationUnit();

            ParseTreeWalker walker = new ParseTreeWalker(); 
            //J2NetCILGenerator listener = new J2NetCILGenerator(parser, "test"); //attach our listener to build CIL
            
            //listener.Start();
            //walker.Walk(listener, tree); //initiate walk of tree with listener
            
            //listener.End();
            
            //TODO: output parsed code to text-based CIL (*.il) file.

            // Test stringbuilder
            //Console.WriteLine(listener.printCIL());

            J2NetCILVisitor visitor = new J2NetCILVisitor();
            Console.WriteLine(visitor.Visit(tree));
            
            return true;
        }


    }
}
