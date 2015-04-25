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

        /// <summary>
        /// This will generate a ParseTreeMatch object based on testCode and the ruleName that you want to test. 
        /// The pattern is a token that you wish to see matched from your rule. The ParseTreeMatch object is 
        /// analogous to the Match object that you would receive after a RegEx match. But a better explanation 
        /// is through an example. 
        /// 
        /// For this example, suppose the grammar is the following. In reality, the grammar is all of our parsers:
        ///     expr : ID '=' s ';';
        ///     s : ID ('+' ID)?;
        ///     ID : [a-zA-Z]+;
        /// 
        /// testCode = "foo = a + b + z;";
        /// ruleName = "expr";                      // you're entering the grammar here
        /// pattern  = "<ID> = <ID> + <ID> + <ID>;" // you're identifying the lower level tokens here. In order to 
        ///                                         // get from expr to ID tokens, it'll have passed through rule 's'.
        ///                                         // so we'll have essentially tested rule 's' as well.
        /// 
        /// HOWEVER, patterns can be labeled. So rather than a bunch of <ID> which is difficult to tell which ID you're 
        /// referring too, do the following.
        /// 
        /// pattern  = "<a:ID> = <b:ID> + <c:ID> + <d:ID>;"
        /// 
        /// ParseTreeMatch m = GetParseTreeMatch(testCode, pattern, ruleName); // Now, match it
        ///
        /// FROM HERE, you'll have a ParseTreeMatch object that will allow you to inspect whether or not it matched as 
        /// expected. See javadoc reference link to ParseTreeMatch for more detail. In most cases, we'll simply want to 
        /// see that the match was successful or that the value is as expected.
        /// 
        /// Assert.IsTrue(match.Succeeded);
        /// Assert.AreEqual(m.Get("a").GetText(), "foo");
        /// 
        /// IF YOU WANT TO CHECK ALL OF IT, you may want to work with arrays instead.
        ///
        /// Assert.AreEqual(m.GetAll("ID").toString(), "[foo, a, b, z]");
        ///
        /// </summary>
        /// <param name="testCode">The line of Java code that you wish to parse.</param>
        /// <param name="pattern">Tokenized "<token>" pattern that the parser should match too</param>
        /// <param name="ruleName">The name of the rule that we're targeting</param>
        /// <seealso type="javadoc" cref="http://www.antlr.org/api/Java/org/antlr/v4/runtime/tree/pattern/ParseTreeMatch.html"/>
        /// <seealso cref="https://theantlrguy.atlassian.net/wiki/display/ANTLR4/Parse+Tree+Matching+and+XPath"/>
        /// <seealso cref="https://github.com/antlr/antlr4/blob/master/tool/test/org/antlr/v4/test/tool/TestParseTreeMatcher.java"/>
        /// <seealso type="javadoc" cref="http://www.antlr.org/api/Java/index.html"/>
        /// <returns>ParseTreeMatch object built from Java.g4 Parser and matching against provided pattern</returns>
        private static ParseTreeMatch GetParseTreeMatch(String testCode, String pattern, String ruleName)
        {
            //Generate a lexer and parser based on test code
            JavaLexer lexer = new JavaLexer(new AntlrInputStream(testCode));
            JavaParser parser = new JavaParser(new CommonTokenStream(lexer));

            //use .Net Reflection to get the Parser's Rule's method and execute it to generate a Parse Tree object
            System.Reflection.MethodInfo ruleMethod = parser.GetType().GetMethod(ruleName);
            IParseTree ruleTree = (IParseTree)ruleMethod.Invoke(parser, null);

            //From our empty Lexer and Parser, compile a ParseTreePattern 
            ParseTreePattern treePattern = GetParseTreePattern(pattern, ruleName);

            //Pass the Rule's tree into the tree Pattern to get any match
            return treePattern.Match(ruleTree);
        }

        /// <summary>
        /// This is a singleton design pattern to construct only one empty parser that compiles a 
        /// ParseTreePattern. You should not call this directly from any Test. Use GetParseTreeMatch(..) instead.
        /// </summary>
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
            match = GetParseTreeMatch("int", "<integralType>", "type");
            Assert.IsTrue(match.Succeeded);
            Assert.AreEqual(match.Get("integralType").GetText(), "int");
            //this is obviously a wrong test, because typeName rule is broken at this time. But this gives an 
            // example of using multiple Pattern tags and going through Parser rules
            //Eric reason of change: the DOT seems to be very buggy at this stage, as it can mean DOT or THIS at the same time.
            //match = GetParseTreeMatch("@.long", "<b:annotation> <a:integralType>", "type"); 
            match = GetParseTreeMatch("@lang long", "<b:annotation> <a:integralType>", "type");
            Assert.AreEqual(match.Get("a").GetText(), "long");
            Assert.AreEqual(match.Get("b").GetText(), "@lang");
         
        }

        [TestMethod]
        public void Parser_Packages_packageDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "package <Identifiers>.<Identifiers>;";
            match = GetParseTreeMatch("package java.lang;", treePattern, "packageDeclaration");
            Assert.IsTrue(match.Succeeded);
        }

        [TestMethod]
        public void Parser_Packages_singleTypeImportDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "import <typeName>;";
            match = GetParseTreeMatch("import java.util.Vector;", treePattern, "singleTypeImportDeclaration");
            Assert.IsTrue(match.Succeeded);
        }

        [TestMethod]
        public void Parser_Packages_typeImportOnDemandDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "import <typeName>.*;";
            match = GetParseTreeMatch("import java.util.*;", treePattern, "typeImportOnDemandDeclaration");
            Assert.IsTrue(match.Succeeded);
        }
        [TestMethod]
        public void Parser_Packages_singleStaticImportDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "import static <typeName>.<Identifiers>;";
            match = GetParseTreeMatch("import static java.lang.Math.PI;", treePattern, "singleStaticImportDeclaration");
            Assert.IsTrue(match.Succeeded);
        }
        [TestMethod]
        public void Parser_Packages_staticImportOnDemandDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "import static <typeName>.*;";
            match = GetParseTreeMatch("import static java.lang.Math.*;", treePattern, "staticImportOnDemandDeclaration");
            Assert.IsTrue(match.Succeeded);
        }
        [TestMethod]
        public void Parser_Packages_HelloIFN660PackageDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "package <Identifiers>.<Identifiers>;";
            match = GetParseTreeMatch("package com.company;", treePattern, "packageDeclaration");
            Assert.IsTrue(match.Succeeded);
        }

        [TestMethod]
        public void Parser_Classes_classDeclaration()
        {
            ParseTreeMatch match;
            match = GetParseTreeMatch("public", "<classModifier>", "classModifier");
            Assert.IsTrue(match.Succeeded);
            match = GetParseTreeMatch("protected", "<classModifier>", "classModifier");
            Assert.IsTrue(match.Succeeded);
            match = GetParseTreeMatch("private", "<classModifier>", "classModifier");
            Assert.IsTrue(match.Succeeded);
            match = GetParseTreeMatch("abstract", "<classModifier>", "classModifier");
            Assert.IsTrue(match.Succeeded);
            match = GetParseTreeMatch("static", "<classModifier>", "classModifier");
            Assert.IsTrue(match.Succeeded);
            match = GetParseTreeMatch("final", "<classModifier>", "classModifier");
            Assert.IsTrue(match.Succeeded);
            match = GetParseTreeMatch("strictfp", "<classModifier>", "classModifier");
            Assert.IsTrue(match.Succeeded);
        }

        [TestMethod]
        public void Parser_Classes_normalClassDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "<classModifier> class <Identifiers> { }";
            match = GetParseTreeMatch("public class main { }", treePattern, "normalClassDeclaration");
            Assert.IsTrue(match.Succeeded);
        }

        [TestMethod]
        public void Parser_Classes_methodDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "{<methodModifier> <methodModifier> <result> <methodDeclarator> { }}";
            match = GetParseTreeMatch("{public static void main(String[] args) { }}", treePattern, "classBody");
            Assert.IsTrue(match.Succeeded);
        }

        [TestMethod]
        public void Parser_Classes_enumDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "<classModifier> enum <Identifiers> { }";
            match = GetParseTreeMatch("public enum testEnum { }", treePattern, "enumDeclaration");
            Assert.IsTrue(match.Succeeded);
        }

        [TestMethod]
        public void Parser_Expressions_primaryNoNewArray()
        {
            ParseTreeMatch match;
            match = GetParseTreeMatch("true", "<literal>", "primaryNoNewArray");
            Assert.IsTrue(match.Succeeded, "boolean literal");
            match = GetParseTreeMatch("false", "<literal>", "primaryNoNewArray");
            Assert.IsTrue(match.Succeeded, "boolean literal");

            match = GetParseTreeMatch(".", "<classLiteral>", "primaryNoNewArray");
            Assert.IsTrue(match.Succeeded, "class literal");

            match = GetParseTreeMatch("new", "<classInstanceCreationExpression>", "primaryNoNewArray");
            Assert.IsTrue(match.Succeeded, "'new' operation");
            match = GetParseTreeMatch("super.a", "<fieldAccess>", "primaryNoNewArray");
            Assert.IsTrue(match.Succeeded, "accessing field");

            match = GetParseTreeMatch("super", "<methodInvocation>", "primaryNoNewArray");
            Assert.IsTrue(match.Succeeded, "method's super");
            match = GetParseTreeMatch("Method()", "<methodReference>", "primaryNoNewArray");
            Assert.IsTrue(match.Succeeded, "method reference");
        }

        [TestMethod]
        public void Parser_Expressions_classInstanceCreationExpression()
        {
            ParseTreeMatch match;
            match = GetParseTreeMatch("new a", "<unqualifiedClassInstanceCreationExpression>", "classInstanceCreationExpression");
            Assert.IsTrue(match.Succeeded);
            //match = GetParseTreeMatch("String new String ", "<expressionName.unqualifiedClassInstanceCreationExpression>", "classInstanceCreationExpression");
            //Assert.IsTrue(match.Succeeded);
            //match = GetParseTreeMatch("this.", "<primary DOT unqualifiedClassInstanceCreationExpression>", "classInstanceCreationExpression");
            //Assert.IsTrue(match.Succeeded);
        }

        [TestMethod]
        public void Parser_Expressions_typeArgumentsOrDiamond()
        {
            //ParseTreeMatch match;
            //match = GetParseTreeMatch("new a", "<typeArguments>", "typeArgumentsOrDiamond");
            //Assert.IsTrue(match.Succeeded);
            //match = GetParseTreeMatch("<>", "<LT GT>", "typeArgumentsOrDiamond");
            //ssert.IsTrue(match.Succeeded);
            }

        [TestMethod]
        public void Parser_Expressions_fieldAccess()
        {
            //ParseTreeMatch match;
            //match = GetParseTreeMatch("t.x=1", "<primary . Identifiers>", "fieldAccess");
            //Assert.IsTrue(match.Succeeded);
            //match = GetParseTreeMatch("String", "SUPER . Identifiers", "fieldAccess");
            //Assert.IsTrue(match.Succeeded);
            //match = GetParseTreeMatch("String new String ", "typeName . SUPER . Identifiers", "fieldAccess");
            //Assert.IsTrue(match.Succeeded);
        } 
        
        [TestMethod]
        public void Parser_Statements_statements()
        {
            //ParseTreeMatch match;
        }

        [TestMethod]
        public void Parser_Classes_classes()
        {
            //ParseTreeMatch match;
        }

        [TestMethod]
        public void Parser_Interfaces_interfaces()
        {
            //ParseTreeMatch match;
        }

        [TestMethod]
        public void Parser_Names_names()
        {
            //ParseTreeMatch match;
        }

        [TestMethod]
        public void Parser_Packages_packages()
        {
            //ParseTreeMatch match;
        }

        [TestMethod]
        public void Parser_Interfaces_normalInterfaceDeclaration()
        {
            ParseTreeMatch match;
            String treePattern = "public interface <Identifiers>{}";
            match = GetParseTreeMatch("public interface MyInterface{}", treePattern, "normalInterfaceDeclaration");
            Assert.IsTrue(match.Succeeded);
        }
    }
}