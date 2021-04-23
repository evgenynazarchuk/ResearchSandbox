using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadJsonFile
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameLocal { get; set; }

        public override string ToString()
        {
            return $"{Id}" + " " + Name + " " + NameLocal;
        }
    }
}
