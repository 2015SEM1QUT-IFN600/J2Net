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
        ScopeStack ss;

        public J2NetNameBinder(Parser parser, ScopeStack scopeStack)
        {
            parser.AddParseListener(this); //upon instantiation, add this listener to a parser
            ss = scopeStack;
        }


        public override void EnterLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
        {
            ss.Add(context.variableDeclaratorList().GetText(), ScopeStack.Kind.VARIABLE, context.GetChild(0).GetText());
        }

        public override void EnterClassDeclaration(JavaParser.ClassDeclarationContext context)
        {
            ss.Push(context.normalClassDeclaration().Identifiers().GetText(), ScopeStack.Kind.CLASS);
        }

        public override void EnterMethodDeclaration(JavaParser.MethodDeclarationContext context)
        {
            ss.Push(context.methodHeader().methodDeclarator().Identifiers().GetText(), ScopeStack.Kind.FUNCTION);

        }

        public override void EnterPackageDeclaration(JavaParser.PackageDeclarationContext context)
        {
            //TODO: Harsh, Can you tell me how this would work with ScopeStack? thx, jason

            String packageName = "";
            int i = 1;
            while (i < context.ChildCount - 1)
            { //-1 avoids the endline character
                packageName += context.GetChild(i).GetText();
                i++;
            }


        }

        public override void EnterInterfaceDeclaration(JavaParser.InterfaceDeclarationContext context)
        {
            //TODO: Harsh, Same with this one. thx, jason
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
            ss.Add(context.variableDeclaratorList().GetText(), ScopeStack.Kind.VARIABLE, context.GetChild(0).GetText());
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
