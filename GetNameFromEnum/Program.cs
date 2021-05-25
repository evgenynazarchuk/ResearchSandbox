using System;

namespace GetNameFromEnum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(nameof(Attrs.FileLogName));
            Console.WriteLine(nameof(Attrs.AnyNameAttr));
        }

        enum Attrs
        {
            FileLogName,
            AnyNameAttr
        }
    }
}
