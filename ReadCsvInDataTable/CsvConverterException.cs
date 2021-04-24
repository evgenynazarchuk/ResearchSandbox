using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReader
{
    public class CsvConverterException : Exception
    {
        public CsvConverterException(string message) : base(message)
        {
        }
    }
}