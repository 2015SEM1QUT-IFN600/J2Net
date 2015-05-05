using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2Net.Grammar;
using Antlr4.Runtime;
using System.Diagnostics;

namespace J2Net
{
    [CLSCompliant(false)]
    public partial class J2NetCILGenerator : JavaBaseListener
    {
        StringBuilder sb = new StringBuilder();
        J2NetNameBinder binder = new J2NetNameBinder();
        Hashtable Symtable = new Hashtable();
        
        public J2NetCILGenerator(Parser parser)
        {
            parser.AddParseListener(this); //upon instantiation, add this listener to a parser
        }

        public override void EnterPackageDeclaration(JavaParser.PackageDeclarationContext context)
        {
            base.EnterPackageDeclaration(context);
            //".assembly HelloIFN660 {}"
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
            
        }


        public override void EnterClassDeclaration(JavaParser.ClassDeclarationContext context)
        {
            base.EnterClassDeclaration(context);
            //.class private auto ansi beforefieldinit HelloIFN660.Program extends [mscorlib]System.Object
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
           binder.SymbolTable(context.normalClassDeclaration().Identifiers().GetText(),"-"); 
            //push class name into hashtable
        }

       
        public override void EnterClassBodyDeclaration(JavaParser.ClassBodyDeclarationContext context)
        {
            base.EnterClassBodyDeclaration(context);
            //{
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
                     
            
            
        }
        public override void ExitClassBodyDeclaration(JavaParser.ClassBodyDeclarationContext context)
        {
            base.ExitClassBodyDeclaration(context);
            //}
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
        }


        public override void EnterConstructorDeclaration(JavaParser.ConstructorDeclarationContext context)
        {
            base.EnterConstructorDeclaration(context);
            //  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
        }
        public override void EnterConstructorBody(JavaParser.ConstructorBodyContext context)
        {
            base.EnterConstructorBody(context);
            //{
            //.maxstack  8
            //ldarg.0
            //call       instance void [mscorlib]System.Object::.ctor()
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
        }
        public override void ExitConstructorBody(JavaParser.ConstructorBodyContext context)
        {
            base.ExitConstructorBody(context);
            //ret
            //}
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
        }


        public override void EnterMethodDeclaration(JavaParser.MethodDeclarationContext context)
        {
            base.EnterMethodDeclaration(context);
            //.method private hidebysig static void  Main(string[] args) cil managed
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
            binder.SymbolTable(context.methodHeader().methodDeclarator().Identifiers().GetText(),"-");
            //push method name into hashtable
        }
        public override void EnterMethodBody(JavaParser.MethodBodyContext context)
        {
            base.EnterMethodBody(context);
            //{
            //.entrypoint
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
            
        }
        public override void ExitMethodBody(JavaParser.MethodBodyContext context)
        {
            base.ExitMethodBody(context);
            //ret
            //}
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
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
            binder.SymbolTable(context.variableDeclaratorList().GetText(),"int");
            //push identifeir name into hashtable, this section needs improvement, i am still pushing datatype manually.need help to find out the data type of identifier
            
           
        }

        public override void ExitLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
        {
            base.ExitLocalVariableDeclaration(context);
        }


       public override void EnterStatement(JavaParser.StatementContext context)
        {
            base.EnterStatement(context);
            //ldc.i4.s   42
            //stloc.0
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
            //this part should return me left hand side value and right hand side value but no luck.
        }


        public override void EnterExpression(JavaParser.ExpressionContext context)
        {
            base.EnterExpression(context);
            //ldloc.0
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
           // Console.WriteLine(context.assignmentExpression().GetText());
           
        }


        
            


       
        

        public StringBuilder printCIL()
        {
            binder.printHash();
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
