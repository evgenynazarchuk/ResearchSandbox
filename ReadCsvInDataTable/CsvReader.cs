using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace CsvReader
{
    public class CsvReader<ResultType>
        where ResultType: CsvModel, new()
    {
        protected StreamReader Reader { get; private set; }
        protected Regex Parser { get; private set; }
        private string _line;

        public CsvReader(string path, bool hasHeader = true)
        {
            // TODO: buffer value will need read from config (65535)
            this.Reader = new StreamReader(path, Encoding.UTF8, true, 65535);
            this.Parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))", RegexOptions.Compiled);

            if (hasHeader)
            {
                this.Reader.ReadLine();
            }
        }

        public ReadOnlySpan<string> GetNextRow()
        {
            string[] row = null;
            this._line = this.Reader.ReadLine();

            if (this._line != null)
            {
                row = this.Parser.Split(this._line);
            }

            return row;
        }

        public ResultType GetNextObject()
        {
            var row = this.GetNextRow();

            if (row != null)
            {
                var instance = new ResultType();
                instance.InitializationFromRow(row);
                return instance;
            }

            return default;
        }
    }
}
