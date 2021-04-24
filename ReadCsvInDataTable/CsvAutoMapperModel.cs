using System;

namespace CsvReader
{
    public abstract class CsvAutoMapperModel : CsvModel
    {
        public override void InitializationFromRow(ReadOnlySpan<string> row)
        {
            var type = this.GetType();
            var properties = type.GetProperties();

            if (row.Length != properties.Length)
            {
                throw new ApplicationException("Row length is not equal properties length");
            }

            for (var i = 0; i < properties.Length; i++)
            {
                var propertyType = properties[i].PropertyType;

                if (propertyType == typeof(string))
                {
                    properties[i].SetValue(this, row[i], null);
                }

                //
                else if (propertyType == typeof(int))
                {
                    properties[i].SetValue(this, IntegerIsRequired(row[i]), null);
                }

                else if (propertyType == typeof(int?))
                {
                    properties[i].SetValue(this, IntegerIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(uint))
                {
                    properties[i].SetValue(this, UnsignedIntegerIsRequired(row[i]), null);
                }

                else if (propertyType == typeof(uint?))
                {
                    properties[i].SetValue(this, UnsignedIntegerIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(long))
                {
                    properties[i].SetValue(this, LongIntegerIsRequired(row[i]), null);
                }

                else if (propertyType == typeof(long?))
                {
                    properties[i].SetValue(this, LongIntegerIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(ulong))
                {
                    properties[i].SetValue(this, UnsignedLongIntegerIsRequired(row[i]), null);
                }

                else if (propertyType == typeof(ulong?))
                {
                    properties[i].SetValue(this, UnsignedLongIntegerIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(float))
                {
                    properties[i].SetValue(this, FloatIsRequired(row[i]), null);
                }

                else if (propertyType == typeof(float?))
                {
                    properties[i].SetValue(this, FloatIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(double))
                {
                    properties[i].SetValue(this, DoubleIsRequired(row[i]), null);
                }

                else if (propertyType == typeof(double?))
                {
                    properties[i].SetValue(this, DoubleIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(decimal))
                {
                    properties[i].SetValue(this, DecimalIsRequired(row[i]), null);
                }

                else if (propertyType == typeof(decimal?))
                {
                    properties[i].SetValue(this, DecimalIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(bool))
                {
                    properties[i].SetValue(this, BooleanIsRequered(row[i]), null);
                }

                else if (propertyType == typeof(bool?))
                {
                    properties[i].SetValue(this, BooleanIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(DateTime))
                {
                    properties[i].SetValue(this, DateTimeIsRequered(row[i]), null);
                }

                else if (propertyType == typeof(DateTime?))
                {
                    properties[i].SetValue(this, DateTimeIsNotRequired(row[i]), null);
                }

                //
                else if (propertyType == typeof(TimeSpan))
                {
                    properties[i].SetValue(this, DateTimeIsRequered(row[i]), null);
                }

                else if (propertyType == typeof(TimeSpan?))
                {
                    properties[i].SetValue(this, DateTimeIsNotRequired(row[i]), null);
                }

                //
                else new ApplicationException("Unknow Type");
            }
        }
    }
}
