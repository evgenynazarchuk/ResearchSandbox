using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace ReadCsvInDataTable
{
    class Program
    {
        static void Main()
        {
            var personReader = new CsvReader<Person>("person.csv");
            var locationReader = new CsvReader<Location>("location.csv");


            Console.WriteLine("Persons:");
            Person person;
            while ((person = personReader.GetNextObject()) != null)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("\nLocations:");
            Location location;
            while ((location = locationReader.GetNextObject()) != null)
            {
                Console.WriteLine(location);
            }    
        }
    }
}
