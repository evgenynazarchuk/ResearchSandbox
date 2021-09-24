using System;
using System.Reflection;
using Logger;

namespace UniversalLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger.UniversalLogger();

            logger.AppendLogMessage("file1.txt", "ukraine,kyiv", typeof(SimpleMessage1));
            logger.AppendLogMessage("file2.txt", "evgeny,evgeny,nazarchuk", typeof(SimpleMessage2));
            logger.AppendLogMessage("file1.txt", "russia,moscow", typeof(SimpleMessage1));
            logger.AppendLogMessage("file2.txt", "anna,anna,zamorskaya", typeof(SimpleMessage2));
            logger.AppendLogMessage("file1.txt", "usa,washington", typeof(SimpleMessage1));

            logger.StartProcessing();
        }

        public class SimpleMessage1
        {
            public string Country { get; set; }
            public string Location { get; set; }
        }

        public class SimpleMessage2
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
        }



        public void TestReflection()
        {
            var obj = Activator.CreateInstance(typeof(LogMessage));
            var properties = obj.GetType().GetProperties();

            properties[0].SetValue(obj, 1);
            properties[1].SetValue(obj, "Test Name");

            foreach (var property in properties)
            {
                Console.WriteLine(property.Name);
            }
        }
    }

    public class LogMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
