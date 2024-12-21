using System.IO;
using TYLDDB.Basic;

namespace TYLDDB.Utils
{
    internal class ReadFile
    {
        public static string ReadTylddbFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }
            }
            catch (TylddbException ex)
            {
                throw new FileReadingFailException($"Error reading file: {ex.Message}");
            }
        }
    }
}
