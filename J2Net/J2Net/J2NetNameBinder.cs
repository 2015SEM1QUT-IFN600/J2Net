using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2Net.Grammar;
using Antlr4.Runtime.Tree;
namespace J2Net
{
    class J2NetNameBinder
    {
        //TODO: Identify which rules to push declarations onto the Symbol Table

        //TODO: Push items onto Symbol Table
       static  Hashtable SymTable = new Hashtable();
        
        public void SymbolTable(String ident, String Type)
        {
            SymTable.Add(ident, Type); //puhsing identifier and its datatype into hashtable
            
        }

       public void printHash()
       {
           foreach (DictionaryEntry entry in SymTable)
           {
               Console.WriteLine(entry.Key.ToString() + "    " + entry.Value);
           }
       }
        
    }
}
