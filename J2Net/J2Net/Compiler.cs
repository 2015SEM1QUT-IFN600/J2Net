using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2Net.Grammar;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.IO;
using System.Diagnostics;

namespace J2Net
{
    public static class Compiler
    {
        public static StringBuilder Compile(StreamReader javaCode)
        {

            // Antlr Lexer Pass
            JavaLexer lexer = new JavaLexer(new AntlrInputStream(javaCode));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            

            // Antlr Parser Pass
            JavaParser parser = new JavaParser(tokens);
            ParserRuleContext tree = parser.compilationUnit();


            // Semantic Anlaysis Pass
            ScopeStack scopeStack = new ScopeStack();
            J2NetNameBinder listener = new J2NetNameBinder(parser, scopeStack);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(listener, tree);
            

            // Generate IL Code Pass
            J2NetCILVisitor visitor = new J2NetCILVisitor(scopeStack);
            visitor.Visit(tree);


            // return IL Code
            return visitor.ToStringBuilder();
        }

        public static void junkCode()
        {
            ////////////////////////////////////////////////////////////////
            //Just a test - Ignore it
            JavaLexer lexer = new JavaLexer(new AntlrInputStream("2+3"));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            JavaParser parser = new JavaParser(tokens);
            IParseTree tree = parser.expression();
            Console.WriteLine(tree.ToStringTree(parser));
            MyVisitor visitor = new MyVisitor();
            Console.WriteLine(visitor.Visit(tree));
            ////////////////////////////////////////////////////////////////
        }


    }
}
