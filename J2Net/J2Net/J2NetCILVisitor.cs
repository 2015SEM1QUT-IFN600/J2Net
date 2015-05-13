using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using J2Net.Grammar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2Net
{
    public class J2NetCILVisitor : JavaBaseVisitor<string>
    {
        static private string TAB = "    ";
        private StreamWriter IlCodeStream;
        StringBuilder sb = new StringBuilder();
        string ilName;
        private bool exitLocalVariableDeclaration = false;
        private int localVariableDeclarationCounter = 0;
        private string localVariableDeclarationString = TAB + TAB + ".locals init (";

        public J2NetCILVisitor(string ilName2)
        {
            ilName = ilName2;

            this.buildJava2CSTypeMappnigList();
        }

        public override string VisitClassDeclaration(JavaParser.ClassDeclarationContext context)
        {
            IlCodeStream.WriteLine(".class " + context.normalClassDeclaration().classModifier(0).GetText() + " "
                      + context.normalClassDeclaration().Identifiers().GetText() + "\n{");

            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText()); 
            return base.VisitClassDeclaration(context);
        }

        public override string VisitClassBodyDeclaration(JavaParser.ClassBodyDeclarationContext context)
        {
            //Field declaration
            if (context.classMemberDeclaration().fieldDeclaration() != null)
            {
                string fieldString = TAB + ".field ";
                for (int i = 0; i < context.classMemberDeclaration().fieldDeclaration().fieldModifier().Count; i++)
                {
                    fieldString += context.classMemberDeclaration().fieldDeclaration().fieldModifier(i).GetText() + " ";
                }
                fieldString += typeRecognition(context.classMemberDeclaration().fieldDeclaration().unannType().GetText()) + " " +
                                context.classMemberDeclaration().fieldDeclaration().variableDeclaratorList().variableDeclarator(0).GetText();   //assuming only one variable is declared each time.
                IlCodeStream.WriteLine(fieldString);
            }

            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitClassBodyDeclaration(context);
        }

        public override string VisitMethodDeclaration(JavaParser.MethodDeclarationContext context)
        {
            IlCodeStream.Write(TAB + ".method ");
            for (int i = 0; i < context.methodModifier().Count; i++)
            {
                IlCodeStream.Write(context.methodModifier(i).GetText() + " ");
            }

            IlCodeStream.Write(this.typeRecognition(context.methodHeader().GetChild(0).GetText()) + " ");

            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitMethodDeclaration(context);
        }

        public override string VisitMethodDeclarator(JavaParser.MethodDeclaratorContext context)
        {
            string outContext = this.typeRecognition(context.GetText());
            IlCodeStream.WriteLine(outContext + "\n" + TAB + "{");
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, outContext);

            return base.VisitMethodDeclarator(context);
        }

        public override string VisitMethodBody(JavaParser.MethodBodyContext context)
        {
            IlCodeStream.WriteLine(TAB + TAB + ".entrypoint");
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitMethodBody(context);
        }

        public override string VisitLocalVariableDeclaration(JavaParser.LocalVariableDeclarationContext context)
        {
            //incase if localVariableDeclaration needs to be call again
            if (exitLocalVariableDeclaration == true)
            {
                exitLocalVariableDeclaration = false;
            }
            //Setting up string for .locals
            if (localVariableDeclarationCounter == 0)
            {
                localVariableDeclarationString += "[" + localVariableDeclarationCounter + "] " + typeRecognition(context.unannType().GetText()) + " " + context.variableDeclaratorList().variableDeclarator(0).variableDeclaratorId().GetText();
            }
            else
            {
                localVariableDeclarationString += ", [" + localVariableDeclarationCounter + "] " + typeRecognition(context.unannType().GetText()) + " " + context.variableDeclaratorList().variableDeclarator(0).variableDeclaratorId().GetText();
            }
            
            //adds a counter for maxstack usage
            localVariableDeclarationCounter++;
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitLocalVariableDeclaration(context);
        }

        public override string VisitStatement(JavaParser.StatementContext context)
        {
            //writes .locals and maxstack before performing statements
            if (exitLocalVariableDeclaration == false)
            {
                if (localVariableDeclarationCounter > 0) { 
                    exitLocalVariableDeclaration = true;
                    //IlCodeStream.WriteLine(TAB + TAB + ".maxstack " + localVariableDeclarationCounter);
                    //IlCodeStream.WriteLine(localVariableDeclarationString + ")");
                }
            }

            if (context.GetText().Contains("System.out.println"))
            {
                writeline();
            }

            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitStatement(context);
        }

        public override string VisitExpression(JavaParser.ExpressionContext context)
        {
            if (localVariableDeclarationCounter == 1)
            {
                sb.Append(TAB + TAB + "ldc.i4.s " + context.GetText() + "\n");

                // Hardcode! Needs to be put into logic. Just for testing purposes!
                sb.Append("stloc.0\n" + "ldloc.0\n" + "ldc.i4.1\n" + "add\n" + "stloc.1\n");
            }
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitExpression(context);
        }

        public override string VisitExpressionName(JavaParser.ExpressionNameContext context)
        {
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitExpressionName(context);
        }

        public void Start()
        {
            IlCodeStream = new StreamWriter(ilName + ".il", false);
            IlCodeStream.WriteLine(".assembly extern mscorlib\n{\n}\n");
            IlCodeStream.WriteLine(".assembly HelloIFN660\n{\n}\n");
        }

        public void End()
        {
            IlCodeStream.WriteLine(TAB + TAB + ".maxstack " + localVariableDeclarationCounter);
            IlCodeStream.WriteLine(localVariableDeclarationString + ")");
            IlCodeStream.WriteLine(sb);

            // Put a Console.ReadKey(); here just to see our output when running exe
            IlCodeStream.WriteLine(TAB + TAB + "call       valuetype [mscorlib]System.ConsoleKeyInfo [mscorlib]System.Console::ReadKey()");
            IlCodeStream.WriteLine(TAB + TAB + "pop");
            // Console.ReadKey(); ends

            IlCodeStream.WriteLine(TAB + TAB + "ret");
            IlCodeStream.WriteLine(TAB + "}");
            IlCodeStream.WriteLine("}");
            IlCodeStream.Flush();
            IlCodeStream.Close();
        }

        // System.out.println -> Console.Out.WriteLine. ATTN: Hardcode. Should have more logic. e.g. ldloc.1
        public void writeline()
        {
            sb.Append(TAB + TAB + "call       class [mscorlib]System.IO.TextWriter [mscorlib]System.Console::get_Out()\n");
            sb.Append(TAB + TAB + "ldloc.1\n");
            sb.Append(TAB + TAB + "callvirt   instance void [mscorlib]System.IO.TextWriter::WriteLine(int32)\n");
        }

        /// <summary>
        /// Keeping this method separate so not to confuse this method with the overrides
        /// </summary>
        private static void Log(String methodName, String contextText)
        {
            String msg = String.Format("{0}: {1}", methodName, contextText);
            Debug.WriteLine(msg);
        }

        //Name Mapping: If someone needs to add a new type for converting, please just adding it into JavaTypeList and CSType in sequence.
        Dictionary<string, string> Java2CSTypeMappingList = new Dictionary<string, string>();
        string[] JavaTypeList = { "int", "float", "unsigned int", "String" };
        string[] CSType = { "int32", "float32", "unsigned int32", "string" };

        private void buildJava2CSTypeMappnigList()
        {
            for (int i = 0; i < JavaTypeList.Length; i++)
            {
                this.Java2CSTypeMappingList.Add(JavaTypeList[i], CSType[i]);
            }
        }

        public string typeRecognition(string type)
        {
            string rtnType = "";
            if (!this.Java2CSTypeMappingList.TryGetValue(type, out rtnType))
            {
                //For bug fixing of main function
                if (type.IndexOf("String[]") > 0)
                    type = type.Replace("String[]", "string[]");

                return type;
            }

            return rtnType;
        }
    }
}
