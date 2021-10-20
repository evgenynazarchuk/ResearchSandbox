using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace GetNormalType
{
    public class Program
    {
        static void Main(string[] args)
        {
            string name = "";
            string? note = "";
            List<Person> persons = new();
            ConcurrentDictionary<string, Person> personDict = new();
            
            Console.WriteLine($"{name.GetType()}");
            Console.WriteLine($"{note.GetType()}");
            Console.WriteLine($"{persons.GetType()}");
            Console.WriteLine($"{personDict.GetType()}");
        }
    }

    public class Person
    {
        public long Id { get; set; }
    }
}
