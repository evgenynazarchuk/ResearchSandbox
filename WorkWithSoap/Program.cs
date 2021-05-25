using System;
using System.Collections.Generic;

namespace WorkWithSoap
{
    class Program
    {
        static void Main(string[] args)
        {
            Test2();
        }

        static void Test1()
        {
            var saveLocation = new SaveLocation()
            {
                Location = new Location
                {
                    Id = 1,
                    Name = "Test Location"
                }
            };

            var message = SoapMessage.CreateMessage(saveLocation, prefix: "ns0", @namespace: "http://host/facade");
            Console.WriteLine(message);
        }

        static void Test2()
        {
            var employeeEnvelope = new EmployeeEnvelope()
            {
                Header = new Header(),
                Body = new Body()
                {
                    SaveEmployee = new SaveEmployee()
                    {
                        Employee = new Employee()
                        {
                            Id = 1,
                            Name = "Evgeny"
                        }
                    }
                }
            };

            var ns = new Dictionary<string, string>()
            {
                { "soapenv", "http://schemas.xmlsoap.org/soap/envelope/" },
                { "ns0", "http://host/servce" }
            };

            var soapMessage = employeeEnvelope.ToXmlString(ns);
            Console.WriteLine(soapMessage);
        }
    }
}
