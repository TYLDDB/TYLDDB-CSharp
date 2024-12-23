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
{
// 获取文件信息
        FileInfo fileInfo = new FileInfo(filePath);

        // 获取文件大小 (单位字节)
        long fileSize = fileInfo.Length;

        // 根据文件大小区间进行不同的处理
        if (fileSize < 1 * 1024 * 1024) // 小于 1MB
        {
            ReadSmallFile(filePath);
        }
        else if (fileSize < 15 * 1024 * 1024) // 小于 15MB
        {
            ReadMediumFile(filePath);
        }
        else
        {
            Console.WriteLine("文件较大 (> 10MB 且 <= 25MB)，使用缓冲读取方式处理。");
            ReadLargeFile(filePath);
        }
}
// 定义委托类型，表示文件处理的方式
    private delegate void FileProcessingStrategy(string filePath);

    // 使用一个字典来映射文件大小区间到处理方法
    private static readonly Dictionary<(long, long), FileProcessingStrategy> FileProcessingStrategies
        = new Dictionary<(long, long), FileProcessingStrategy>
    {
        { (0, 1 * 1024 * 1024), ReadSmallFile },        // 小于 1MB
        { (1 * 1024 * 1024, 15 * 1024 * 1024), ReadMediumFile }, // 1MB 到 15MB
        { (15 * 1024 * 1024, long.MaxValue), ReadLargeFile }   // 大于 15MB
    };

    public static void ReadFilePlus(string filePath)
    {
        // 获取文件信息
        FileInfo fileInfo = new FileInfo(filePath);
        long fileSize = fileInfo.Length;

        // 根据文件大小选择合适的处理方法
        foreach (var entry in FileProcessingStrategies)
        {
            var (minSize, maxSize) = entry.Key;
            if (fileSize > minSize && fileSize <= maxSize)
            {
                entry.Value(filePath); // 调用相应的处理策略
                break;
            }
        }
    }
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
                byte[] buffer = new byte[65536];  // 缓冲区大小
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
