namespace ConsoleApp1.FileReader
{
    public class RawTextFileReader : FileReaderBase
    {
        public List<List<char>> Data { get; private set; } = new List<List<char>>();

        private int _xSize = 0;
        public int XSize { get => _xSize; }

        private int _ySize = 0;
        public int YSize { get => _ySize; }

        public RawTextFileReader(string filename)
            : base(filename)
        {
        }

        public override void ReadData()
        {
            var fileSize = new FileInfo(Filename).Length;

            using (StreamReader sr = new StreamReader(Filename))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine() ?? string.Empty;
                    Data.Add(line.ToList());
                } 
            }

            _xSize = Data.Count;
            if (XSize > 0)
            {
                _ySize = Data[0].Count;
            }
        }
    }
}
