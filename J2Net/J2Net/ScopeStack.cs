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
            public Antlr4.Runtime.RuleContext parent;
            public IList<Antlr4.Runtime.Tree.IParseTree> children;
            //parent
            //children?
        }
        //!contains then lookup parent if not found error
        public static void pushScope(Scope scopeItem)//Scope ScopeItem)
        {
            Scope_Stack.Push(scopeItem);
        }

        public static Scope popScope()
        {
            return (Scope)Scope_Stack.Pop();
        }

        public static bool inScope(String ident, Scope scope)
        {
            Antlr4.Runtime.RuleContext currentNode = scope.parent;
            while (!currentNode.parent.IsEmpty)
            {
                int i = 0;
                while (i < currentNode.ChildCount)
                {
                    Antlr4.Runtime.Tree.IParseTree currentChild = currentNode.GetChild(i);
                    string test = currentChild.GetText();
                    if (test == ident)
                    {
                        return true;
                    }
                    i++;
                }
                currentNode = currentNode.parent;
            }
            //Breaks out of while for the final node, need to do last check
            int x = 0;
            while (x < currentNode.ChildCount)
            {
                Antlr4.Runtime.Tree.IParseTree currentChild = currentNode.GetChild(x);
                string test2 = currentChild.GetText();
                if (test2 == ident)
                {
                    return true;
                }
                x++;
            }
            return true;
        }

        public static void printScopeStack()
        {
            while (Scope_Stack.Count > 0)
            {
                Scope nextScope = popScope();
                Console.WriteLine("ident:"+nextScope.ident);
                Console.WriteLine("type:"+nextScope.Type);
                Console.WriteLine("parent:"+nextScope.parent.ToString());
                int i = 0;
                while(i<nextScope.children.Count){
                    Console.WriteLine("child " + i + ": " + nextScope.children[i].ToString());
                    i++;
                }
                Console.WriteLine("-----");
            }
        }

    }
}