using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Antlr4.Runtime;
using J2Net.Grammar;

namespace J2Net.Tests
{
    [TestClass]
    public class WhitespaceTests
    {
        JavaLexer lexer;

        [ClassInitialize]
        public void InitializeTestEnvironment()
        {
            string fileName = Path.GetFullPath(Path.Combine(".\\JavaCodeTestFiles\\", this.GetType().Name, ".java"));

            StreamReader inputStream = new StreamReader(fileName);                        //read file into stream
            AntlrInputStream input = new AntlrInputStream(inputStream.ReadToEnd());       //pass the string (read input) into an Antlr stream
            lexer = new JavaLexer(input);                                                //lexer is created with Antlr stream

            //TODO: determine how tests will interact with tokens
            // 1 option) use CommonTokenStream to create tokens here and then handle the stream in the unit tests
        }

        [TestMethod]
        public void FoundWhitespace()
        {
            // 2 option) use CommonToken to loop through tokens, may look something like this
            int SOME_EXPECTED_TOKEN_VALUE = 0;
            CommonToken tokens;
            do
            {
                tokens = new CommonToken(lexer.NextToken());
                Assert.IsTrue(tokens.Type == SOME_EXPECTED_TOKEN_VALUE);

            } while (!tokens.Type.Equals(JavaLexer.Eof));
        }
    }
}
