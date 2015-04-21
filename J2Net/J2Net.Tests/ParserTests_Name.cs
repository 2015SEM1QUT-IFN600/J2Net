using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Tree.Pattern;
using J2Net.Grammar;

namespace J2Net.Tests
{
    [TestClass]
    public class ParserTests_Name
    {
        static JavaLexer lexer;
        static JavaParser parser;


        private static ParseTreeMatch getParseTreeMatch(String testCode, String pattern, String ruleName)
        {
            JavaLexer javaLexer = new JavaLexer(new AntlrInputStream(testCode));
            JavaParser javaParser = new JavaParser(new CommonTokenStream(javaLexer));

            System.Reflection.MethodInfo ruleMethod = javaParser.GetType().GetMethod(ruleName);
            IParseTree ruleTree = (IParseTree)ruleMethod.Invoke(javaParser, null);

            ParseTreePattern treePattern = getParseTreePattern(pattern, ruleName);

            return treePattern.Match(ruleTree);
        }

        private static ParseTreePattern getParseTreePattern(String pattern, String ruleName)
        {
            if (lexer == null)
            {
                lexer = new JavaLexer(null);
                parser = new JavaParser(new CommonTokenStream(lexer));
            }

            return parser.CompileParseTreePattern(pattern, parser.GetRuleIndex(ruleName));
        }

        [TestMethod]
        public void Parser_Name_packageName()
        {
            ParseTreeMatch match;
            match = getParseTreeMatch("Stationary", "<Identifiers>", "packageName");
            Assert.IsTrue(match.Succeeded);
            Assert.AreEqual(match.Get("Identifiers").GetText(), "Stationary");
        
        }

        [TestMethod]
        public void Parser_Name_packageName_DOTIdentifier()
        {
            ParseTreeMatch match = getParseTreeMatch("Stationary.Pen", "<packageName>.<Identifiers>", "packageName");
            Assert.IsTrue(match.Succeeded);

            //Excpetion: Object reference not set to an instance of an object
            //Assert.AreEqual(match.Get("Identifiers.Identifiers").GetText(), "Stationary.Pen");
        }

        [TestMethod]
        public void Parser_Name_packageName_2Pack()
        {
            ParseTreeMatch match = getParseTreeMatch("Stationary.Pen.WithEraser", "<packageName>.<Identifiers>.<Identifiers>", "packageName");
            Assert.IsTrue(match.Succeeded);
            //Excpetion: Object reference not set to an instance of an object
            //Assert.AreEqual(match.Get("packageName.Identifiers.Identifiers").GetText(), "Stationary.Pen.WithEraser");
        }

        [TestMethod]
        public void Parser_Name_packageName_3Pack()
        {
            ParseTreeMatch match = getParseTreeMatch("Office.Stationary.Pen.WithEraser", "<packageName>.<Identifiers>", "packageName");
            Assert.IsTrue(match.Succeeded);
            //Excpetion: Object reference not set to an instance of an object
            //Assert.AreEqual(match.Get("packageName.Identifiers").GetText(), "Office.Stationary.Pen.WithEraser");
        }


        [TestMethod]
        public void Parser_Name_typeName()
        {
            ParseTreeMatch match = getParseTreeMatch("Integer", "<Identifiers>", "typeName");
            Assert.IsTrue(match.Succeeded);
            Assert.AreEqual(match.Get("Identifiers").GetText(), "Integer");
        }

        [TestMethod]
        public void Parser_Name_typeName_packageOrTypeName()
        {
            ParseTreeMatch match = getParseTreeMatch("Number.Integer", "<packageOrTypeName>.<Identifiers>", "typeName");
            Assert.IsTrue(match.Succeeded);

        //Exception object reference not set to an instance of an object
           //Assert.AreEqual(match.Get("packageOrTypeName.Identifiers").GetText(), "Number.Integer");
        }

        [TestMethod]
        public void Parser_Name_packageOrTypeName()
        {
            ParseTreeMatch match = getParseTreeMatch("Double", "<Identifiers>", "packageOrTypeName");
            Assert.IsTrue(match.Succeeded);
            //Exception object reference not set to an instance of an object
            Assert.AreEqual(match.Get("Identifiers").GetText(), "Double");
        }

        [TestMethod]
        public void Parser_Name_packageOrTypeName_Identifiers()
        {
            ParseTreeMatch match = getParseTreeMatch("Number.Double", "<Identifiers>.<packageOrTypeName>", "packageOrTypeName");
            Assert.IsTrue(match.Succeeded);

            //Exception object reference not set to an instance of an object
            //Assert.AreEqual(match.Get("packageOrTypeName.Identifiers").GetText(), "Number.Double");
        }

        [TestMethod]
        public void Parser_Name_expressionName()
        {
            ParseTreeMatch match = getParseTreeMatch("Student", "<Identifiers>", "expressionName");
            Assert.IsTrue(match.Succeeded);

            Assert.AreEqual(match.Get("Identifiers").GetText(), "Student");
        }

        [TestMethod]
        public void Parser_Name_expressionName_AmbiguousName()
        {
            //failed
            ParseTreeMatch match = getParseTreeMatch("Student.Number", "<Identifiers>.<ambiguousName>", "expressionName");
            Assert.IsTrue(match.Succeeded);

            //Exception object reference not set to an instance of an object
            //Assert.AreEqual(match.Get("ambiguousName.Identifiers").GetText(), "Student.Number");
        }

        [TestMethod]
        public void Parser_Name_methodName()
        {
            ParseTreeMatch match = getParseTreeMatch("getMummy", "<Identifiers>", "methodName");
            Assert.IsTrue(match.Succeeded);

            Assert.AreEqual(match.Get("Identifiers").GetText(), "getMummy");
        }

        [TestMethod]
        public void Parser_Name_ambiguousName()
        {
            ParseTreeMatch match = getParseTreeMatch("Student", "<Identifiers>", "ambiguousName");
            Assert.IsTrue(match.Succeeded);

            //Exception object reference not set to an instance of an object
            Assert.AreEqual(match.Get("Identifiers").GetText(), "Student");
        }

        [TestMethod]
        public void Parser_Name_ambiguousName_2()
        {
            ParseTreeMatch match = getParseTreeMatch("Human.Student", "<Identifiers>.<ambiguousName>", "ambiguousName");
            Assert.IsTrue(match.Succeeded);

            //Exception object reference not set to an instance of an object
            //Assert.AreEqual(match.Get("Identifiers.ambiguousName").GetText(), "Human.Student");
        }
    }
}
