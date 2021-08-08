using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionRunner;

namespace ReflectionRunner.Tests
{
    public class TestSample2
    {
        [Test]
        public async Task Test1()
        {
            Console.WriteLine($"{nameof(Test1)}");

            await Task.CompletedTask;
        }

        [Test]
        public async Task Test2()
        {
            Console.WriteLine($"{nameof(Test2)}");

            await Task.CompletedTask;
        }
    }
}
