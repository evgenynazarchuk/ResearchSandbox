using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCsvInDataTable
{
    public interface ICsvModel
    {
        public void Init(string[] row);
    }
}
