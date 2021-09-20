using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ReadConfiguration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnectionString", "ffffffffffffff" }
            }).Build();

            Console.WriteLine(config.GetConnectionString("Development"));
        }
    }
}
