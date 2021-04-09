using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace ReadCsvInDataTable
{
    class Program
    {
        static void Main(string[] args)
        {
            using var streamReader = new StreamReader("data.csv", Encoding.UTF8);
            Regex parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string line;
            List<Info> rows = new();

            // skip header
            streamReader.ReadLine();

            while ((line = streamReader.ReadLine()) != null)
            {
                rows.Add(new(parser.Split(line)));
            }
            foreach (var info in rows)
            {
                Console.WriteLine(info.City + " " + info.Number);
            }
        }

        class Info
        {
            public Info(string[] row)
            {
                City = row[0];
                if (row[1] is not null && row[1] != "")
                {
                    Number = int.Parse(row[1]);
                }
            }
            public string City;
            public int? Number;
        }
    }
}
