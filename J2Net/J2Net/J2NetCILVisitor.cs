using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using J2Net.Grammar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace J2Net
{
    [CLSCompliant(false)]
    public class J2NetCILVisitor : JavaBaseVisitor<string>
    {
        private ScopeStack ss;

        static private string TAB = "    ";
        StringBuilder sb = new StringBuilder();
        private bool exitLocalVariableDeclaration = false;
        private int localVariableDeclarationCounter = 0;
        private string localVariableDeclarationString = TAB + TAB + ".locals init (";

        public J2NetCILVisitor(ScopeStack scopeStack)
        {
            ss = scopeStack;
            Start();

            this.buildJava2CSTypeMappnigList();

        }

        public struct matchString {
            public string matchType;
            public int matchIndex;

            public matchString(string s, int i)
            {
                matchType = s;
                matchIndex = i;
            }
        }

        public StringBuilder ToStringBuilder()
        {
            End();
            //return new StringBuilder("# # # # # # # # # # # # # # # # # # # # # #\nChange J2NetVisitor: Remove All StreamWriter references and use a StringBuilder instead. That way you can view the output in a console and write to a file.\n# # # # # # # # # # # # # # # # # # # # # #");
            return sb;
        }

        public override string VisitClassDeclaration(JavaParser.ClassDeclarationContext context)
        {
            sb.Append(".class " + context.normalClassDeclaration().classModifier(0).GetText() + " "
                      + context.normalClassDeclaration().Identifiers().GetText() + "\n{\n");

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
                sb.Append(fieldString + "\n");
            }

            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitClassBodyDeclaration(context);
        }

        public override string VisitMethodDeclaration(JavaParser.MethodDeclarationContext context)
        {
            sb.Append(TAB + ".method ");
            for (int i = 0; i < context.methodModifier().Count; i++)
            {
                sb.Append(context.methodModifier(i).GetText() + " ");
            }

            sb.Append(this.typeRecognition(context.methodHeader().GetChild(0).GetText()) + " ");

            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitMethodDeclaration(context);
        }

        public override string VisitMethodDeclarator(JavaParser.MethodDeclaratorContext context)
        {
            string outContext = this.typeRecognition(context.GetText());
            sb.Append(outContext + "\n" + TAB + "{\n");
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, outContext);

            return base.VisitMethodDeclarator(context);
        }

        public override string VisitMethodBody(JavaParser.MethodBodyContext context)
        {
            sb.Append(TAB + TAB + ".entrypoint\n");
            //sb.Append(TAB + TAB + ".maxstack " +  ss.Count(ScopeStack.Kind.VARIABLE, "main") + "\n"); <- changing back to the old one for now due to .local not recgonizing the scope it is in.

            ///
            /// Problem with the current .local is that it can't determine which scope it is in, and so, it reads everything from the current method it is in.
            /// If someone have a better way of doing it, please feel free to fix the code.
            ///
            //type array
            string[] typeArray = new string[] { "int", "double", "byte", "short", "char", "long", "float", "string", "bool" };
            //holds match type and index position
            List<matchString> matchString = new List<matchString>();
            string tempString = String.Join("|", typeArray);
            //finding types and their index position
            foreach (Match m in Regex.Matches(context.GetText(), @"\b"+tempString+"\b"))
            {
                matchString.Add(new matchString(m.Value,m.Index));
            }
            //getting the variable names
            for (int i = 0; i < matchString.Count; i++)
            {
                bool found = false;
                int size = 0;   //size of the variable name
                while (!found)
                {
                    if (context.GetText()[matchString[i].matchIndex + matchString[i].matchType.Length + size].Equals(';') ||
                        context.GetText()[matchString[i].matchIndex + matchString[i].matchType.Length + size].Equals('='))
                    {
                        found = true;
                    }
                    else
                    {
                        size++;
                    }
                }
                if (exitLocalVariableDeclaration == true)
                {
                    exitLocalVariableDeclaration = false;
                }
                //Setting up string for .locals
                if (localVariableDeclarationCounter == 0)
                {
                    localVariableDeclarationString += "[" + localVariableDeclarationCounter + "] " + typeRecognition(matchString[i].matchType) + " " + context.GetText().Substring(matchString[i].matchIndex + matchString[i].matchType.Length, size);
                }
                else
                {
                    localVariableDeclarationString += ", [" + localVariableDeclarationCounter + "] " + typeRecognition(matchString[i].matchType) + " " + context.GetText().Substring(matchString[i].matchIndex + matchString[i].matchType.Length, size);
                }
                localVariableDeclarationCounter++;
            }
            sb.Append(TAB + TAB + ".maxstack " + localVariableDeclarationCounter + "\n");   //<- changing back to this for now due to .local not recgonizing the scope it is in.
            sb.Append(localVariableDeclarationString + ")\n"); //<-- this needs to be fixed or put somewhere else, no variables in there currently
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
                sb.Append(TAB + TAB + "stloc.0\n" + TAB + TAB + "ldloc.0\n" + TAB + TAB + "ldc.i4.1\n" + TAB + TAB + "add\n" + TAB + TAB + "stloc.1\n");
            }
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitExpression(context);
        }

        public override string VisitExpressionName(JavaParser.ExpressionNameContext context)
        {
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitExpressionName(context);
        }

        private void Start()
        {
            sb.Append(".assembly extern mscorlib\n{\n}\n");
            sb.Append(".assembly HelloIFN660\n{\n}\n");
        }

        private void End()
        {
            // Put a Console.ReadKey(); here just to see our output when running exe
            sb.Append(TAB + TAB + "call       valuetype [mscorlib]System.ConsoleKeyInfo [mscorlib]System.Console::ReadKey()\n");
            sb.Append(TAB + TAB + "pop\n");
            // Console.ReadKey(); ends

            sb.Append(TAB + TAB + "ret\n");
            sb.Append(TAB + "}\n");
            sb.Append("}");
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
