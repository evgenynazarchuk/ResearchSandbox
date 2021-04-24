using System.Text;
using System.IO;

namespace ReportWriter
{
    public class ReportWriter
    {
        private readonly StreamWriter _writer;

        public ReportWriter(string path)
        {
            this._writer = new(path, false, Encoding.UTF8, 65535);
        }

        public void Write(string msg)
        {
            this._writer.WriteLine(msg);
        }

        public void Dispose()
        {
            this._writer.Flush();
            this._writer.Close();
        }
    }
}
