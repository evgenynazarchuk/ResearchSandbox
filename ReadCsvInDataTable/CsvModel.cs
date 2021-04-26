using System;

namespace CsvReader
{
    public abstract class CsvModel
    {
        public abstract void InitializeFromRow(ReadOnlySpan<string> columns);

        //
        public static int IntegerIsRequired(ReadOnlySpan<char> column)
            => int.TryParse(column, out int columnValue) ? columnValue 
            : throw new CsvModelException("Integer field is required");

        public static uint UnsignedIntegerIsRequired(ReadOnlySpan<char> column)
            => uint.TryParse(column, out uint columnValue) ? columnValue 
            : throw new CsvModelException("Unsigned Integer field is required");

        public static long LongIntegerIsRequired(ReadOnlySpan<char> column)
            => long.TryParse(column, out long columnValue) ? columnValue 
            : throw new CsvModelException("Long Integer field is required");

        public static ulong UnsignedLongIntegerIsRequired(ReadOnlySpan<char> column)
            => ulong.TryParse(column, out ulong columnValue) ? columnValue 
            : throw new CsvModelException("Unsigned Long Integer field is required");

        public static float FloatIsRequired(ReadOnlySpan<char> column)
            => float.TryParse(column, out float columnValue) ? columnValue 
            : throw new CsvModelException("Float field is required");

        public static double DoubleIsRequired(ReadOnlySpan<char> column)
            => double.TryParse(column, out double columnValue) ? columnValue
            : throw new CsvModelException("Double field is required");

        public static decimal DecimalIsRequired(ReadOnlySpan<char> column)
            => decimal.TryParse(column, out decimal columnValue) ? columnValue
            : throw new CsvModelException("Decimal field is required");

        public static bool BooleanIsRequired(ReadOnlySpan<char> column)
            => bool.TryParse(column, out bool columnValue) ? columnValue
            : throw new CsvModelException("Boolean field is required");

        public static DateTime DateTimeIsRequired(ReadOnlySpan<char> column)
            => DateTime.TryParse(column, out DateTime columnValue) ? columnValue
            : throw new CsvModelException("DateTime field is required");

        public static TimeSpan TimeSpanIsRequired(ReadOnlySpan<char> column)
            => TimeSpan.TryParse(column, out TimeSpan columnValue) ? columnValue
            : throw new CsvModelException("DateTime field is required");

        //
        public static int? IntegerIsNotRequired(ReadOnlySpan<char> column)
        {
            if (column.Length > 0)
            {
                return int.TryParse(column, out int columnValue) 
                    ? columnValue 
                    : throw new CsvModelException("Integer must be integer");
            }
            return null;
        }

        public static uint? UnsignedIntegerIsNotRequired(ReadOnlySpan<char> column)
        {
            if (column.Length > 0)
            {
                return uint.TryParse(column, out uint columnValue) ? columnValue
                    : throw new CsvModelException("Unsigned Integer must be unsigned integer");
            }
            return null;
        }

        public static long? LongIntegerIsNotRequired(ReadOnlySpan<char> column)
        {
            if (column.Length > 0)
            {
                return long.TryParse(column, out long columnValue) ? columnValue
                    : throw new CsvModelException("Long Integer must be long integer");
            }
            return null;
        }
        
        public static ulong? UnsignedLongIntegerIsNotRequired(ReadOnlySpan<char> column)
        {
            if (column.Length > 0)
            {
                return ulong.TryParse(column, out ulong columnValue) ? columnValue
                    : throw new CsvModelException("Unsigned Long Integer must be unsigned long integer");
            }
            return null;
        }
        
        public static float? FloatIsNotRequired(ReadOnlySpan<char> column)
        {
            if (column.Length > 0)
            {
                return float.TryParse(column, out float columnValue) ? columnValue
                    : throw new CsvModelException("Float must be float");
            }
            return null;
        }

        public static double? DoubleIsNotRequired(ReadOnlySpan<char> column)
        {
            {
                if (column.Length > 0)
                {
                    return double.TryParse(column, out double columnValue) ? columnValue
                        : throw new CsvModelException("Doouble must be double");
                }
                return null;
            }
        }

        public static decimal? DecimalIsNotRequired(ReadOnlySpan<char> column)
        {
            {
                if (column.Length > 0)
                {
                    return decimal.TryParse(column, out decimal columnValue) ? columnValue
                        : throw new CsvModelException("Decimal must be decimal");
                }
                return null;
            }
        }
        

        public static bool? BooleanIsNotRequired(ReadOnlySpan<char> column)
        {
            {
                if (column.Length > 0)
                {
                    return bool.TryParse(column, out bool columnValue) ? columnValue
                        : throw new CsvModelException("Boolean must be boolean (true/false)");
                }
                return null;
            }
        }
        

        public static DateTime? DateTimeIsNotRequired(ReadOnlySpan<char> column)
        {
            {
                if (column.Length > 0)
                {
                    return DateTime.TryParse(column, out DateTime columnValue) ? columnValue
                        : throw new CsvModelException("DateTime must be datetime");
                }
                return null;
            }
        }

        public static TimeSpan? TimeSpanIsNotRequired(ReadOnlySpan<char> column)
        {
            {
                if (column.Length > 0)
                {
                    return TimeSpan.TryParse(column, out TimeSpan columnValue) ? columnValue
                        : throw new CsvModelException("DateTime must be datetime");
                }
                return null;
            }
        }
    }
}
