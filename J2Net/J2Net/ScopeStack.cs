using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace J2Net
{
    public class ScopeStack
    {
        /// <summary>
        /// KNOWN ISSUES: Because scopeTable is a flat dictionary of scope, there is a potential for scope name conflicts. Example:
        ///                     class foo(){
        ///                         void abc(){}
        ///                     }
        ///                     class bar(){
        ///                         void abc(){} <!-- this is legal, but would result in a name conflict
        ///                     }
        ///               This should be ok for our simple hello world tests... we can extend this later if there is problem 
        /// </summary>

        private SymbolTable currentScopeLevel;
        private Dictionary<string, SymbolTable> scopeTable;
        private static string ROOT_NODE_NAME = "__root";
        public enum Kind { CLASS, FUNCTION, PARAMETER, STATEMENT, VARIABLE, LABEL };

        public ScopeStack()
        {
            // create root symbol table
            currentScopeLevel = new SymbolTable()
            {
                Parent = null,
                Name = ROOT_NODE_NAME,
                Symbols = new Dictionary<string, Symbol>()
            };

            // add symbol table to a new scope table
            scopeTable = new Dictionary<string, SymbolTable>();
            scopeTable.Add(ROOT_NODE_NAME, currentScopeLevel);
        }

        public void Push(string scopeName, Kind kind)
        {
            // only functions, classes, and statements can declare a new scope
            if (!(kind == Kind.FUNCTION || kind == Kind.CLASS || kind == Kind.STATEMENT))
            {
                throw new IlegalScopeTableException();
            }

            // add a reference to the new scope in the currentScopeLevel
            Add(scopeName, kind, "");

            // create a new symbol table with currentScopeLevel as parent, and set this table as currentScopeLevel
            currentScopeLevel = new SymbolTable()
            {
                Parent = currentScopeLevel,
                Name = scopeName,
                Symbols = new Dictionary<string, Symbol>()
            };

            // add this table as a new scope in scope table
            scopeTable.Add(scopeName, currentScopeLevel);

        }

        public void Pop()
        {
            // set currentScopeLevel to parent if not null
            if (currentScopeLevel.Parent != null)
            {
                currentScopeLevel = currentScopeLevel.Parent;
            }
        }

        public void Add(string id, Kind kind, string type)
        {
            try
            {
                // only add if id hasn't been already declared
                Resolve(id, currentScopeLevel.Name);
            }
            catch (UndeclaredException)
            {
                // add new entry into Symbol Table at current scope
                currentScopeLevel.Symbols.Add(id, new Symbol() { Kind = kind, Type = type });
                return;
            }

            // throw an exception if name is already declared
            throw new NameClashException();
        }

        public Symbol Resolve(string id, string scopeName)
        {
            // get symbol table for specified scope
            SymbolTable st;
            Symbol sym;
            if (scopeTable.TryGetValue(scopeName, out st))
            {

                while (st != null)
                {
                    // return the symbol object for specified identifier
                    if (st.Symbols.TryGetValue(id, out sym))
                    {
                        return sym;
                    }

                    // if not found in current table, move to parent and repeat
                    st = st.Parent;
                }
            }
            else
            {
                throw new IlegalScopeNameException();
            }

            // throw an exception if identifier can't be found 
            throw new UndeclaredException();
        }

        public int Count(Kind kind, string scopeName)
        {
            // get symbol table for specified scope
            SymbolTable st;
            if (scopeTable.TryGetValue(scopeName, out st))
            {

                // return a count of identifiers for specified kind
                return st.Symbols.Where(p => p.Value.Kind == kind).Count();
            }
            return 0;
        }

        public override string ToString()
        {
            // loop through all scopes
            StringBuilder sb = new StringBuilder();
            foreach (var scope in scopeTable)
            {
                // loop through all symbols tables
                foreach (var symbol in scope.Value.Symbols)
                {

                    // record each item 
                    sb.AppendFormat("{0},{1},{2},{3}\n", scope.Key, symbol.Key, symbol.Value.Kind, symbol.Value.Type);
                }
            }

            // return as a string 
            return sb.ToString();
        }

        private class SymbolTable
        {
            public SymbolTable Parent;
            public string Name;
            public Dictionary<string, Symbol> Symbols;
        }

        public struct Symbol
        {
            public Kind Kind;
            public string Type;
        }

        public class NameClashException : Exception { }
        public class UndeclaredException : Exception { }
        public class IlegalScopeTableException : Exception { }
        public class IlegalScopeNameException : Exception { }
    }
}