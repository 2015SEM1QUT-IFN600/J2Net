using System;
using System.Collections.Generic;
using System.Collections;
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
        List<variableDeclaration> localVariableDeclarationList = new List<variableDeclaration>();

        public struct variableDeclaration {
            public string type;
            public string name;

            public variableDeclaration(string t, string n) {
                type = t;
                name = n;
            }
        }

        StringBuilder sb = new StringBuilder();
        J2NetNameBinder binder = new J2NetNameBinder();
        Hashtable Symtable = new Hashtable();

        public J2NetCILGenerator(Parser parser, string ilName2)
        {
            parser.AddParseListener(this); //upon instantiation, add this listener to a parser
            ilName = ilName2;
        }

        public void Start()
        {
            IlCodeStream = new StreamWriter(ilName + ".il", false);
            IlCodeStream.WriteLine(".assembly extern mscorlib\n{\n}\n");
            IlCodeStream.WriteLine(".assembly HelloIFN660\n{\n}\n");
        }

        public void End()
        {
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
//----------------------------------------------------------------------------------
//BUG:This causes error while ilasm.exe tries to build a executable file.
//                      +context.normalClassDeclaration().CLASS().GetText()
//----------------------------------------------------------------------------------
                      + context.normalClassDeclaration().Identifiers().GetText() + "{");
            sb.Append("\n");

            IlCodeStream.WriteLine(".class " + context.normalClassDeclaration().classModifier(0).GetText() + " "
//----------------------------------------------------------------------------------
//BUG:This causes error while ilasm.exe tries to build a executable file.
//                      + context.normalClassDeclaration().CLASS().GetText() + " "
//----------------------------------------------------------------------------------
                      + context.normalClassDeclaration().Identifiers().GetText() + "\n{");
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

            IlCodeStream.Write(TAB + ".method ");
            for (int i = 0; i < context.methodModifier().Count; i++)
            {
                IlCodeStream.Write(context.methodModifier(i).GetText() + " ");
            }

            for (int i = 0; i < context.methodHeader().ChildCount-1; i++) // -1 to not print main(String[]args)
            {
                IlCodeStream.Write(context.methodHeader().GetChild(i).GetText() + " ");
            }
            IlCodeStream.WriteLine("main(string[]args)\n" + TAB + "{"); // Hardcoded until fixed...
            binder.SymbolTable(context.methodHeader().methodDeclarator().Identifiers().GetText(),"-");
            //push method name into hashtable
        }
        public override void EnterMethodBody(JavaParser.MethodBodyContext context)
        {
            base.EnterMethodBody(context);
            //{
            //.entrypoint
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.block().blockStatements().GetText());
            sb.Append(context.block().blockStatements().GetText());
            sb.Append("\n");
            IlCodeStream.WriteLine(TAB + TAB + ".entrypoint");
        }
        public override void ExitMethodBody(JavaParser.MethodBodyContext context)
        {
            base.ExitMethodBody(context);
            //ret
            //}
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "}");
            sb.Append("}");
            sb.Append("\n");
            IlCodeStream.WriteLine(TAB + TAB + "ret");
            IlCodeStream.WriteLine(TAB + "}");
        }


        public override void EnterLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
        {
            base.EnterLocalVariableDeclaration(context);
            //.maxstack  1 // <-- maybe count the number of variables then put that number for maxstack
            //.locals init ([0] int32 i)
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            sb.Append(context.GetText());
            sb.Append("\n");
            binder.SymbolTable(context.variableDeclaratorList().GetText(),context.GetChild(0).GetText());
            //push identifeir name into hashtable, this section needs improvement, i am still pushing datatype manually.need help to find out the data type of identifier
            
           
        }

        public override void ExitLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
        {
            base.ExitLocalVariableDeclaration(context);
            //Do not do the following over here. if you have more than 1 variable to declare, you will print multiple .maxstack and .locals
            //IlCodeStream.WriteLine(TAB + TAB + ".maxstack  1");
            //IlCodeStream.WriteLine(TAB + TAB + ".locals init ([0] int32 i)");
            
            //first value is type, second value is name
            localVariableDeclarationList.Add(new variableDeclaration(context.GetChild(0).GetText(), context.GetChild(1).GetText()));
            
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
            sb.Append("\n");

            //Should not be doing this over here too, as it will call multiple generations. Need a better place to put these code
            //Create .maxstack. Set to defaul value '8' if localVariableDeclarationList is 0
            if (localVariableDeclarationList.Count == 0)
            {
                IlCodeStream.WriteLine(TAB + TAB + ".maxstack 8");
            }
            else
            {    //localVariableDeclartionList != 0 and creates Local
                IlCodeStream.WriteLine(TAB + TAB + ".maxstack " + localVariableDeclarationList.Count);
                //creates Local
                string tempString = "(";
                for (int i = 0; i < localVariableDeclarationList.Count; i++)
                {
                    tempString += "[" + i + "] " + typeRecognition(localVariableDeclarationList[i].type) + " " + localVariableDeclarationList[i].name;
                    if (i != localVariableDeclarationList.Count - 1)
                    {
                        tempString += ", ";
                    }
                    else
                    {
                        tempString += ")";
                    }
                }
                IlCodeStream.WriteLine(TAB + TAB + ".locals init " + tempString);
            }
            //need to clear localVariableDeclarationList for further usage?
            localVariableDeclarationList.Clear();

            IlCodeStream.WriteLine(TAB + TAB + "ldc.i4.s " + context.GetText());
            IlCodeStream.WriteLine(TAB + TAB + "stloc.0 ");
            //IlCodeStream.WriteLine(TAB + TAB + "ldloc.0 ");
            //IlCodeStream.WriteLine(TAB + TAB + "call " + TAB + "void [mscorlib]System.Console::WriteLine(int32)");
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
            binder.printHash();
            return sb;
        }

        public string typeRecognition (string type) {
            string temp = "";
            if (type.Equals("int") || type.Equals("float") || type.Equals("unsigned int")) {
                if (type.Equals("int")) {
                    temp = "int32";
                } else if (type.Equals("float")){
                    temp = "float32";
                } else if (type.Equals("unsigned int")) {   //don't think this is ever needed for java
                    temp = "unsigned int32";
                }
            } else {
                temp = type;
            }
            return temp;
        }
    }

     <summary>
     Keeping this method separate so not to confuse this method with the overrides
     </summary>
    public partial class J2NetCILGenerator
    {
        private static void Log(String methodName, String contextText)
        {
            String msg = String.Format("{0}: {1}", methodName, contextText);
            Debug.WriteLine(msg);
        }
    }
}
