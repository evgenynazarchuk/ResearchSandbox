using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGeneration
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{TestValueGenerator.GenerateCapitalFirstLetterString()}");
            Console.WriteLine($"{TestValueGenerator.GenerateLowercaseLetterString()}");
            Console.WriteLine($"{TestValueGenerator.GenerateUppercaseLetterString()}");
        }
    }

    public static class TestValueGenerator
    {
        private static Random random = new Random();

        public static string GenerateUppercaseLetterString(int length = 10)
        {
            if (length <= 0)
            {
                throw new ApplicationException("String length must be greater than 0");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateLowercaseLetterString(int length = 10)
        {
            return GenerateUppercaseLetterString(length).ToLower();
        }

        public static string GenerateCapitalFirstLetterString(int length = 10)
        {
            var lowerString = GenerateLowercaseLetterString(length);
            return string.Concat(lowerString[0].ToString().ToUpper(), lowerString.AsSpan(1));
        }

        public static int GenerateNumber()
        {
            return random.Next();
        }

        public static int GenerateNumber(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
