using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Antlr4.Runtime;
using J2Net.Grammar;

namespace J2Net.Tests
{
    [TestClass]
    public class LexerTests
    {
        // NOTE: Cannot test lexer rules which use the ->skip lexer command, 
        //   for more details, see http://www.antlr.org/api/Java/org/antlr/v4/runtime/CommonTokenStream.html
        
        
        /// <summary>
        /// Assert that said javaCode equals expected tokenType
        /// </summary>
        /// <param name="javaCode"></param>
        /// <param name="tokenType"></param>
        private static void AssertToken(String javaCode, int tokenType)
        {
            AntlrInputStream antlrStream = new AntlrInputStream(javaCode);
            JavaLexer lexer = new JavaLexer(antlrStream);
            CommonToken token = new CommonToken(lexer.NextToken());
            Assert.AreEqual(tokenType, token.Type);
        }

        [TestMethod]
        public void Lexer_Identifiers_letters()
        {
            AssertToken("myVar", JavaLexer.Identifiers);
        }

        [TestMethod]
        public void Lexer_Keywords_if()
        {
            AssertToken("if", JavaLexer.IF);
        }
        
        [TestMethod]
        public void Lexer_Keywords_abstract()
        {
            AssertToken("abstract", JavaLexer.ABSTRACT);
        }

        [TestMethod]
        public void Lexer_Keywords_void()
        {
            AssertToken("void", JavaLexer.VOID);
        }

        [TestMethod]
        public void Lexer_Keywords_for()
        {
            AssertToken("for", JavaLexer.FOR);
        }

        [TestMethod]
        public void Lexer_Keywords_while()
        {
            AssertToken("while", JavaLexer.WHILE);
        }

        [TestMethod]
        public void Lexer_Literals_decimalLiteral()
        {
            AssertToken("42", JavaLexer.IntegerLiteral);
        }

        [TestMethod]
        public void Lexer_Literals_hexLiteral()
        {
            AssertToken("0x90Af", JavaLexer.IntegerLiteral);
        }

        [TestMethod]
        public void Lexer_Literals_octalLiteral()
        {
            AssertToken("08", JavaLexer.IntegerLiteral);
        }

        [TestMethod]
        public void Lexer_Literals_binaryLiteral()
        {
            AssertToken("1bl", JavaLexer.IntegerLiteral); // what? 
            AssertToken("1by", JavaLexer.IntegerLiteral); // what? 
            AssertToken("1b", JavaLexer.IntegerLiteral);
            AssertToken("01_10_00_11b", JavaLexer.IntegerLiteral);
            AssertToken("01_10_00_11", JavaLexer.IntegerLiteral);
        }

        [TestMethod]
        public void Lexer_Literals_decimalFloatingPointLiteral()
        {
            AssertToken("42f", JavaLexer.FloatingPointLiteral);
            AssertToken("42F", JavaLexer.FloatingPointLiteral);
        }
    }
}
