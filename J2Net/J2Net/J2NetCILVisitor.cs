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
        string ilName;

        public J2NetCILVisitor(string ilName2)
        {
            ilName = ilName2;
        }

        public override string VisitPackageDeclaration(JavaParser.PackageDeclarationContext context)
        {
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText()); 
            return base.VisitPackageDeclaration(context);
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
                string fieldString = ".field ";
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

            IlCodeStream.Write(context.methodHeader().GetChild(0).GetText() + " ");

            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitMethodDeclaration(context);
        }

        public override string VisitMethodDeclarator(JavaParser.MethodDeclaratorContext context)
        {
            IlCodeStream.WriteLine(context.GetText() + "\n" + TAB + "{");
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitMethodDeclarator(context);
        }

        public override string VisitMethodBody(JavaParser.MethodBodyContext context)
        {
            IlCodeStream.WriteLine(TAB + TAB + ".entrypoint");
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitMethodBody(context);
        }

        public override string VisitVariableDeclarator(JavaParser.VariableDeclaratorContext context)
        {
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitVariableDeclarator(context);
        }

        public override string VisitVariableDeclaratorList(JavaParser.VariableDeclaratorListContext context)
        {
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitVariableDeclaratorList(context);
        }

        public override string VisitStatement(JavaParser.StatementContext context)
        {
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText());
            return base.VisitStatement(context);
        }

        public override string VisitExpression(JavaParser.ExpressionContext context)
        {
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
            IlCodeStream.WriteLine(TAB + TAB + "ret");
            IlCodeStream.WriteLine(TAB + "}");
            IlCodeStream.WriteLine("}");
            IlCodeStream.Flush();
            IlCodeStream.Close();
        }

        /// <summary>
        /// Keeping this method separate so not to confuse this method with the overrides
        /// </summary>
        private static void Log(String methodName, String contextText)
        {
            String msg = String.Format("{0}: {1}", methodName, contextText);
            Debug.WriteLine(msg);
        }

        public string typeRecognition(string type)
        {
            string temp = "";
            if (type.Equals("int") || type.Equals("float") || type.Equals("unsigned int"))
            {
                if (type.Equals("int"))
                {
                    temp = "int32";
                }
                else if (type.Equals("float"))
                {
                    temp = "float32";
                }
                else if (type.Equals("unsigned int"))
                {   //don't think this is ever needed for java
                    temp = "unsigned int32";
                }
            }
            else
            {
                temp = type;
            }
            return temp;
        }
    }
}
