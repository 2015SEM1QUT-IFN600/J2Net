using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace J2Net.IL
{
    public class ILCodeGenerator
    {
        private static ILCodeGenerator instance = new ILCodeGenerator();

        //Ref:http://stackoverflow.com/questions/6529647/what-does-code-size-in-msil-mean
        //Code size is depending on how many bytes used in the stack. Format:IL_000x


        
        public string getMainFunction(int maxStackNum)
        {
            StringBuilder sb = new StringBuilder();

            string[] paraTypes  = { "string []" };
            string[] paras      = { "args" };

            sb.AppendLine(ILInstructionGenerator.Instance.getDeclareMethod("private", "", "void", "Main", paraTypes, paras));
            sb.AppendLine(ILInstructionGenerator.Instance.getBracketBegin());
            sb.AppendLine(ILInstructionGenerator.Instance.getEntryPoint());
            sb.AppendLine(ILInstructionGenerator.Instance.getMaxStack(maxStackNum));

            //Declare local variable if there is.


            //Present the input tree


            sb.AppendLine(ILInstructionGenerator.Instance.getNoOperation());
            sb.AppendLine(ILInstructionGenerator.Instance.getExitMethodAndReturnValueToCaller());
            sb.AppendLine(ILInstructionGenerator.Instance.getBracketEnd());
            return sb.ToString();
        }

        public string getFunction(string accesibility, string keywords, string returnType, string name, string[] paraTypes, string[] paras)
        {
            return "";
        }



        private int getCodeSize()
        {
            return 0;
        }

        public static ILCodeGenerator Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
