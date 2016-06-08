using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionVSDelegate
{
    class MethodClass
    {
        static int id = 0;

        public static int getNewId()
        {
            id++;
            return id;
        }

        public static string getNewValue(string propname)
        {
            int value;
            int.TryParse(propname.Substring(8, propname.Length - 8), out value);
            return "value"+value+"_"+id;
        }

        public static string getNewDouble(string propname)
        {
            double value;
            double.TryParse(propname.Substring(8, propname.Length - 8), out value);
            return (value * id).ToString();
        }
    }
}
