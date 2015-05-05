using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace J2Net
{
    class ScopeStack
    {
        //TODO: Find out what kind of data type this should be to accomplish a Symbol Table

        //TODO: build this as that data type 
        static Stack Scope_Stack = new Stack();

        public struct Scope
        {
            public String ident;
            public String Type;
            //parent
            //children?
        }
        //!contains then lookup parent if not found error
        public static void pushScope(String ident)//Scope ScopeItem)
        {
            Scope_Stack.Push(ident);//ScopeItem);
        }

        public Scope popScope()
        {
            return (Scope)Scope_Stack.Pop();
        }

        public static void printScopeStack()
        {
            while (Scope_Stack.Count > 0)
            {
                Console.WriteLine(Scope_Stack.Pop().ToString());
            }
        }

    }
}