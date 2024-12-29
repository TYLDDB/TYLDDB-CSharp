using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TYLDDB.Utils
{
    /// <summary>
    /// A class used to write contents into the file.
    /// <list type="bullet"><see cref="WriteStringToFile(string, string)"/>Suitable for writing to slower hard drives, such as mechanical hard drives.</list>
    /// <list type="bullet"><see cref="WriteStringToFileAsync(string, string)"/>Suitable for asynchronously writing to slow hard drives, such as mechanical hard drives.</list>
    /// <list type="bullet"><see cref="WriteStringToFileInChunks(string, string, int)"/>Ideal for writing to fast hard drives, such as solid state drives.</list>
    /// <list type="bullet"><see cref="WriteStringToFileInChunksAsync(string, string, int)"/>Ideal for asynchronously writing content in faster hard drives, such as solid state drives.</list>
    /// 用于写入文件的类。
    /// <list type="bullet"><see cref="WriteStringToFile(string, string)"/>适用于在较慢的硬盘中写入内容，如机械硬盘。</list>
    /// <list type="bullet"><see cref="WriteStringToFileAsync(string, string)"/>适用于在较慢的硬盘中异步写入内容，如机械硬盘。</list>
    /// <list type="bullet"><see cref="WriteStringToFileInChunks(string, string, int)"/>适用于在较快的硬盘中写入内容，如固态硬盘。</list>
    /// <list type="bullet"><see cref="WriteStringToFileInChunksAsync(string, string, int)"/>适用于在较快的硬盘中异步写入内容，如固态硬盘。</list>
    /// </summary>
    public struct Writer
    {
        /// <summary>
        /// Write large strings to a file synchronously.<br />
        /// 同步写入大字符串到文件。
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <param name="content">Written content<br />写入的内容</param>
        public static void WriteStringToFile(string filePath, string content)
        {
            // 使用64KB的缓冲区
            int bufferSize = 64 * 1024; // 64KB 缓冲区

            // 使用FileStream打开文件，确保文件完全覆盖而不是追加
            using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize);
            using StreamWriter writer = new StreamWriter(fs, Encoding.UTF8, bufferSize);
            // 写入内容
            writer.Write(content);
        }

        /// <summary>
        /// Asynchronously writes large strings to a file.<br />
        /// 异步写入大字符串到文件。
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <param name="content">Written content<br />写入的内容</param>
        public static async void WriteStringToFileAsync(string filePath, string content)
        {
            // 使用64KB的缓冲区
            int bufferSize = 64 * 1024; // 64KB 缓冲区

            // 使用FileStream打开文件，确保文件完全覆盖而不是追加
            using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize);
            using StreamWriter writer = new StreamWriter(fs, Encoding.UTF8, bufferSize);
            // 异步写入内容
            await writer.WriteAsync(content);
        }

        /// <summary>
        /// Write large strings to the file in blocks.<br />
        /// 分块写入大字符串到文件。
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <param name="content">Written content<br />写入的内容</param>
        /// <param name="chunkSize">Block size<br />分块大小</param>
        public static void WriteStringToFileInChunks(string filePath, string content, int chunkSize = 64 * 1024)
        {

            // 将字符串转换为字节数组
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);

            using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, chunkSize);
            using StreamWriter writer = new StreamWriter(fs, Encoding.UTF8, chunkSize);
            int totalLength = contentBytes.Length;
            int offset = 0;

            while (offset < totalLength)
            {
                int length = Math.Min(chunkSize, totalLength - offset);
                writer.Write(Encoding.UTF8.GetString(contentBytes, offset, length));
                offset += length;
            }
        }

        /// <summary>
        /// Asynchronously blocks large strings to a file.<br />
        /// 异步分块写入大字符串到文件。
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <param name="content">Written content<br />写入的内容</param>
        /// <param name="chunkSize">Block size<br />分块大小</param>
        public static async void WriteStringToFileInChunksAsync(string filePath, string content, int chunkSize = 64 * 1024)
        {
            // 将字符串转换为字节数组
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);

            using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, chunkSize);
            using StreamWriter writer = new StreamWriter(fs, Encoding.UTF8, chunkSize);
            int totalLength = contentBytes.Length;
            int offset = 0;

            while (offset < totalLength)
            {
                int length = Math.Min(chunkSize, totalLength - offset);
                string chunk = Encoding.UTF8.GetString(contentBytes, offset, length);

                // 异步写入当前块
                await writer.WriteAsync(chunk);

                // 更新偏移量
                offset += length;
            }
        }
    }
}
