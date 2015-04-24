﻿using System;
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
            J2NetListener extractor = new J2NetListener(parser); //attach our listener to build CIL
            walker.Walk(extractor, tree); //initiate walk of tree with listener
            
            //TODO: output parsed code to text-based CIL (*.il) file.
            
            return true;
        }


    }
}
