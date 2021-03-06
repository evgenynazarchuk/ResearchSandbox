using System;
using System.Reflection;
using System.Collections.Generic;
using ReflectionRunner.Tests;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace ReflectionRunner
{
    class Program
    {
        static async Task Main()
        {
            // create collection of test methods
            Type[] assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            Dictionary<int, (Type, MethodInfo, object[])> testsList = new();

            int testNumber = 1;
            foreach (var assemblyType in assemblyTypes)
            {
                foreach (var methodInfo in assemblyType.GetMethods())
                {
                    foreach (var attribute in methodInfo
                        .GetCustomAttributes()
                        .Where(x => x is TestAttribute)
                        .Select(x => x as TestAttribute)
                        )
                    {
                        testsList.Add(testNumber++, (assemblyType, methodInfo, attribute.Parameters));
                    }
                }
            }

            // print test list
            foreach (var test in testsList)
            {
                var methodParameters = test.Value.Item2.GetParameters();
                var methodParametersName = methodParameters.Select(x => x.Name).ToList();

                StringBuilder args = new();
                for (int i = 0; i < test.Value.Item3?.Length; i++)
                {
                    args.Append($"{methodParametersName[i]}: {test.Value.Item3[i]}, ");
                }
                if (args.Length > 0)
                {
                    args.Remove(args.Length - 2, 2);
                }

                if (args.Length > 0)
                {
                    Console.WriteLine($"{test.Key} - {test.Value.Item1.Name}.{test.Value.Item2.Name}: {args}");
                }
                else
                {
                    Console.WriteLine($"{test.Key} - {test.Value.Item1.Name}.{test.Value.Item2.Name}");
                }   
            }

            Console.Write($"Enter test number: ");
            if (!Int32.TryParse(Console.ReadLine(), out int selectedTestNumber)) 
                throw new ApplicationException("Test number is incorrect");


            // create test class
            var testClass = Activator.CreateInstance(testsList[selectedTestNumber].Item1);
            if (testClass is null) 
                throw new ApplicationException("Error create test");


            // invoke selected method
            object testTask;

            if (testsList[selectedTestNumber].Item3 is not null)
            {
                testTask = testsList[selectedTestNumber].Item2.Invoke(testClass, testsList[selectedTestNumber].Item3);
            }
            else
            {
                testTask = testsList[selectedTestNumber].Item2.Invoke(testClass, null);
            }

            if (testTask is not null && testTask is Task)
            {
                await (Task)testTask;
            }
            else
            {
                throw new ApplicationException("Error run test");
            }
        }
    }
}
