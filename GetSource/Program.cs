using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

namespace GetSource
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var type = typeof(TestClass);
            var method = type.GetMethod("TestMethod");
            var body = method.GetMethodBody();
            Console.WriteLine(body.ToString());
        }
    }


    internal class TestClass
    {
        public void TestMethod(int x, int y)
        {
            Console.WriteLine($"{x}, {y}");
        }
    }
}
