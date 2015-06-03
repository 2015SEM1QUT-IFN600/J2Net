using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace J2Net.Tests
{
    public class CompilerException : Exception
    {
        public string CompilerMessage { get; protected set; }

        public CompilerException(string message, Exception innerException)
            : base(message, innerException)
        {
            CompilerMessage = message;
        }

        public CompilerException(string message)
            : base(message)
        {
            CompilerMessage = message;
        }

    }
}