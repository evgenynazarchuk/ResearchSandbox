using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReader
{
    public class CsvModelException : Exception
    {
        public CsvModelException(string message) : base(message)
        {
        }
    }
}