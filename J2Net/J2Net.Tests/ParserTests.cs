using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Tree.Pattern;
using J2Net.Grammar;

namespace J2Net.Tests
{
    [TestClass]
    public class ParserTests
    {
        static JavaLexer _Lexer;
        static JavaParser _Parser;

        private static ParseTreeMatch GetParseTreeMatch(String testCode, String pattern, String ruleName)
        {
            //Generate a lexer and parser based on test code
            JavaLexer lexer = new JavaLexer(new AntlrInputStream(testCode));
            JavaParser parser = new JavaParser(new CommonTokenStream(lexer));

            //use .Net Reflection to get the Paser's Rule's method and execute it to generate a Parse Tree object
            System.Reflection.MethodInfo ruleMethod = parser.GetType().GetMethod(ruleName);
            IParseTree ruleTree = (IParseTree)ruleMethod.Invoke(parser, null);

            //From our empty Lexer and Parser, compile a ParseTreePattern 
            ParseTreePattern treePattern = GetParseTreePattern(pattern, ruleName);

            //Pass the Rule's tree into the tree Pattern to get any match
            return treePattern.Match(ruleTree);
        }

        private static ParseTreePattern GetParseTreePattern(String pattern, String ruleName)
        {
            if (_Lexer == null)
            {
                _Lexer = new JavaLexer(null);
                _Parser = new JavaParser(new CommonTokenStream(_Lexer));
            }
            return _Parser.CompileParseTreePattern(pattern, _Parser.GetRuleIndex(ruleName));
        }


        [TestMethod]
        public void Parser_Types_type()
        {
            ParseTreeMatch match;
            match = GetParseTreeMatch("int", "<a:integralType>", "type");
            Assert.AreEqual(match.Get("a").GetText(), "int");

            //this is obviously a wrong test, because typeName rule is broken at this time. But this gives an 
            // example of using multiple Pattern tags and going through Parser rules
            match = GetParseTreeMatch("@. long", "<b:annotation> <a:integralType>", "type");
            Assert.AreEqual(match.Get("a").GetText(), "long");
            Assert.AreEqual(match.Get("b").GetText(), "@.");

        }
    }
}
