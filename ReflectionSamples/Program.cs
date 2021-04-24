using System;
using System.Reflection;

namespace ReflectionSamples
{
    class Program
    {
        static void Main()
        {
            var sampleType = new AnySampleType();
            sampleType.Init(new string[] { "11111", "22222", "aaaaa", "BBBBB" });

            Console.WriteLine(sampleType.Int1);
            Console.WriteLine(sampleType.Int2);
            Console.WriteLine(sampleType.String1);
            Console.WriteLine(sampleType.String2);
        }
    }

    public class InitProperties
    {
        public void Init(ReadOnlySpan<string> values)
        {
            var type = this.GetType();
            var properties = type.GetProperties();
            

            if (values.Length != properties.Length)
            {
                throw new ApplicationException("Values length is not equal properties length");
            }

            for (var i = 0; i < properties.Length; i++)
            {
                var propertyType = properties[i].PropertyType;

                #region integer_init
                if (propertyType == typeof(int))
                {
                    if (int.TryParse(values[i], out int result))
                    {
                        properties[i].SetValue(this, result, null);
                    }
                    else
                    {
                        throw new ApplicationException("Integer field is required");
                    }
                }

                if (propertyType == typeof(int?))
                {
                    if (int.TryParse(values[i], out int result))
                    {
                        properties[i].SetValue(this, result, null);
                    }
                    else
                    {
                        properties[i].SetValue(this, null, null);
                    }
                }
                #endregion integer_init

                #region string_init
                if (propertyType == typeof(string))
                {
                    properties[i].SetValue(this, values[i], null);
                }
                #endregion string_init
            }
        }
    }

    public class AnySampleType : InitProperties
    {
        public int Int1 { get; private set; }
        public int Int2 { get; private set; }
        public string String1 { get; private set; }
        public string String2 { get; private set; }
    }
}
