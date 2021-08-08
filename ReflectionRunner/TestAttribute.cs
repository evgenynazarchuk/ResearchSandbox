using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionRunner
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestAttribute : Attribute
    {
        public object[] Parameters { get; private set; }

        public TestAttribute() { }

        public TestAttribute(params object[] parametrs)
        {
            Parameters = parametrs;
        }
    }
}
