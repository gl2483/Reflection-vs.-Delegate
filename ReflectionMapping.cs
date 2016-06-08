using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionVSDelegate
{
    class ReflectionMapping
    {
        private static Dictionary<String, PropertyInfo> PropInfoCache = new Dictionary<string, PropertyInfo>(); 

        public void Map(Entity en, Dictionary<string, string[]> kvps)
        {
            PropertyInfo prop;
            foreach (KeyValuePair<string, string[]> kvp in kvps)
            {
                //get property in cache
                if (!PropInfoCache.TryGetValue(kvp.Key, out prop))
                {
                    //get property
                    prop = en.GetType().GetProperty(kvp.Key);
                    //put property in cache
                    PropInfoCache.Add(kvp.Key, prop);
                }

                setByMethod(en, prop, kvp.Value[0], kvp.Value[1], prop.Name);
            }
        }

        void setByValue(Entity en, PropertyInfo prop, string value)
        {
            string original = prop.GetValue(en) as string;
            if (!string.IsNullOrEmpty(original))
                value = original + " " + value;
            prop.SetValue(en, Convert.ChangeType(value, prop.PropertyType));
        }

        void setByMethod(Entity en, PropertyInfo prop, string classname, string methodname, string param)
        {
            //Get class
            Type type = Type.GetType(classname);
            MethodInfo info = type.GetMethod(methodname);

            if (info != null)
            {
                object result = null;
                //get parameters
                ParameterInfo[] parameters = info.GetParameters();
                object classInstance = Activator.CreateInstance(type, null);

                //Invoke method call
                if (parameters.Length == 0)
                {
                    result = info.Invoke(classInstance, null);
                }
                else
                {
                    object[] paramArray = new object[] { param };
                    result = info.Invoke(classInstance, paramArray);
                }

                //set the property to the return value from method
                string val = result.ToString();
                setByValue(en, prop, val);
            }
        }
    }
}
