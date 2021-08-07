using System;
using System.Reflection;
using System.Collections.Generic;
using ReflectionRunner.Tests;

namespace ReflectionRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] assemblyTypes = assembly.GetTypes();
            List<Type> testClassTypes = new();

            Dictionary<int, (Type, MethodInfo)> tests = new();

            foreach (var type in assemblyTypes)
            {
                foreach (var attr in type.CustomAttributes)
                {
                    if (attr.AttributeType == typeof(TestClassAttribute))
                    {
                        testClassTypes.Add(type);
                    }
                }
            }

            int testNumber = 1;
            foreach (var testClassType in testClassTypes)
            {
                var methods = testClassType.GetMethods();
                foreach (var method in methods)
                {
                    foreach (var attr in method.CustomAttributes)
                    {
                        if (attr.AttributeType == typeof(TestAttribute))
                        {
                            tests.Add(testNumber++, (testClassType, method));
                        }
                    }
                }
            }

            foreach (var item in tests)
            {
                Console.WriteLine($"{item.Key} {item.Value.Item1.Name} {item.Value.Item2.Name}");
            }

            Console.Write($"Enter test number: ");
            int selectedTestNumber = Int32.Parse(Console.ReadLine());

            var sampleTest = Activator.CreateInstance(tests[selectedTestNumber].Item1);
            var sampleTestMethod = sampleTest.GetType().GetMethods();
            foreach (var method in sampleTestMethod)
            {
                if (method == tests[selectedTestNumber].Item2)
                {
                    method.Invoke(sampleTest, null);
                }
            }
        }
    }
}
