using System;
using System.Runtime.CompilerServices;

namespace CsvReader
{
    public abstract class CsvModel
    {
        public abstract void InitializeFromRow(ReadOnlySpan<string> row);

        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IntegerIsRequired(ReadOnlySpan<char> column)
            => int.TryParse(column, out int columnValue) ? columnValue 
            : throw new CsvModelException("Integer field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint UnsignedIntegerIsRequired(ReadOnlySpan<char> column)
            => uint.TryParse(column, out uint columnValue) ? columnValue 
            : throw new CsvModelException("Unsigned Integer field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long LongIntegerIsRequired(ReadOnlySpan<char> column)
            => long.TryParse(column, out long columnValue) ? columnValue 
            : throw new CsvModelException("Long Integer field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong UnsignedLongIntegerIsRequired(ReadOnlySpan<char> column)
            => ulong.TryParse(column, out ulong columnValue) ? columnValue 
            : throw new CsvModelException("Long Integer field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float FloatIsRequired(ReadOnlySpan<char> column)
            => float.TryParse(column, out float columnValue) ? columnValue 
            : throw new CsvModelException("Float field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DoubleIsRequired(ReadOnlySpan<char> column)
            => double.TryParse(column, out double columnValue) ? columnValue
            : throw new CsvModelException("Double field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal DecimalIsRequired(ReadOnlySpan<char> column)
            => decimal.TryParse(column, out decimal columnValue) ? columnValue
            : throw new CsvModelException("Decimal field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BooleanIsRequered(ReadOnlySpan<char> column)
            => bool.TryParse(column, out bool columnValue) ? columnValue
            : throw new CsvModelException("Boolean field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime DateTimeIsRequired(ReadOnlySpan<char> column)
            => DateTime.TryParse(column, out DateTime columnValue) ? columnValue
            : throw new CsvModelException("DateTime field is required");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan TimeSpanIsRequired(ReadOnlySpan<char> column)
            => TimeSpan.TryParse(column, out TimeSpan columnValue) ? columnValue
            : throw new CsvModelException("DateTime field is required");

        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int? IntegerIsNotRequired(ReadOnlySpan<char> column)
            => int.TryParse(column, out int columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint? UnsignedIntegerIsNotRequired(ReadOnlySpan<char> column)
            => uint.TryParse(column, out uint columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long? LongIntegerIsNotRequired(ReadOnlySpan<char> column)
            => long.TryParse(column, out long columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong? UnsignedLongIntegerIsNotRequired(ReadOnlySpan<char> column)
            => ulong.TryParse(column, out ulong columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float? FloatIsNotRequired(ReadOnlySpan<char> column)
            => float.TryParse(column, out float columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? DoubleIsNotRequired(ReadOnlySpan<char> column)
            => double.TryParse(column, out double columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal? DecimalIsNotRequired(ReadOnlySpan<char> column)
            => decimal.TryParse(column, out decimal columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool? BooleanIsNotRequired(ReadOnlySpan<char> column)
            => bool.TryParse(column, out bool columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime? DateTimeIsNotRequired(ReadOnlySpan<char> column)
            => DateTime.TryParse(column, out DateTime columnValue) ? columnValue : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan? TimeSpanIsNotRequired(ReadOnlySpan<char> column)
            => TimeSpan.TryParse(column, out TimeSpan columnValue) ? columnValue : null;
    }
}
