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
        public void Lexer_Keywords_abstract()
        {
            AssertToken("abstract", JavaLexer.ABSTRACT);
        }

        [TestMethod]
        public void Lexer_Keywords_continue()
        {
            AssertToken("continue", JavaLexer.CONTINUE);
        }

        [TestMethod]
        public void Lexer_Keywords_for()
        {
            AssertToken("for", JavaLexer.FOR);
        }

        [TestMethod]
        public void Lexer_Keywords_new()
        {
            AssertToken("new", JavaLexer.NEW);
        }

        [TestMethod]
        public void Lexer_Keywords_switch()
        {
            AssertToken("switch", JavaLexer.SWITCH);
        }

        [TestMethod]
        public void Lexer_Keywords_assert()
        {
            AssertToken("assert", JavaLexer.ASSERT);
        }

        [TestMethod]
        public void Lexer_Keywords_default()
        {
            AssertToken("default", JavaLexer.DEFAULT);
        }

        [TestMethod]
        public void Lexer_Keywords_if()
        {
            AssertToken("if", JavaLexer.IF);
        }

        [TestMethod]
        public void Lexer_Keywords_package()
        {
            AssertToken("package", JavaLexer.PACKAGE);
        }

        [TestMethod]
        public void Lexer_Keywords_synchronized()
        {
            AssertToken("synchronized", JavaLexer.SYNCHRONIZED);
        }

        [TestMethod]
        public void Lexer_Keywords_boolean()
        {
            AssertToken("boolean", JavaLexer.BOOLEAN);
        }

        [TestMethod]
        public void Lexer_Keywords_do()
        {
            AssertToken("do", JavaLexer.DO);
        }

        [TestMethod]
        public void Lexer_Keywords_goto()
        {
            AssertToken("goto", JavaLexer.GOTO);
        }

        [TestMethod]
        public void Lexer_Keywords_private()
        {
            AssertToken("private", JavaLexer.PRIVATE);
        }

        [TestMethod]
        public void Lexer_Keywords_this()
        {
            AssertToken("this", JavaLexer.THIS);
        }

        [TestMethod]
        public void Lexer_Keywords_break()
        {
            AssertToken("break", JavaLexer.BREAK);
        }

        [TestMethod]
        public void Lexer_Keywords_double()
        {
            AssertToken("double", JavaLexer.DOUBLE);
        }

        [TestMethod]
        public void Lexer_Keywords_implements()
        {
            AssertToken("implements", JavaLexer.IMPLEMENTS);
        }

        [TestMethod]
        public void Lexer_Keywords_protected()
        {
            AssertToken("protected", JavaLexer.PROTECTED);
        }

        [TestMethod]
        public void Lexer_Keywords_throw()
        {
            AssertToken("throw", JavaLexer.THROW);
        }

        [TestMethod]
        public void Lexer_Keywords_byte()
        {
            AssertToken("byte", JavaLexer.BYTE);
        }

        [TestMethod]
        public void Lexer_Keywords_else()
        {
            AssertToken("else", JavaLexer.ELSE);
        }

        [TestMethod]
        public void Lexer_Keywords_import()
        {
            AssertToken("import", JavaLexer.IMPORT);
        }

        [TestMethod]
        public void Lexer_Keywords_public()
        {
            AssertToken("public", JavaLexer.PUBLIC);
        }

        [TestMethod]
        public void Lexer_Keywords_throws()
        {
            AssertToken("throws", JavaLexer.THROWS);
        }

        [TestMethod]
        public void Lexer_Keywords_case()
        {
            AssertToken("case", JavaLexer.CASE);
        }

        [TestMethod]
        public void Lexer_Keywords_enum()
        {
            AssertToken("enum", JavaLexer.ENUM);
        }

        [TestMethod]
        public void Lexer_Keywords_instanceof()
        {
            AssertToken("instanceof", JavaLexer.INSTANCEOF);
        }

        [TestMethod]
        public void Lexer_Keywords_return()
        {
            AssertToken("return", JavaLexer.RETURN);
        }

        [TestMethod]
        public void Lexer_Keywords_transient()
        {
            AssertToken("transient", JavaLexer.TRANSIENT);
        }

        [TestMethod]
        public void Lexer_Keywords_catch()
        {
            AssertToken("catch", JavaLexer.CATCH);
        }

        [TestMethod]
        public void Lexer_Keywords_extends()
        {
            AssertToken("extends", JavaLexer.EXTENDS);
        }

        [TestMethod]
        public void Lexer_Keywords_int()
        {
            AssertToken("int", JavaLexer.INT);
        }

        [TestMethod]
        public void Lexer_Keywords_short()
        {
            AssertToken("short", JavaLexer.SHORT);
        }

        [TestMethod]
        public void Lexer_Keywords_try()
        {
            AssertToken("try", JavaLexer.TRY);
        }

        [TestMethod]
        public void Lexer_Keywords_char()
        {
            AssertToken("char", JavaLexer.CHAR);
        }

        [TestMethod]
        public void Lexer_Keywords_final()
        {
            AssertToken("final", JavaLexer.FINAL);
        }

        [TestMethod]
        public void Lexer_Keywords_interface()
        {
            AssertToken("interface", JavaLexer.INTERFACE);
        }

        [TestMethod]
        public void Lexer_Keywords_static()
        {
            AssertToken("static", JavaLexer.STATIC);
        }

        [TestMethod]
        public void Lexer_Keywords_void()
        {
            AssertToken("void", JavaLexer.VOID);
        }

        [TestMethod]
        public void Lexer_Keywords_class()
        {
            AssertToken("class", JavaLexer.CLASS);
        }

        [TestMethod]
        public void Lexer_Keywords_finally()
        {
            AssertToken("finally", JavaLexer.FINALLY);
        }

        [TestMethod]
        public void Lexer_Keywords_strictfp()
        {
            AssertToken("strictfp", JavaLexer.STRICTFP);
        }

        [TestMethod]
        public void Lexer_Keywords_volatile()
        {
            AssertToken("volatile", JavaLexer.VOLATILE);
        }

        [TestMethod]
        public void Lexer_Keywords_const()
        {
            AssertToken("const", JavaLexer.CONST);
        }

        [TestMethod]
        public void Lexer_Keywords_float()
        {
            AssertToken("float", JavaLexer.FLOAT);
        }

        [TestMethod]
        public void Lexer_Keywords_native()
        {
            AssertToken("native", JavaLexer.NATIVE);
        }

        [TestMethod]
        public void Lexer_Keywords_super()
        {
            AssertToken("super", JavaLexer.SUPER);
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


        //
        [TestMethod]
        public void Lexer_Separators_lparen()
        {
            AssertToken("(", JavaLexer.LPAREN);
        }

        [TestMethod]
        public void Lexer_Separators_rparen()
        {
            AssertToken(")", JavaLexer.RPAREN);
        }

        [TestMethod]
        public void Lexer_Separators_lbrace()
        {
            AssertToken("{", JavaLexer.LBRACE);
        }

        [TestMethod]
        public void Lexer_Separators_rbrace()
        {
            AssertToken("}", JavaLexer.RBRACE);
        }

        [TestMethod]
        public void Lexer_Separators_lbrack()
        {
            AssertToken("[", JavaLexer.LBRACK);
        }

        [TestMethod]
        public void Lexer_Separators_rbrack()
        {
            AssertToken("]", JavaLexer.RBRACK);
        }

        [TestMethod]
        public void Lexer_Separators_semi()
        {
            AssertToken(";", JavaLexer.SEMI);
        }

        [TestMethod]
        public void Lexer_Separators_comma()
        {
            AssertToken(",", JavaLexer.COMMA);
        }

        [TestMethod]
        public void Lexer_Separators_dot()
        {
            AssertToken(".", JavaLexer.DOT);
        }

        [TestMethod]
        public void Lexer_Separators_trpdot()
        {
            AssertToken("...", JavaLexer.TRPDOT);
        }

        [TestMethod]
        public void Lexer_Separators_atsign()
        {
            AssertToken("@", JavaLexer.ATSIGN);
        }

        [TestMethod]
        public void Lexer_Separators_dblcolon()
        {
            AssertToken("::", JavaLexer.DBLCOLON);
        }

        [TestMethod]
        public void Lexer_Operators_inc()
        {
            AssertToken("++", JavaLexer.INC);
        }

        [TestMethod]
        public void Lexer_Operators_dec()
        {
            AssertToken("--", JavaLexer.DEC);
        }

        [TestMethod]
        public void Lexer_Operators_tilde()
        {
            AssertToken("~", JavaLexer.TILDE);
        }

        [TestMethod]
        public void Lexer_Operators_not()
        {
            AssertToken("!", JavaLexer.NOT);
        }

        [TestMethod]
        public void Lexer_Operators_mul()
        {
            AssertToken("*", JavaLexer.MUL);
        }

        [TestMethod]
        public void Lexer_Operators_div()
        {
            AssertToken("/", JavaLexer.DIV);
        }

        [TestMethod]
        public void Lexer_Operators_mod()
        {
            AssertToken("%", JavaLexer.MOD);
        }
        
        [TestMethod]
        public void Lexer_Operators_this()
        {
            AssertToken(".", JavaLexer.DOT);
        }
        
        [TestMethod]
        public void Lexer_Operators_add()
        {
            AssertToken("+", JavaLexer.ADD);
        }

        [TestMethod]
        public void Lexer_Operators_sub()
        {
            AssertToken("-", JavaLexer.SUB);
        }

        [TestMethod]
        public void Lexer_Operators_assign()
        {
            AssertToken("=", JavaLexer.ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_left_signed()
        {
            AssertToken("<<", JavaLexer.LEFT_SIGNED);
        }

        [TestMethod]
        public void Lexer_Operators_right_signed()
        {
            AssertToken(">>", JavaLexer.RIGHT_SIGNED);
        }

        [TestMethod]
        public void Lexer_Operators_right_unsigned()
        {
            AssertToken(">>>", JavaLexer.RIGHT_UNSIGNED);
        }

        [TestMethod]
        public void Lexer_Operators_urshift_assign()
        {
            AssertToken(">>>=", JavaLexer.URSHIFT_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_lt()
        {
            AssertToken("<", JavaLexer.LT);
        }

        [TestMethod]
        public void Lexer_Operators_le()
        {
            AssertToken("<=", JavaLexer.LE);
        }

        [TestMethod]
        public void Lexer_Operators_gt()
        {
            AssertToken(">", JavaLexer.GT);
        }

        [TestMethod]
        public void Lexer_Operators_ge()
        {
            AssertToken(">=", JavaLexer.GE);
        }

        [TestMethod]
        public void Lexer_Operators_equal()
        {
            AssertToken("==", JavaLexer.EQUAL);
        }

        [TestMethod]
        public void Lexer_Operators_notequal()
        {
            AssertToken("!=", JavaLexer.NOTEQUAL);
        }

        [TestMethod]
        public void Lexer_Operators_bitand()
        {
            AssertToken("&", JavaLexer.BITAND);
        }

        [TestMethod]
        public void Lexer_Operators_caret()
        {
            AssertToken("^", JavaLexer.CARET);
        }

        [TestMethod]
        public void Lexer_Operators_bitor()
        {
            AssertToken("|", JavaLexer.BITOR);
        }

        [TestMethod]
        public void Lexer_Operators_and()
        {
            AssertToken("&&", JavaLexer.AND);
        }

        [TestMethod]
        public void Lexer_Operators_or()
        {
            AssertToken("||", JavaLexer.OR);
        }

        [TestMethod]
        public void Lexer_Operators_question()
        {
            AssertToken("?", JavaLexer.QUESTION);
        }

        [TestMethod]
        public void Lexer_Operators_colon()
        {
            AssertToken(":", JavaLexer.COLON);
        }

        [TestMethod]
        public void Lexer_Operators_add_assign()
        {
            AssertToken("+=", JavaLexer.ADD_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_sub_assign()
        {
            AssertToken("-=", JavaLexer.SUB_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_mul_assign()
        {
            AssertToken("*=", JavaLexer.MUL_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_div_assign()
        {
            AssertToken("/=", JavaLexer.DIV_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_and_assign()
        {
            AssertToken("&=", JavaLexer.AND_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_or_assign()
        {
            AssertToken("|=", JavaLexer.OR_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_xor_assign()
        {
            AssertToken("^=", JavaLexer.XOR_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_mod_assign()
        {
            AssertToken("%=", JavaLexer.MOD_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_lshift_assign()
        {
            AssertToken("<<=", JavaLexer.LSHIFT_ASSIGN);
        }

        [TestMethod]
        public void Lexer_Operators_rshift_assign()
        {
            AssertToken(">>=", JavaLexer.RSHIFT_ASSIGN);
        }

    }
}
