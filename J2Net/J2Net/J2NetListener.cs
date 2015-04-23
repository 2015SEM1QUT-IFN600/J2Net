using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2Net.Grammar;
using Antlr4.Runtime;
using System.Diagnostics;

namespace J2Net
{
    [CLSCompliant(false)]
    public partial class J2NetListener : JavaBaseListener
    {
        public J2NetListener(Parser parser)
        {
            parser.AddParseListener(this); //upon instantiation, add this listener to a parser
        }

        public override void EnterPackageDeclaration(JavaParser.PackageDeclarationContext context)
        {
            base.EnterPackageDeclaration(context);
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }


        public override void EnterClassDeclaration(JavaParser.ClassDeclarationContext context)
        {
            base.EnterClassDeclaration(context);
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }

        public override void EnterFieldDeclaration(JavaParser.FieldDeclarationContext context)
        {
            base.EnterFieldDeclaration(context);
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }

        public override void EnterMethodDeclaration(JavaParser.MethodDeclarationContext context)
        {
            base.EnterMethodDeclaration(context);
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }

        public override void EnterLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
        {
            base.EnterLocalVariableDeclaration(context);
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }

        public override void EnterStatement(JavaParser.StatementContext context)
        {
            base.EnterStatement(context);
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }

        public override void EnterExpression(JavaParser.ExpressionContext context)
        {
            base.EnterExpression(context);
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }
    }

    /// <summary>
    /// Keeping this method separate so not to confuse this method with the overrides
    /// </summary>
    public partial class J2NetListener
    {
        private static void Log(String methodName, String contextText)
        {
            String msg = String.Format("{0}: {1}", methodName, contextText);
            Debug.WriteLine(msg);
        }
    }
}
