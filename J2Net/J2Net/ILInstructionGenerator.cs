using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace J2Net.IL
{
    public class ILInstructionGenerator
    {
        //The code of Intermediate Language. If someone needs to use the string of code, just using 
        private enum ILInstruction
        {
            [Description("ldci4.m1")]       ldc_i4_m1,      //Push -1 onto the stack as int32.
            [Description("ldc.i4.s  ")]     ldc_i4_s,       //Push num onto the stack as int32 
            [Description("ldstr")]          ldstr,          //Loads the string on the stack.
            [Description("ldfld")]          ldfld,          //Loads field of an object
            [Description("ldarg")]          ldarg,          //Loads by-value argument. 
            [Description("ldarga")]         ldarga,         //Loads by-reference argument.
            [Description("stloc.")]         stloc_n,        //Pop a value from stack and store into local variable at index n.  	
            [Description("starg.")]         starg_n,        //Pop off the value from the stack ans store into the method argument at index n.
            [Description("pop")]            pop,            //Only Pops off the value from the stack  
            [Description("ret")]            ret,            //This instruction is used to exit a method and return a value to the caller. (if there is any)
            [Description("ldloc.")]         ldloc_x,        //Load local variable x on the stack.
            [Description("ldloca")]         ldloca,         //Load memory address of local variable.
            [Description("ldc.")]           ldc_aster,      //used to load constants of t ype int32,int62,float32,float64.
            [Description("br  ")]           br_target,      //Branch to target.The br instruction unconditionally transfers control to target.target is signed offset 4 bytes
            [Description("br.s  ")]         br_s_Target,    //Branch to target, short form.  Target is  represented as 1 byte 
            [Description("clt")]            clt,            //Compares less than. Returns 1 or 0
            [Description("blt  ")]          blt_target,     //The blt instruction transfers control to target if value1 is less than value2.
            [Description("bgt  ")]          bgt_target,     //The bgt  instruction transfers control to target if value1 is greter than value2. 
            [Description("call")]           call,           //Call method described by method
            [Description("nop")]            nop,            //Do nothing (No Operation)

            [Description(".maxstack ")]     maxstack,       //
            [Description(".entrypoint")]    entrypoint,     //
            [Description(".locals init")]   locals,         //Declare local variable
            [Description(".field")]         field,          //Declare data member
            [Description(".method")]        method,         //Declare method
            [Description(".class")]         klass,          //Declare class
            [Description("extends")]        extends,        //Declare extends


            [Description("{")]  BracketBegin,
            [Description("}")]  BracketBnd,
            [Description("[")]  SquareBracketBegin,
            [Description("]")]  SquareBracketEnd,
            [Description("(")]  FunctionBracketBegin,
            [Description(")")]  FunctionBracketEnd,
            [Description("<")]  LessThan,
            [Description(">")]  GreaterThan,
        }

        private static const string DEFAULT_EXTENDS = MSCorLib.Instance.getSystem_Object();


        private static ILInstructionGenerator instance = new ILInstructionGenerator();

        public string getBracketBegin()
        {
            return this.getDescription(ILInstruction.BracketBegin);
        }

        public string getBracketEnd()
        {
            return this.getDescription(ILInstruction.BracketBnd);
        }

        public string getSquareBracketBegin()
        {
            return this.getDescription(ILInstruction.SquareBracketBegin);
        }

        public string getSquareBracketEnd()
        {
            return this.getDescription(ILInstruction.SquareBracketEnd);
        }

        public string getFunctionBracketBegin()
        {
            return this.getDescription(ILInstruction.FunctionBracketBegin);
        }

        public string getFunctionBracketEnd()
        {
            return this.getDescription(ILInstruction.FunctionBracketEnd);
        }

        public string getLessThanBracket()
        {
            return this.getDescription(ILInstruction.LessThan);
        }

        public string getGreatThanBracket()
        {
            return this.getDescription(ILInstruction.GreaterThan);
        }

        public string getNoOperation()
        {
            return this.getDescription(ILInstruction.nop);
        }

        public string getDeclareClass(string accessability, string nameSpace, string name, string extends)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0} ", this.getDescription(ILInstruction.klass)));
            sb.Append(string.Format("{0} ", accessability));

            if (nameSpace.Length > 0)
                sb.Append(string.Format("{0}.", nameSpace));

            sb.Append(string.Format("{0} ", name));

            sb.Append(string.Format("{0} ", this.getDescription(ILInstruction.extends)));
            sb.Append(string.Format("{0}", (extends.Length > 0) ? extends : DEFAULT_EXTENDS));

            return sb.ToString();
        }

        public string getDeclareMethod(string accessability, string type, string returnType, string name, string args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0} ", this.getDescription(ILInstruction.method)));
            sb.Append(string.Format("{0} ", accessability));

            if (type.Length > 0)
                sb.Append(string.Format("{0} ", type));

            sb.Append(string.Format("{0} ", returnType));
            sb.Append(string.Format("{0} ", name));
            sb.Append(args);

            return sb.ToString();
        }

        public string getDeclareDataMember(string accessability, string type, string variable)
        {

            return string.Format("{0} {1} {2} {3}", this.getDescription(ILInstruction.field), accessability, type, variable);
        }

        public string getDeclareLocalVariable(string[] types, string[] variables)
        {
            StringBuilder sb = new StringBuilder();
            string connFormat = ", [{0}] {1} {2}";
            string headFormat = "[{0}] {1} {2}";
            string format;

            for (int i = 0; i < variables.Length; i++)
            {
                format = (i == 0) ? headFormat : connFormat;
                sb.Append(string.Format(format, i, types[i], variables[i]));
            }

            return string.Format("{0} ({1})", this.getDescription(ILInstruction.locals), sb.ToString());
        }

        public string getMaxStack(int variableNum)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.maxstack), variableNum);
        }

        public string getPushMinus1OntoStackAsInt32()
        {
            return this.getDescription(ILInstruction.ldc_i4_m1);
        }

        public string getEntryPoint()
        {
            return this.getDescription(ILInstruction.entrypoint);
        }

        public string getPushNumOntoStackAsInt32(int Num)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.ldc_i4_s), Num);
        }

        public string getLoadStringOnStack(string str)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.ldstr), str);
        }

        public string getLoadFieldOfAnObject()
        {
            return this.getDescription(ILInstruction.ldfld);
        }

        public string getLoadByValueArgument()
        {
            return this.getDescription(ILInstruction.ldarg);
        }

        public string getLoadByReferenceArgument()
        {
            return this.getDescription(ILInstruction.ldarga);
        }

        public string getPopValueFromStackAndStoreIntoLocalVariable(int index)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.stloc_n), index);
        }

        public string getPopOffValueFromStackAsStoreIntoMethodArgument(int index)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.starg_n), index);
        }

        public string getOnlyPopOffValueFromStack()
        {
            return this.getDescription(ILInstruction.pop);
        }

        public string getExitMethodAndReturnValueToCaller(string value)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.ret), value);
        }

        public string getLoadLocalVariableOnTheStack(string variable)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.ldloc_x), variable);
        }

        public string getLoadMemoryAddressOfLocalVariable()
        {
            return this.getDescription(ILInstruction.ldloca);
        }

        public string getLoadConstants(int constants)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.ldc_aster), constants);
        }

        public string getBranchToTarget(string target)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.br_target), target);
        }

        public string getBranchToTargetShortForm(string target)
        {
            return string.Format("{0} {1}", this.getDescription(ILInstruction.br_s_Target), target);
        }

        public string getCompareLessThan()
        {
            return this.getDescription(ILInstruction.clt);
        }

        public string getBLTInstructionTransferControl()
        {
            return this.getDescription(ILInstruction.blt_target);
        }

        public string getBGTInstructionTransferControl()
        {
            return this.getDescription(ILInstruction.bgt_target);
        }

        //Return the IL command string.
        private string getDescription(ILInstruction code)
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
        public static ILInstructionGenerator Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
