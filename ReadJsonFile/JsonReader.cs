using System.Text;
using System.IO;
using System.Text.Json;

namespace ReadJsonFile
{
    public class JsonReader<ResultType>
        where ResultType : class, new()
    {
        protected StreamReader Reader { get; private set; }
        protected JsonSerializerOptions JsonSerializerOptions { get; set; }

        public JsonReader(string path)
        {
            this.Reader = new StreamReader(path, Encoding.UTF8, true, 65535);
            this.JsonSerializerOptions = new();
            this.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // не учитываем регистр атрибутов
        }

        public void InitializeJsonSerializerOptions(JsonSerializerOptions options)
        {
            this.JsonSerializerOptions = options;
        }

        public ResultType GetNextObject()
        {
            var line = this.Reader.ReadLine();

            if (line != null)
            {
                return JsonSerializer.Deserialize<ResultType>(line, this.JsonSerializerOptions);
            }
            else
            {
                return default;
            }
        }
    }
}
