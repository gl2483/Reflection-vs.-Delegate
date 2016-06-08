using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ReflectionVSDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string[]> mappings = new Dictionary<string, string[]>();
            mappings.Add("EntityId", new string[] {"ReflectionVSDelegate.MethodClass","getNewId"});
            mappings.Add("property1", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property2", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property3", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property4", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property5", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property6", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property7", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property8", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property9", new string[] { "ReflectionVSDelegate.MethodClass", "getNewValue" });
            mappings.Add("property10", new string[] { "ReflectionVSDelegate.MethodClass", "getNewDouble" });
            mappings.Add("property11", new string[] { "ReflectionVSDelegate.MethodClass", "getNewDouble" });
            mappings.Add("property12", new string[] { "ReflectionVSDelegate.MethodClass", "getNewDouble" });
            mappings.Add("property13", new string[] { "ReflectionVSDelegate.MethodClass", "getNewDouble" });
            mappings.Add("property14", new string[] { "ReflectionVSDelegate.MethodClass", "getNewDouble" });
            mappings.Add("property15", new string[] { "ReflectionVSDelegate.MethodClass", "getNewDouble" });

            List<Entity> testCollection = new List<Entity>();
            for (int i = 0; i < 20000; i++)
                testCollection.Add(new Entity());

            ReflectionMapping Rmapper = new ReflectionMapping();
            DelegateMapping mapper = new DelegateMapping();
            Stopwatch sw = new Stopwatch();

            sw.Start();
            foreach (Entity en in testCollection)
            {
                en.EntityId = MethodClass.getNewId();
                en.property1 = MethodClass.getNewValue("property1");
                en.property2 = MethodClass.getNewValue("property2");
                en.property3 = MethodClass.getNewValue("property3");
                en.property4 = MethodClass.getNewValue("property4");
                en.property5 = MethodClass.getNewValue("property5");
                en.property6 = MethodClass.getNewValue("property6");
                en.property7 = MethodClass.getNewValue("property7");
                en.property8 = MethodClass.getNewValue("property8");
                en.property9 = MethodClass.getNewValue("property9");
                en.property10 = double.Parse(MethodClass.getNewDouble("property10"));
                en.property11 = double.Parse(MethodClass.getNewDouble("property11"));
                en.property12 = double.Parse(MethodClass.getNewDouble("property12"));
                en.property13 = double.Parse(MethodClass.getNewDouble("property13"));
                en.property14 = double.Parse(MethodClass.getNewDouble("property14"));
                en.property15 = double.Parse(MethodClass.getNewDouble("property15"));
            }
            sw.Stop();

            Stopwatch sw1 = new Stopwatch();

            sw1.Start();
            foreach(Entity en in testCollection)
                mapper.Map(en, mappings, false);
            sw1.Stop();

            Stopwatch sw11 = new Stopwatch();

            sw11.Start();
            foreach (Entity en in testCollection)
                mapper.Map(en, mappings, true);
            sw11.Stop();

            Stopwatch sw2 = new Stopwatch();

            sw2.Start();
            foreach (Entity en in testCollection)
                Rmapper.Map(en, mappings);
            sw2.Stop();

            TimeSpan ts2 = sw2.Elapsed;
            TimeSpan ts11 = sw11.Elapsed;
            TimeSpan ts = sw.Elapsed;
            TimeSpan ts1 = sw1.Elapsed;
            Console.WriteLine("Direct invoke time: {0} seconds {1} ms", ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Delegate time: {0} seconds {1} ms", ts1.Seconds, ts1.Milliseconds);
            Console.WriteLine("Delegate cached time: {0} seconds {1} ms", ts11.Seconds, ts11.Milliseconds);
            Console.WriteLine("Reflection time: {0} seconds {1} ms", ts2.Seconds, ts2.Milliseconds);

            Console.ReadLine();
        }
    }
}
