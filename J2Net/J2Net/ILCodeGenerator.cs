using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2Net.IL
{
    class ILCodeGenerator
    {
        private static ILCodeGenerator instance = new ILCodeGenerator();

        //Ref:http://stackoverflow.com/questions/6529647/what-does-code-size-in-msil-mean
        //Code size is depending on how many bytes used in the stack. Format:IL_000x


        public static ILCodeGenerator Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
