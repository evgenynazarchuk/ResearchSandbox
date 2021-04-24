using System;

namespace ReadCsvInDataTable
{
    public abstract class CsvModel
    {
        public abstract void InitializationFromRow(ReadOnlySpan<string> row);

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

        public long LongIntegerIsRequired(string row)
        {
            if (long.TryParse(row, out long result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("Long Integer field is required");
            }
        }

        public long? LongIntegerIsNotRequired(string row)
        {
            if (long.TryParse(row, out long result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public float FloatIsRequired(string row)
        {
            if (float.TryParse(row, out float result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("Float field is required");
            }
        }

        public float? FloatIsNotRequired(string row)
        {
            if (float.TryParse(row, out float result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public double DoubleIsRequired(string row)
        {
            if (double.TryParse(row, out double result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("Double field is required");
            }
        }

        public double? DoubleIsNotRequired(string row)
        {
            if (double.TryParse(row, out double result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public decimal DecimalIsRequired(string row)
        {
            if (decimal.TryParse(row, out decimal result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("Decimal field is required");
            }
        }

        public decimal? DecimalIsNotRequired(string row)
        {
            if (decimal.TryParse(row, out decimal result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public bool BoolenIsRequered(string row)
        {
            if (bool.TryParse(row, out bool result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("Boolean field is required");
            }
        }

        public bool? BooleanIsNotRequired(string row)
        {
            if (bool.TryParse(row, out bool result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public DateTime DateTimeIsRequered(string row)
        {
            if (DateTime.TryParse(row, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("DateTime field is required");
            }
        }

        public DateTime? DateTimeIsNotRequired(string row)
        {
            if (DateTime.TryParse(row, out DateTime result))
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
