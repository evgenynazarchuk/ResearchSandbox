using System;

namespace ReadCsvInDataTable
{
    public abstract class CsvModel
    {
        public abstract void InitFromRow(string[] row);

        public int IntegerIsRequired(string row)
        {
            if (int.TryParse(row, out int result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("Integer field is required");
            }
        }

        public int? IntegerIsNotRequired(string row)
        {
            if (int.TryParse(row, out int result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
