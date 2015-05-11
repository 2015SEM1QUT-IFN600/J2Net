using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2Net.Grammar;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
namespace J2Net
{
    class J2NetNameBinder : JavaBaseListener
    {
        //TODO: Identify which rules to push declarations onto the Symbol Table

        //TODO: Push items onto Symbol Table
        string ilName;
        ScopeStack.Scope scopeItem = new ScopeStack.Scope();
        public J2NetNameBinder(Parser parser, string ilName2)
        {
            parser.AddParseListener(this); //upon instantiation, add this listener to a parser
            ilName = ilName2;
        }

       static  Hashtable SymTable = new Hashtable();
        
        public void SymbolTable(String ident, String Type)
        {
            SymTable.Add(ident, Type); //puhsing identifier and its datatype into hashtable
            scopeItem.ident = ident;
            scopeItem.Type = Type;
            ScopeStack.pushScope(scopeItem);
        }

       public void printHash()
       {
           foreach (DictionaryEntry entry in SymTable)
           {
               Console.WriteLine(entry.Key.ToString() + "    " + entry.Value);
           }
       }

       public override void EnterLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
       {
           base.EnterLocalVariableDeclaration(context);
           SymbolTable(context.variableDeclaratorList().GetText(), context.GetChild(0).GetText());
           //TODO Push to scope stack
           scopeItem.ident = context.GetChild(0).GetText();
           scopeItem.Type = context.GetChild(1).GetText();
           scopeItem.parent = context.parent.ToString();
           ScopeStack.pushScope(scopeItem);
           //ScopeStack.printScopeStack();
           //TODO parent and children
       }

       public override void EnterClassDeclaration(JavaParser.ClassDeclarationContext context)
       {
           base.EnterClassDeclaration(context);
           SymbolTable(context.normalClassDeclaration().Identifiers().GetText(), "-");
           //TODO Push to scope stack
           scopeItem.Type = context.GetChild(0).GetChild(1).GetText();
           scopeItem.ident = context.GetChild(0).GetChild(2).GetText();
           scopeItem.parent = context.parent.ToString();
           ScopeStack.pushScope(scopeItem);
           //ScopeStack.printScopeStack();
       }

       public override void EnterMethodDeclaration(JavaParser.MethodDeclarationContext context)
       {
           base.EnterMethodDeclaration(context);
           SymbolTable(context.methodHeader().methodDeclarator().Identifiers().GetText(), "-");
           //Main method context.GetChild(2).GetChild(1).GetChild(0).GetText is main, class not method
           //scopeItem.ident = context.GetChild(context.ChildCount-1).GetText();
           //scopeItem.Type = context.GetChild(context.ChildCount-2).GetText();
           //scopeItem.parent = context.parent.ToString();
           //ScopeStack.pushScope(scopeItem);
           //ScopeStack.printScopeStack();
       }

       public override void EnterPackageDeclaration(JavaParser.PackageDeclarationContext context)
       {
           //TODO Push to scope stack
           String packageName = "";
           int i = 1;
           while (i < context.ChildCount - 1)
           { //-1 avoids the endline character
               packageName += context.GetChild(i).GetText();
               i++;
           }
           scopeItem.ident = packageName;
           scopeItem.Type = context.GetChild(0).GetText();
           scopeItem.parent = context.parent.ToString();
           ScopeStack.pushScope(scopeItem);
           //ScopeStack.printScopeStack();
       }

       public override void EnterInterfaceDeclaration(JavaParser.InterfaceDeclarationContext context)
       {
           //TODO Push to scope stack
           scopeItem.ident = context.GetChild(0).GetText();
           scopeItem.Type = context.GetChild(1).GetText();
           scopeItem.parent = context.parent.ToString();
           ScopeStack.pushScope(scopeItem);
           //ScopeStack.printScopeStack();
       }

       public override void EnterClassBodyDeclaration(JavaParser.ClassBodyDeclarationContext context)
       {
           base.EnterClassBodyDeclaration(context);
       }

       public override void EnterConstructorDeclaration(JavaParser.ConstructorDeclarationContext context)
       {
           base.EnterConstructorDeclaration(context);
       }

       public override void EnterConstructorBody(JavaParser.ConstructorBodyContext context)
       {
           base.EnterConstructorBody(context);
       }

       public override void ExitConstructorBody(JavaParser.ConstructorBodyContext context)
       {
           base.ExitConstructorBody(context);
       }

       public override void ExitConstructorDeclaration(JavaParser.ConstructorDeclarationContext context)
       {
           base.ExitConstructorDeclaration(context);
       }


       public override void EnterMethodBody(JavaParser.MethodBodyContext context)
       {
           base.EnterMethodBody(context);
       }

       public override void ExitMethodBody(JavaParser.MethodBodyContext context)
       {
           base.ExitMethodBody(context);
       }



       public override void ExitLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
       {
           base.ExitLocalVariableDeclaration(context);
       }

       public override void EnterFieldDeclaration(JavaParser.FieldDeclarationContext context)
       {
           base.EnterFieldDeclaration(context);
           SymbolTable(context.variableDeclaratorList().GetText(), context.GetChild(0).GetText());
       }

       public override void ExitFieldDeclaration(JavaParser.FieldDeclarationContext context)
       {
           base.ExitFieldDeclaration(context);
       }

       public override void ExitMethodDeclaration(JavaParser.MethodDeclarationContext context)
       {
           base.ExitMethodDeclaration(context);
       }

       public override void ExitClassDeclaration(JavaParser.ClassDeclarationContext context)
       {
           base.ExitClassDeclaration(context);
       }
    }
}
