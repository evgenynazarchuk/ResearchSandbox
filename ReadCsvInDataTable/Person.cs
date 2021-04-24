using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCsvInDataTable
{
    public class Person : CsvModel
    {
        public int? Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override void InitializationFromRow(ReadOnlySpan<string> row)
        {
            this.Id = IntegerIsNotRequired(row[0]);
            this.FirstName = row[1];
            this.LastName = row[2];
        }

        public override string ToString()
        {
            return $"{this.Id} {this.FirstName} {this.LastName}";
        }
    }
}
