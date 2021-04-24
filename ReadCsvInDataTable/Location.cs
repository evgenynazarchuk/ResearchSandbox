using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCsvInDataTable
{
    public class Location : CsvModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public override void InitializationFromRow(ReadOnlySpan<string> row)
        {
            Id = IntegerIsRequired(row[0]);
            this.Name = row[1];
        }

        public override string ToString()
        {
            return $"{this.Id}\t{this.Name}";
        }
    }
}
