using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLog
{
    public class BytesCount
    {
        public DateTime EndResponse { get; set; }

        public int Count { get; set; }

        public BytesCount(DateTime EndResponse, int count)
        {
            this.EndResponse = EndResponse;
            Count = count;
        }
    }
}
