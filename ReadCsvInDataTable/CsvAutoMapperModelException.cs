using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReader
{
    public class CsvAutoMapperModelException : Exception
    {
        public CsvAutoMapperModelException(string message) : base(message)
        {
        }
    }
}
