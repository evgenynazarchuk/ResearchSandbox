using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionRunner;

namespace ReflectionRunner.Tests
{
    public class TestSample1
    {
        [Test(5, 7)]
        [Test(21, 27)]
        public async Task Test1(int size, int length)
        {
            Console.WriteLine($"{nameof(Test1)} {size} {length}");

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
