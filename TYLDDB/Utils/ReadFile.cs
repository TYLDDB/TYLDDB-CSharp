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
internal class NewReader
{
public static string ReadFile(string filePath)
{return null;}
public static string ReadSmallFile(string filePath)
{return File.ReadAllText(filePath);}
public static string ReadMediumFile(string filePath)
    {
        // 使用 FileStream 和 BufferedStream 进行高效读取
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (BufferedStream bs = new BufferedStream(fs))
        {
            // 使用 MemoryStream 来动态存储文件内容
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = new byte[8192];  // 缓冲区大小
                int bytesRead;
                
                // 读取文件并将其内容写入 MemoryStream
                while ((bytesRead = bs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, bytesRead);
                }

                // 返回完整文件内容的字符串
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
public static string ReadLargeFile(string filePath)
    {
        using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open))
        {
            using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
            {
                byte[] buffer = new byte[accessor.Capacity];
                accessor.ReadArray(0, buffer, 0, (int)accessor.Capacity);
                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}
}
