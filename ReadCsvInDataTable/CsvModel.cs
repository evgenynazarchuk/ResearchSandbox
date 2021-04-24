using System;

namespace CsvReader
{
    public abstract class CsvModel
    {
        public abstract void InitializationFromRow(ReadOnlySpan<string> row);

        //
        public static int IntegerIsRequired(ReadOnlySpan<char> row)
            => int.TryParse(row, out int result) ? result 
            : throw new CsvConverterException("Integer field is required");

        public static uint UnsignedIntegerIsRequired(ReadOnlySpan<char> row)
            => uint.TryParse(row, out uint result) ? result 
            : throw new CsvConverterException("Unsigned Integer field is required");

        public static long LongIntegerIsRequired(ReadOnlySpan<char> row)
            => long.TryParse(row, out long result) ? result 
            : throw new CsvConverterException("Long Integer field is required");

        public static ulong UnsignedLongIntegerIsRequired(ReadOnlySpan<char> row)
            => ulong.TryParse(row, out ulong result) ? result 
            : throw new CsvConverterException("Long Integer field is required");

        public static float FloatIsRequired(ReadOnlySpan<char> row)
            => float.TryParse(row, out float result) ? result 
            : throw new CsvConverterException("Float field is required");
        
        public static double DoubleIsRequired(ReadOnlySpan<char> row)
            => double.TryParse(row, out double result) ? result
            : throw new CsvConverterException("Double field is required");
        
        public static decimal DecimalIsRequired(ReadOnlySpan<char> row)
            => decimal.TryParse(row, out decimal result) ? result
            : throw new CsvConverterException("Decimal field is required");

        public static bool BooleanIsRequered(ReadOnlySpan<char> row)
            => bool.TryParse(row, out bool result) ? result
            : throw new CsvConverterException("Boolean field is required");

        public static DateTime DateTimeIsRequered(ReadOnlySpan<char> row)
            => DateTime.TryParse(row, out DateTime result) ? result
            : throw new CsvConverterException("DateTime field is required");

        public static TimeSpan TimeSpanIsRequered(ReadOnlySpan<char> row)
            => TimeSpan.TryParse(row, out TimeSpan result) ? result
            : throw new CsvConverterException("DateTime field is required");

        //
        public static int? IntegerIsNotRequired(ReadOnlySpan<char> row)
            => int.TryParse(row, out int result) ? result : null;

        public static uint? UnsignedIntegerIsNotRequired(ReadOnlySpan<char> row)
            => uint.TryParse(row, out uint result) ? result : null;

        public static long? LongIntegerIsNotRequired(ReadOnlySpan<char> row)
            => long.TryParse(row, out long result) ? result : null;

        public static ulong? UnsignedLongIntegerIsNotRequired(ReadOnlySpan<char> row)
            => ulong.TryParse(row, out ulong result) ? result : null;

        public static float? FloatIsNotRequired(ReadOnlySpan<char> row)
            => float.TryParse(row, out float result) ? result : null;

        public static double? DoubleIsNotRequired(ReadOnlySpan<char> row)
            => double.TryParse(row, out double result) ? result : null;

        public static decimal? DecimalIsNotRequired(ReadOnlySpan<char> row)
            => decimal.TryParse(row, out decimal result) ? result : null;

        public static bool? BooleanIsNotRequired(ReadOnlySpan<char> row)
            => bool.TryParse(row, out bool result) ? result : null;

        public static DateTime? DateTimeIsNotRequired(ReadOnlySpan<char> row)
            => DateTime.TryParse(row, out DateTime result) ? result : null;

        public static TimeSpan? TimeSpanIsNotRequired(ReadOnlySpan<char> row)
            => TimeSpan.TryParse(row, out TimeSpan result) ? result : null;
    }
}
