using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Antlr4.Runtime;
using J2Net.Grammar;

namespace J2Net.Tests
{
    [TestClass]
    public class ParserTests
    {
        private static void AssertRule(String javaCode)
        {
            AntlrInputStream antlrStream = new AntlrInputStream(javaCode);
            JavaLexer lexer = new JavaLexer(antlrStream);
            CommonTokenStream token = new CommonTokenStream(lexer);

            JavaParser parser = new JavaParser(token);

            // Option 1: implement IJavaListner, then build a test for each override
            //http://stackoverflow.com/questions/15050137/once-grammar-is-complete-whats-the-best-way-to-walk-an-antlr-v4-tree

            // Option 2: throw errors from inside the rule, and catch in test
            // http://meri-stuff.blogspot.com.au/2011/08/antlr-tutorial-hello-word.html#ChangingCatchinParser
            //   another example of same technique, but implemented differently
            // http://meri-stuff.blogspot.com.au/2011/08/antlr-tutorial-hello-word.html#ChangingCatchinParser

            
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
