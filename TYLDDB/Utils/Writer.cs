using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TYLDDB.Utils
{
    /// <summary>
    /// A class used to write contents into the file<br />
    /// 用于写入文件的类 
    /// </summary>
    public struct Writer
    {
        // 同步写入大字符串到文件
        public static void WriteStringToFile(string filePath, string content, bool append = false)
        {
            // 使用64KB的缓冲区
            int bufferSize = 64 * 1024; // 64KB 缓冲区

            // 确保文件路径所在的目录存在
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // 使用StreamWriter并指定缓冲区大小
            using (StreamWriter writer = new StreamWriter(filePath, append, Encoding.UTF8, bufferSize))
            {
                writer.Write(content);
            }
        }

        // 异步写入大字符串到文件
        public static async Task WriteStringToFileAsync(string filePath, string content, bool append = false)
        {
            // 使用64KB的缓冲区
            int bufferSize = 64 * 1024; // 64KB 缓冲区

            // 确保文件路径所在的目录存在
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // 使用StreamWriter并指定缓冲区大小
            using (StreamWriter writer = new StreamWriter(filePath, append, Encoding.UTF8, bufferSize))
            {
                // 异步写入内容
                await writer.WriteAsync(content);
            }
        }

        // 分块写入大字符串到文件
        public static void WriteStringToFileInChunks(string filePath, string content, int chunkSize = 64 * 1024)
        {
            // 确保文件路径所在的目录存在
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8, 64 * 1024))
            {
                int totalLength = content.Length;
                int offset = 0;

                while (offset < totalLength)
                {
                    int length = Math.Min(chunkSize, totalLength - offset);
                    writer.Write(content.Substring(offset, length));
                    offset += length;
                }
            }
        }

        // 分块写入大字符串到文件
        public static async Task WriteStringToFileInChunksAsync(string filePath, string content, int chunkSize = 64 * 1024, bool append = false)
        {
            // 确保文件路径所在的目录存在
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (StreamWriter writer = new StreamWriter(filePath, append, Encoding.UTF8, 64 * 1024))
            {
                int totalLength = content.Length;
                int offset = 0;

                // 分块写入文件
                while (offset < totalLength)
                {
                    // 计算当前块的大小
                    int length = Math.Min(chunkSize, totalLength - offset);
                    string chunk = content.Substring(offset, length);

                    // 异步写入当前块
                    await writer.WriteAsync(chunk);

                    // 更新偏移量
                    offset += length;
                }
            }
        }
    }
}
