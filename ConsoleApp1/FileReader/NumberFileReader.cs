namespace ConsoleApp1.FileReader
{
    /// <summary>
    /// A class to manage the Advent Of Code input files for Santa.
    /// </summary>
    public class NumbersFileReader : FileReaderBase
    {
        private char _separator;

        private List<string[]> _data = new List<string[]>();

        public int ColumnsCount { get; set; }
        public int LinesCount
        {
            get => _data.Count;
        }

        /// <summary>
        /// Creates a new instance of the SantaFileReader
        /// </summary>
        /// <param name="filename">The filename with full or relative path.</param>
        /// <param name="separator">The separator used in the file to separate fields</param>
        public NumbersFileReader(string filename, char separator)
            :base(filename)
        {
            _separator = separator;
            ReadData();
        }

        public override void ReadData()
        {
            using (StreamReader sr = new StreamReader(Filename))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine()?.Split(_separator, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];

                    if (line.Length > ColumnsCount)
                    {
                        ColumnsCount = line.Length;
                    }

                    _data.Add(line);
                }
            }

        }
        /// <summary>
        /// Gets a list of int from the specified line number. The number of elements depends on the line elements count.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IEnumerable<int?> GetIntsLine(int lineNumber)
        {
            if (lineNumber > _data.Count)
            {
                throw new ArgumentOutOfRangeException(string.Format("Line number must be between 0 and {1}", _data.Count - 1), nameof(lineNumber));
            }

            List<int?> data = new List<int?>();
            var dataLine = _data[lineNumber];

            foreach (var item in dataLine)
            {
                int? value = null;
                bool res = int.TryParse(item, out int parsedValue);
                if (res)
                {
                    value = parsedValue;
                }
                data.Add(value);
            }

            return data;
        }

        /// <summary>
        /// Gets a list of int from the specified column index.
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IEnumerable<SantaElement<int?>> GetInt32NullColumn(byte columnIndex)
        {
            if (columnIndex < 0 || columnIndex > ColumnsCount)
            {
                throw new ArgumentOutOfRangeException(string.Format("Column index must be between 0 and {1}", ColumnsCount - 1), nameof(columnIndex));
            }
            int index = 0;
            return _data.Select(a =>
            {
                int? value = null;
                if (columnIndex < a.Length)
                {
                    var res = int.TryParse(a[columnIndex], out int parsedValue);
                    if (res)
                    {
                        value = parsedValue;
                    }
                }
                return new SantaElement<int?>(index++, value, false);
            }).ToList();
        }

    }

}
