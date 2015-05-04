using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2Net.Grammar;
using Antlr4.Runtime;
using System.Diagnostics;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Misc;
using System.Collections;

namespace J2Net
{
    [CLSCompliant(false)]
    public partial class J2NetCILGenerator : JavaBaseListener
    {
        static private string TAB = "    ";
        private StreamWriter IlCodeStream;
        string ilName;

        StringBuilder sb = new StringBuilder();

        public J2NetCILGenerator(Parser parser, string ilName2)
        {
            parser.AddParseListener(this); //upon instantiation, add this listener to a parser
            ilName = ilName2;
        }

        public void Start()
        {
            IlCodeStream = new StreamWriter(ilName + ".il", false);
            IlCodeStream.WriteLine(".assembly HelloIFN660\n{\n}\n");
            IlCodeStream.WriteLine(".class private auto ansi beforefieldinit " + "HelloIFN660.Program" + " extends [mscorlib]System.Object \n{");
            IlCodeStream.WriteLine(TAB + ".method private hidebysig static void  Main(string[] args) cil managed\n" + TAB + "{");
            IlCodeStream.WriteLine(TAB + TAB + " .entrypoint");
        }

        public void End()
        {
            IlCodeStream.WriteLine(TAB + TAB + "ret");
            IlCodeStream.WriteLine(TAB + "}");
            IlCodeStream.WriteLine("}");
            IlCodeStream.Flush();
            IlCodeStream.Close();
        }

        public override void EnterPackageDeclaration(JavaParser.PackageDeclarationContext context)
        {
            base.EnterPackageDeclaration(context);
            //".assembly HelloIFN660 {}"
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());

            // testing using GetChild()..
            //for (int i = 0; i < context.ChildCount; i++)
            //{
            //    Console.WriteLine(context.GetChild(i));
            //}

            sb.Append(context.GetText());
            sb.Append("\n");
        }

        public override void EnterClassDeclaration(JavaParser.ClassDeclarationContext context)
        {
            base.EnterClassDeclaration(context);
            //.class private auto ansi beforefieldinit HelloIFN660.Program extends [mscorlib]System.Object
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.normalClassDeclaration().classModifier(0).GetText()
                                                                     +context.normalClassDeclaration().CLASS().GetText()
                                                                     +context.normalClassDeclaration().Identifiers().GetText()+"{");

            sb.Append(context.normalClassDeclaration().classModifier(0).GetText()
                      +context.normalClassDeclaration().CLASS().GetText()
                      + context.normalClassDeclaration().Identifiers().GetText() + "{");
            sb.Append("\n");

        }

        //public override void EnterClassBodyDeclaration(JavaParser.ClassBodyDeclarationContext context)
        //{
        //    base.EnterClassBodyDeclaration(context);
        //    //{
        //    Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        //}


        public override void EnterConstructorDeclaration(JavaParser.ConstructorDeclarationContext context)
        {
            base.EnterConstructorDeclaration(context);
            //  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }
        public override void EnterConstructorBody(JavaParser.ConstructorBodyContext context)
        {
            base.EnterConstructorBody(context);
            //{
            //.maxstack  8
            //ldarg.0
            //call       instance void [mscorlib]System.Object::.ctor()
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }
        public override void ExitConstructorBody(JavaParser.ConstructorBodyContext context)
        {
            base.ExitConstructorBody(context);
            //ret
            //}
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
        }


        public override void EnterMethodDeclaration(JavaParser.MethodDeclarationContext context)
        {
            base.EnterMethodDeclaration(context);
            //.method private hidebysig static void  Main(string[] args) cil managed
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.methodModifier(0).GetText()+context.methodHeader().GetText()+"{");
            sb.Append(context.methodModifier(0).GetText()+context.methodHeader().GetText() + "{");
            sb.Append("\n");
        }
        public override void EnterMethodBody(JavaParser.MethodBodyContext context)
        {
            base.EnterMethodBody(context);
            //{
            //.entrypoint
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.block().blockStatements().GetText());
            sb.Append(context.block().blockStatements().GetText());
            sb.Append("\n");
        }
        public override void ExitMethodBody(JavaParser.MethodBodyContext context)
        {
            base.ExitMethodBody(context);
            //ret
            //}
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "}");
            sb.Append("}");
            sb.Append("\n");
        }


        public override void EnterLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
        {
            base.EnterLocalVariableDeclaration(context);
            //.maxstack  1 // <-- maybe count the number of variables then put that number for maxstack
            //.locals init ([0] int32 i)
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
        }


        public override void EnterStatement(JavaParser.StatementContext context)
        {
            base.EnterStatement(context);
            //ldc.i4.s   42
            //stloc.0
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
        }


        public override void EnterExpression(JavaParser.ExpressionContext context)
        {
            base.EnterExpression(context);
            //ldloc.0
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");

            IlCodeStream.WriteLine(TAB + TAB + ".maxstack  1");
            IlCodeStream.WriteLine(TAB + TAB + ".locals init ([0] int32 i)");
            IlCodeStream.WriteLine(TAB + TAB + "ldc.i4.s " + context.GetText());
            IlCodeStream.WriteLine(TAB + TAB + "stloc.0 ");
            IlCodeStream.WriteLine(TAB + TAB + "ldloc.0 ");
            IlCodeStream.WriteLine(TAB + TAB + "call " + TAB + "void [mscorlib]System.Console::WriteLine(int32)");
        }
        public override void ExitClassBodyDeclaration(JavaParser.ClassBodyDeclarationContext context)
        {
            base.ExitClassBodyDeclaration(context);
            //}
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "}");
            sb.Append("}");
            sb.Append("\n");
        }

        public StringBuilder printCIL()
        {
            return sb;
        }
    }

    /// <summary>
    /// Keeping this method separate so not to confuse this method with the overrides
    /// </summary>
    public partial class J2NetCILGenerator
    {
        private static void Log(String methodName, String contextText)
        {
            String msg = String.Format("{0}: {1}", methodName, contextText);
            Debug.WriteLine(msg);
        }
    }
}
