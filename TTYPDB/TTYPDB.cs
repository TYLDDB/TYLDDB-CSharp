using TTYPDB.Utils;

namespace TTYPDB
{
    public class TTYPDB
    {
        public string filePath;

        private string _fileContent;

        public void ReadingFile()
        {
            _fileContent = ReadFile.ReadTtypdbFile(filePath);
        }
    }
}
