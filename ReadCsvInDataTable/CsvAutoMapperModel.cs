using System;

namespace CsvReader
{
    public abstract class CsvAutoMapperModel : CsvModel
    {
        public override void InitializeFromRow(ReadOnlySpan<string> row)
        {
            var type = this.GetType();
            var properties = type.GetProperties();

            if (row.Length != properties.Length)
            {
                throw new CsvAutoMapperModelException("Row length is not equal properties length");
            }

            for (var i = 0; i < properties.Length; i++)
            {
                var propertyTypeString = properties[i].PropertyType.ToString();

                switch (propertyTypeString)
                {
                    case TypeString.String:
                        properties[i].SetValue(this, row[i], null);
                        break;

                    case TypeString.Integer:
                        properties[i].SetValue(this, IntegerIsRequired(row[i]), null);
                        break;

                    case TypeString.IntegerOrNull:
                        properties[i].SetValue(this, IntegerIsNotRequired(row[i]), null);
                        break;

                    case TypeString.Boolean:
                        properties[i].SetValue(this, BooleanIsRequired(row[i]), null);
                        break;

                    case TypeString.BooleanOrNull:
                        properties[i].SetValue(this, BooleanIsNotRequired(row[i]), null);
                        break;

                    case TypeString.UnsignedInteger:
                        properties[i].SetValue(this, UnsignedIntegerIsRequired(row[i]), null);
                        break;

                    case TypeString.UnsignedIntegerOrNull:
                        properties[i].SetValue(this, UnsignedIntegerIsNotRequired(row[i]), null);
                        break;

                    case TypeString.LongInterger:
                        properties[i].SetValue(this, LongIntegerIsRequired(row[i]), null);
                        break;

                    case TypeString.LongIntergerOrNull:
                        properties[i].SetValue(this, LongIntegerIsNotRequired(row[i]), null);
                        break;

                    case TypeString.UnsignedLongInterger:
                        properties[i].SetValue(this, UnsignedLongIntegerIsRequired(row[i]), null);
                        break;

                    case TypeString.UnsignedLongIntergerOrNull:
                        properties[i].SetValue(this, UnsignedLongIntegerIsNotRequired(row[i]), null);
                        break;

                    case TypeString.Float:
                        properties[i].SetValue(this, FloatIsRequired(row[i]), null);
                        break;

                    case TypeString.FloatOrNull:
                        properties[i].SetValue(this, FloatIsNotRequired(row[i]), null);
                        break;

                    case TypeString.Double:
                        properties[i].SetValue(this, DoubleIsRequired(row[i]), null);
                        break;

                    case TypeString.DoubleOrNull:
                        properties[i].SetValue(this, DoubleIsNotRequired(row[i]), null);
                        break;

                    case TypeString.Decimal:
                        properties[i].SetValue(this, DecimalIsRequired(row[i]), null);
                        break;

                    case TypeString.DecimalOrNull:
                        properties[i].SetValue(this, DecimalIsNotRequired(row[i]), null);
                        break;

                    case TypeString.DateTime:
                        properties[i].SetValue(this, DateTimeIsRequired(row[i]), null);
                        break;

                    case TypeString.DateTimeOrNull:
                        properties[i].SetValue(this, DateTimeIsNotRequired(row[i]), null);
                        break;

                    case TypeString.TimeSpan:
                        properties[i].SetValue(this, TimeSpanIsRequired(row[i]), null);
                        break;

                    case TypeString.TimeSpanOrNull:
                        properties[i].SetValue(this, TimeSpanIsNotRequired(row[i]), null);
                        break;

                    default:
                        throw new CsvAutoMapperModelException("Unknow Type");
                }
            }
        }
    }
}
