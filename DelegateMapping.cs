using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionVSDelegate
{
    class DelegateMapping
    {
        private static Dictionary<String, PropertyInfo> PropInfoCache = new Dictionary<string, PropertyInfo>();
        private static Dictionary<String, KeyValuePair<int, Delegate>> DelegateCache = new Dictionary<string, KeyValuePair<int, Delegate>>(); 

        public void Map(Entity en, Dictionary<string, string[]> kvps, bool usecache)
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
                if(usecache)
                    setByMethodCache(en, prop, kvp.Value[0], kvp.Value[1], prop.Name);
                else
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

        void setByMethodCache(Entity en, PropertyInfo prop, string classname, string methodname, string param)
        {
            KeyValuePair<int, Delegate> cached;
            
            if (!DelegateCache.TryGetValue(methodname, out cached))
            {
                //Get class
                Type type = Type.GetType(classname);
                MethodInfo info = type.GetMethod(methodname);

                if (info.GetParameters().Length == 0)
                {
                    cached = new KeyValuePair<int,Delegate>(0, Delegate.CreateDelegate(typeof(Func<int>), info));
                    DelegateCache.Add(methodname, cached);
                }
                else
                {
                    cached = new KeyValuePair<int,Delegate>(1, Delegate.CreateDelegate(typeof(Func<string, string>), info));
                    DelegateCache.Add(methodname, cached);
                }
            }

            Func<int> zero;
            Func<string, string> one;
            string value;
            if (cached.Key == 0)
            {
                zero = (Func<int>)cached.Value;
                value = zero().ToString();
            }
            else
            {
                one = (Func<string, string>)cached.Value;
                value = one(param);
            }
            setByValue(en, prop, value);
        }

        void setByMethod(Entity en, PropertyInfo prop, string classname, string methodname, string param)
        {
            KeyValuePair<int, Delegate> cached;
                //Get class
                Type type = Type.GetType(classname);
                MethodInfo info = type.GetMethod(methodname);

                if (info.GetParameters().Length == 0)
                {
                    cached = new KeyValuePair<int, Delegate>(0, Delegate.CreateDelegate(typeof(Func<int>), info));
                }
                else
                {
                    cached = new KeyValuePair<int, Delegate>(1, Delegate.CreateDelegate(typeof(Func<string, string>), info));
                }

            Func<int> zero;
            Func<string, string> one;
            string value;
            if (cached.Key == 0)
            {
                zero = (Func<int>)cached.Value;
                value = zero().ToString();
            }
            else
            {
                one = (Func<string, string>)cached.Value;
                value = one(param);
            }
            setByValue(en, prop, value);
        }
    }
}
