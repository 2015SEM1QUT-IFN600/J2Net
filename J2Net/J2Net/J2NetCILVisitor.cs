using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using J2Net.Grammar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2Net
{
    public class J2NetCILVisitor : JavaBaseVisitor<int>
    {
        public override int VisitPackageDeclaration(JavaParser.PackageDeclarationContext context)
        {
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText()); 
            return base.VisitPackageDeclaration(context);
        }

        public override int VisitClassDeclaration(JavaParser.ClassDeclarationContext context)
        {
            Log(System.Reflection.MethodBase.GetCurrentMethod().Name, context.GetText()); 
            return base.VisitClassDeclaration(context);
        }

        /// <summary>
        /// Keeping this method separate so not to confuse this method with the overrides
        /// </summary>
        private static void Log(String methodName, String contextText)
        {
            String msg = String.Format("{0}: {1}", methodName, contextText);
            Debug.WriteLine(msg);
        }
    }
}
