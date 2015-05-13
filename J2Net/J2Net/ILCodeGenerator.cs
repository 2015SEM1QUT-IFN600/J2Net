using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace J2Net
{
    class ILCodeGenerator
    {
        //The code of Intermediate Language. If someone needs to use the string of code, just using 
        private enum ILCode
        {
            [Description("ldci4.m1")]   ldc_i4_m1,      //Push -1 onto the stack as int32.
            [Description("ldc.i4.s  ")] ldc_i4_s,       //Push num onto the stack as int32 
            [Description("ldstr")]      ldstr,          //Loads the string on the stack.
            [Description("ldfld")]      ldfld,          //Loads field of an object
            [Description("ldarg")]      ldarg,          //Loads by-value argument. 
            [Description("ldarga")]     ldarga,         //Loads by-reference argument.
            [Description("stloc.")]     stloc_n,        //Pop a value from stack and store into local variable at index n.  	
            [Description("starg.")]     starg_n,        //Pop off the value from the stack ans store into the method argument at index n.
            [Description("pop")]        pop,            //Only Pops off the value from the stack  
            [Description("ret")]        ret,            //This instruction is used to exit a method and return a value to the caller. (if there is any)
            [Description("ldloc.")]     ldloc_x,        //Load local variable x on the stack.
            [Description("ldloca")]     ldloca,         //Load memory address of local variable.
            [Description("ldc.")]       ldc_aster,      //used to load constants of t ype int32,int62,float32,float64.
            [Description("br  ")]       br_target,      //Branch to target.The br instruction unconditionally transfers control to target.target is signed offset 4 bytes
            [Description("br.s  ")]     br_s_Target,    //Branch to target, short form.  Target is  represented as 1 byte 
            [Description("clt")]        clt,            //Compares less than. Returns 1 or 0
            [Description("blt  ")]      blt_target,     //The blt instruction transfers control to target if value1 is less than value2.
            [Description("bgt  ")]      bgt_target,     //The bgt  instruction transfers control to target if value1 is greter than value2. 
        }

        private static ILCodeGenerator instance = new ILCodeGenerator();

        public string getPushMinus1OntoStackAsInt32()
        {
            return this.getDescription(ILCode.ldc_i4_m1);
        }

        public string getPushNumOntoStackAsInt32(int Num)
        {
            return string.Format("{0} {1}", this.getDescription(ILCode.ldc_i4_s), Num);
        }

        public string getLoadStringOnTheStack(string str)
        {
            return string.Format("{0} {1}", this.getDescription(ILCode.ldstr), str);
        }

        public string getLoadFieldOfAnObject()
        {
            return this.getDescription(ILCode.ldfld);
        }

        public string getLoadByValueArgument()
        {
            return this.getDescription(ILCode.ldarg);
        }

        public string getLoadByReferenceArgument()
        {
            return this.getDescription(ILCode.ldarga);
        }

        //Return the IL command string.
        private string getDescription(ILCode code)
        {
            string description = code.ToString();

            if (code != null)
            {
                FieldInfo fieldInfo = code.GetType().GetField(description);
                DescriptionAttribute[] attribs = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attribs != null && attribs.Length > 0)
                    description = attribs[0].Description;
            }

            return description;
        }

        //Operating property
        public static ILCodeGenerator Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
