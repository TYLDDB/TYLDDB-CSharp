using System.IO;
using System.Text;
using TYLDDB.Basic;

namespace TYLDDB.Utils
{
    /// <summary>
    /// A struct used to read a file<br />
    /// 用于读取文件的结构体
    /// </summary>
    public struct ReadFile
    {
        /// <summary>
        /// The earliest reading method based on FileStream + SreamReader.<br />
        /// 最早的基于 FileStream + SreamReader 的读取方法。
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        /// <exception cref="FileReadingFailException"></exception>
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

        /// <summary>
        /// Select the desired method based on the file size<br />
        /// 根据文件大小选择所需的方法
        /// <list type="bullet">＜1MB <see cref="ReadSmallFile(string)"/></list>
        /// <list type="bullet">1MB~10MB <see cref="ReadMediumFile(string)"/></list>
        /// <list type="bullet">10MB~15MB <see cref="ReadLargeFile_8(string)"/></list>
        /// <list type="bullet">15MB~20MB <see cref="ReadLargeFile_64(string)"/></list>
        /// <list type="bullet">20MB~30MB <see cref="ReadLargeFile_128(string)"/></list>
        /// <list type="bullet">＞30MB <see cref="ReadVeryLargeFile(string)"/></list>
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        public static string ReadFileAuto(string filePath)
        {
            // 获取文件信息
            FileInfo fileInfo = new FileInfo(filePath);

            // 获取文件大小 (单位字节)
            long fileSize = fileInfo.Length;

            // 根据文件大小区间进行不同的处理
            if (fileSize < 1 * 1024 * 1024) // 小于 1MB
            {
                return ReadSmallFile(filePath);
            }
            else if (fileSize < 10 * 1024 * 1024) // 小于 10MB
            {
                return ReadMediumFile(filePath);
            }
            else if(fileSize < 15 * 1024 * 1024) // 小于 15MB
            {
                return ReadLargeFile_8(filePath);
            }
            else if(fileSize < 20 * 1024 * 1024) // 小于 20MB
            {
                return ReadLargeFile_64(filePath);
            }
            else if (fileSize < 30 * 1024 * 1024) // 小于 30MB
            {
                return ReadLargeFile_128(filePath);
            }
            else // 大于 30MB
            {
                return ReadVeryLargeFile(filePath);
            }
        }

        /// <summary>
        /// Read small file.<br />
        /// 读取小文件。
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadSmallFile(string filePath) => File.ReadAllText(filePath);

        /// <summary>
        /// Read medium file.<br />
        /// 读取中等大小文件。
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadMediumFile(string filePath)
        {
            // 使用 FileStream 和 BufferedStream 进行高效读取
            StringBuilder fileContent = new StringBuilder(); // 用来存储文件内容

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader reader = new StreamReader(bs, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // 将每一行的内容追加到 fileContent
                    fileContent.AppendLine(line);
                }
            }

            return fileContent.ToString(); // 返回文件的所有内容
        }

        /// <summary>
        /// Read large file.(8KB buffer)<br />
        /// 读取大文件。(8KB缓冲区)
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadLargeFile_8(string filePath)
        {
            // 这里采用分块读取文件的方式，避免一次性加载大文件
            const int bufferSize = 8192; // 8 KB缓冲区
            byte[] buffer = new byte[bufferSize];
            StringBuilder fileContent = new StringBuilder();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // 将每一块数据转换成字符串并累积到 fileContent 中
                    string chunkContent = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    fileContent.Append(chunkContent);
                }
            }

            // 返回文件的全部内容
            return fileContent.ToString();
        }

        /// <summary>
        /// Read large file.(64KB buffer)<br />
        /// 读取大文件。(64KB缓冲区)
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadLargeFile_64(string filePath)
        {
            // 这里采用分块读取文件的方式，避免一次性加载大文件
            const int bufferSize = 65536; // 64 KB缓冲区
            byte[] buffer = new byte[bufferSize];
            StringBuilder fileContent = new StringBuilder();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // 将每一块数据转换成字符串并累积到 fileContent 中
                    string chunkContent = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    fileContent.Append(chunkContent);
                }
            }

            // 返回文件的全部内容
            return fileContent.ToString();
        }

        /// <summary>
        /// Read large file.(128KB buffer)<br />
        /// 读取大文件。(128KB缓冲区)
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadLargeFile_128(string filePath)
        {
            // 这里采用分块读取文件的方式，避免一次性加载大文件
            const int bufferSize = 131072; // 128 KB缓冲区
            byte[] buffer = new byte[bufferSize];
            StringBuilder fileContent = new StringBuilder();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // 将每一块数据转换成字符串并累积到 fileContent 中
                    string chunkContent = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    fileContent.Append(chunkContent);
                }
            }

            // 返回文件的全部内容
            return fileContent.ToString();
        }

        // 使用 MemoryMappedFile 读取非常大的文件
        /// <summary>
        /// Read very large file.(Based on MemoryMappedFile)<br />
        /// 读取很大的文件。(基于MemoryMappedFile)
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadVeryLargeFile(string filePath)
        {
            using (System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateFromFile(filePath, FileMode.Open))
            {
                using (System.IO.MemoryMappedFiles.MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                {
                    long length = accessor.Capacity;
                    byte[] buffer = new byte[length];
                    accessor.ReadArray(0, buffer, 0, (int)length);
                    string content = Encoding.UTF8.GetString(buffer);
                    return content;
                }
            }
        }
    }
}
