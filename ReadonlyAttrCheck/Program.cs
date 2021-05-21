using System;

namespace ReadonlyAttrCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class A1
    {
        protected int Code;

        public A1(int? code = default)
        {
            this.Code = code.HasValue ? code.Value : -1;
        }
    }

    public class A2 : A1
    {
        public A2(int? code = null)
        {
            InitCode(code.Value);
        }

        protected void InitCode(int code) => this.Code = code;
    }
}
