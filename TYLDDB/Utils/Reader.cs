using System.IO;
using System.Text;

namespace TYLDDB.Utils
{
    /// <summary>
    /// A struct used to read a file<br />
    /// 用于读取文件的结构体
    /// </summary>
    public struct Reader
    {
        /// <summary>
        /// Read file.(64KB buffer)<br />
        /// 读取文件。(64KB缓冲区)
        /// </summary>
        /// <param name="filePath">File path<br />文件路径</param>
        /// <returns>File content<br />文件内容</returns>
        public static string ReadFile(string filePath)
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
    }
}
