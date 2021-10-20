using System;

namespace WorkInterfaces
{
    public class Program
    {
        static void Main(string[] args)
        {
            InterfaceA b1 = new ClassB();
            b1.Method1(); // => Class A, Method 1
            b1.Method2(); // => Class B, Method 2

            InterfaceA c1 = new ClassC();
            c1.Method1(); // => Class C, Method 1
            c1.Method2(); // => Class C, Method 2
        }
    }

    public interface InterfaceA
    {
        void Method1();
        void Method2();
    }

    public class ClassA : InterfaceA
    {
        public void Method1()
        {
            Console.WriteLine("Class A, Method 1");
        }

        public virtual void Method2()
        {
            Console.WriteLine("Class A, Method 2");
        }
    }

    public class ClassB : ClassA
    {
        public new void Method1()
        {
            Console.WriteLine("Class B, Method 1");
        }

        public override void Method2()
        {
            Console.WriteLine("Class B, Method 2");
        }
    }

    public class ClassC : ClassA, InterfaceA
    {
        public new void Method1()
        {
            Console.WriteLine("Class C, Method 1");
        }

        public override void Method2()
        {
            Console.WriteLine("Class C, Method 2");
        }
    }
}
