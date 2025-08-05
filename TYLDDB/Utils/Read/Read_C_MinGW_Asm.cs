using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TYLDDB.Utils.Read
{
    internal class Read_C_MinGW_Asm : IDisposable
    {
        // 导入原生函数 - 完全匹配C代码签名
        [DllImport("libs/mingw/libRFAsmM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
        private static extern IntPtr read_file(string filename);

        [DllImport("libs/mingw/libRFAsmM.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        private static extern void free_file_buffer(IntPtr buffer);

        private IntPtr _nativeBuffer = IntPtr.Zero;
        private bool _disposed = false;
        private const string ERROR_STRING = "FILE_READ_ERROR";

        /// <summary>
        /// Read the content of the file as a string (in UTF-8 encoding)
        /// </summary>
        public string ReadFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("文件路径不能为空", nameof(path));

            ReleaseResources();

            _nativeBuffer = read_file(path);

            // 处理错误情况
            if (_nativeBuffer == IntPtr.Zero)
            {
                throw new System.IO.IOException($"文件读取失败: {path} (返回空指针)");
            }

            // 检查是否为错误字符串
            string errorCheck = Marshal.PtrToStringAnsi(_nativeBuffer);
            if (errorCheck == ERROR_STRING)
            {
                throw new System.IO.IOException($"文件读取失败: {path} (原生代码错误)");
            }

            // 安全地转换结果
            try
            {
                return MarshalNativeBufferToString();
            }
            catch (AccessViolationException ex)
            {
                throw new System.IO.IOException(
                    $"内存访问错误: {path}. 原生缓冲区无效: 0x{_nativeBuffer.ToInt64():X}", ex);
            }
        }

        /// <summary>
        /// Check whether the returned pointer is an erroneous string
        /// </summary>
        private bool IsErrorString(IntPtr buffer)
        {
            if (buffer == IntPtr.Zero) return false;

            // 将原生字符串转换为托管字符串进行比较
            string result = Marshal.PtrToStringAnsi(buffer);
            return result == ERROR_STRING;
        }

        /// <summary>
        /// Convert the native buffer to a string
        /// </summary>
        private unsafe string MarshalNativeBufferToString()
        {
            // 获取字符串长度（查找null终止符）
            byte* ptr = (byte*)_nativeBuffer;
            int length = 0;
            while (ptr[length] != 0) length++;

            // 直接创建字符串避免额外复制
            return Encoding.UTF8.GetString(ptr, length);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            ReleaseResources();
            _disposed = true;
        }

        private void ReleaseResources()
        {
            if (_nativeBuffer != IntPtr.Zero)
            {
                // 仅释放非错误字符串的缓冲区
                try
                {
                    string errorCheck = Marshal.PtrToStringAnsi(_nativeBuffer);
                    if (errorCheck != ERROR_STRING)
                    {
                        free_file_buffer(_nativeBuffer);
                    }
                }
                catch
                {
                    // 即使检查失败也尝试释放
                    free_file_buffer(_nativeBuffer);
                }

                _nativeBuffer = IntPtr.Zero;
            }
        }

        ~Read_C_MinGW_Asm()
        {
            Dispose(false);
        }
    }
}
