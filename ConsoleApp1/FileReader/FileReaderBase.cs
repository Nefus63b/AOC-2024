
namespace ConsoleApp1.FileReader
{
    public abstract class FileReaderBase
    {
        public string Filename { get; set; }

        public abstract void ReadData();

        public FileReaderBase(string filename)
        {
            Filename = filename;
        }
    }
}
