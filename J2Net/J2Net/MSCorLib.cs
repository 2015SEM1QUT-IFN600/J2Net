using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace J2Net.IL
{
    public class MSCorLib
    {
        private enum CorLib
        {
            [Description("void [mscorlib]System.Console::WriteLine")]
            WriteLine,

            [Description("[mscorlib]System.Object")]
            System_Object,
        }


        private static MSCorLib instance = new MSCorLib();

        public string getSystem_Object()
        {
            return this.getDescription(CorLib.System_Object);
        }

        public string getConsoleWriteLine(string data)
        {
            return string.Format("{0}({1})", this.getDescription(CorLib.WriteLine), data);
        }

        //Return the IL command string.
        private string getDescription(CorLib code)
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

        public static MSCorLib Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
