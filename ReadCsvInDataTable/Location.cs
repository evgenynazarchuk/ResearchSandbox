using System;

namespace CsvReader
{
    public class Location : CsvAutoMapperModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        // Not required
        public override string ToString()
        {
            return $"{this.Id}\t{this.Name}";
        }
    }
}
