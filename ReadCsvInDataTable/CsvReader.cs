using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;

namespace CsvReader
{
    public sealed class CsvReader<ResultType> : IEnumerable<ResultType>
        where ResultType: CsvModel, new()
    {
        private readonly StreamReader _reader;
        private readonly Regex _parser;

        public CsvReader(
            string path, 
            bool hasHeader = true, 
            bool detectEncodingFromByteOrderMarks = true, 
            int bufferSize = 65525
            )
        {
            this._reader = new StreamReader(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, bufferSize);
            this._parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))", RegexOptions.Compiled);

            if (hasHeader)
            {
                this._reader.ReadLine();
            }
        }

        private ReadOnlySpan<string> GetNextRow()
        {
            string[] row = null;
            var line = this._reader.ReadLine();

            if (line != null)
            {
                row = this._parser.Split(line);
            }

            return row;
        }

        public ResultType GetNextObject()
        {
            var row = this.GetNextRow();

            if (row != null)
            {
                var instance = new ResultType();
                instance.InitializeFromRow(row);
                return instance;
            }

            return default;
        }

        public IEnumerator<ResultType> GetEnumerator()
        {
            ResultType resultType;
            while ((resultType = this.GetNextObject()) != null)
            {
                yield return resultType;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
