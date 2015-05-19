using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2Net.Tests
{
    [TestClass]
    public class ScopeStackTest
    {

        private static ScopeStack setup()
        {
            ScopeStack ss = new ScopeStack();
            ss.Add("x", ScopeStack.Kind.VARIABLE, "int");
            ss.Push("foo", ScopeStack.Kind.FUNCTION);
            ss.Add("p", ScopeStack.Kind.PARAMETER, "int");
            ss.Add("a", ScopeStack.Kind.VARIABLE, "float");
            ss.Add("b", ScopeStack.Kind.VARIABLE, "float");
            ss.Push("unnamed", ScopeStack.Kind.FUNCTION);
            ss.Add("n", ScopeStack.Kind.VARIABLE, "bytes");
            ss.Pop();
            ss.Pop();
            ss.Add("y", ScopeStack.Kind.VARIABLE, "float");
            ss.Push("bar", ScopeStack.Kind.FUNCTION);
            ss.Add("q", ScopeStack.Kind.PARAMETER, "int");
            ss.Add("c", ScopeStack.Kind.VARIABLE, "bool");
            ss.Pop();

            return ss;
        }

        [TestMethod]
        public void ScopeStack_IntendedFunctionality()
        {
            ScopeStack ss = setup();

            ScopeStack.Symbol sym = ss.Resolve("x", "unnamed");

            Assert.IsNotNull(sym);
            Assert.AreEqual(sym.Type, "int");
            Assert.AreEqual(sym.Kind, ScopeStack.Kind.VARIABLE);

            Debug.WriteLine(ss.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ScopeStack.UndeclaredException))]
        public void ScopeStack_ResolvingUndeclaredId()
        {
            ScopeStack ss = setup();
            ss.Resolve("qbert", "bar");
        }

        [TestMethod]
        [ExpectedException(typeof(ScopeStack.NameClashException))]
        public void ScopeStack_AddingNameClashingId()
        {
            ScopeStack ss = setup();
            ss.Add("x", ScopeStack.Kind.VARIABLE, "float");
        }

        [TestMethod]
        [ExpectedException(typeof(ScopeStack.IlegalScopeTableException))]
        public void ScopeStack_MissusingPushOperation()
        {
            ScopeStack ss = setup();
            ss.Push("AwesomeMethod", ScopeStack.Kind.VARIABLE);
        }
        [TestMethod]
        [ExpectedException(typeof(ScopeStack.IlegalScopeNameException))]
        public void ScopeStack_MissuingResolveOperation()
        {
            ScopeStack ss = setup();
            ss.Resolve("x", "does not exists");
        }

        [TestMethod]
        public void ScopeStack_JustDontError()
        {
            ScopeStack ss = new ScopeStack();
            //write when there is nothing
            Debug.WriteLine(ss.ToString());
            //pop when there is nothing
            ss.Pop();
            ss.Pop();
        }
    }
}
