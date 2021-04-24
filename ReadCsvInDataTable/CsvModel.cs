using System;

namespace ReadCsvInDataTable
{
    public abstract class CsvModel
    {
        public abstract void InitializationFromRow(ReadOnlySpan<string> row);

        public static int IntegerIsRequired(ReadOnlySpan<char> row)
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

        public static int? IntegerIsNotRequired(ReadOnlySpan<char> row)
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

        public static long LongIntegerIsRequired(ReadOnlySpan<char> row)
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

        public static long? LongIntegerIsNotRequired(ReadOnlySpan<char> row)
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

        public static float FloatIsRequired(ReadOnlySpan<char> row)
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

        public static float? FloatIsNotRequired(ReadOnlySpan<char> row)
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

        public static double DoubleIsRequired(ReadOnlySpan<char> row)
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

        public static double? DoubleIsNotRequired(ReadOnlySpan<char> row)
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

        public static decimal DecimalIsRequired(ReadOnlySpan<char> row)
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

        public static decimal? DecimalIsNotRequired(ReadOnlySpan<char> row)
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

        public static bool BoolenIsRequered(ReadOnlySpan<char> row)
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

        public static bool? BooleanIsNotRequired(ReadOnlySpan<char> row)
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

        public static DateTime DateTimeIsRequered(ReadOnlySpan<char> row)
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

        public static DateTime? DateTimeIsNotRequired(ReadOnlySpan<char> row)
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

        public static TimeSpan TimeSpanIsRequered(ReadOnlySpan<char> row)
        {
            if (TimeSpan.TryParse(row, out TimeSpan result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("DateTime field is required");
            }
        }

        public static TimeSpan? TimeSpanIsNotRequired(ReadOnlySpan<char> row)
        {
            if (TimeSpan.TryParse(row, out TimeSpan result))
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
