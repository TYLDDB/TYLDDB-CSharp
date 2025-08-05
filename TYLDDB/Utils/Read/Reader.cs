using System;
using System.IO;
using System.Security;
using System.Text;
using TYLDDB.Basic.Exception;

namespace TYLDDB.Utils.Read
{
    /// <summary>
    /// A struct used to read a file<br />
    /// 用于读取文件的结构体
    /// </summary>
    internal struct Reader
    {
        /// <summary>
        /// Read file.(64KB buffer)<br />
        /// 读取文件。(64KB缓冲区)
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadFile(string filePath)
        {
            try
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
            catch(Exception e)
            {
                throw new ReadException(e.Message);
            }
        }

        /// <summary>
        /// Read file.(Custom buffer)<br />
        /// 读取文件。(自定义缓冲区)
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <param name="bufferSize">Buffer size<br />缓冲区大小</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadFile(string filePath, int bufferSize)
        {
            try
            {
                // 这里采用分块读取文件的方式，避免一次性加载大文件
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
            catch (Exception e)
            {
                throw new ReadException(e.Message);
            }
        }

#if NET8_0_OR_GREATER
        public static string ReadFile_C_MinGW(string filePath)
        {
            using var reader = new Read_C_MinGW();
            int error = reader.Open(Path.GetFullPath(filePath));

            if (error == 0)
            {
                // 自动检测编码
                string content = reader.GetText();
                //Console.WriteLine($"文件内容长度: {content.Length}");
                return content;
            }
            else
            {
                //Console.WriteLine($"错误代码: {error}");
                throw new FileReadingFailException("Error code: " + error.ToString());
            }
        }

        public static string ReadFile_C_VS(string filePath)
        {
            using var reader = new Read_C_VS();
            int error = reader.Open(Path.GetFullPath(filePath));

            if (error == 0)
            {
                // 自动检测编码
                string content = reader.GetText();
                //Console.WriteLine($"文件内容长度: {content.Length}");
                return content;
            }
            else
            {
                //Console.WriteLine($"错误代码: {error}");
                throw new FileReadingFailException("Error code: " + error.ToString());
            }
        }
#endif
        private static string ReadFile_C_MinGW_Asm(string filePath)
        {
            try
            {
                using var reader = new Read_C_MinGW_Asm();

                // 读取成功
                string content = reader.ReadFile(Path.GetFullPath(filePath));
                return content;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
