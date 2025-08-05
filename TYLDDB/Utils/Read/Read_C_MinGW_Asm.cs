using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TYLDDB.Utils.Read
{
    internal class Read_C_MinGW_Asm : IDisposable
    {
        // 导入原生函数 - 完全匹配C代码签名
        [DllImport("libs/mingw/libRFAsmM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern IntPtr read_file(string filename);

        [DllImport("libs/mingw/libRFAsmM.dll", CallingConvention = CallingConvention.Cdecl)]
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
                throw new ArgumentException("File path cannot be empty", nameof(path));

            ReleaseResources(); // 释放前次资源

            _nativeBuffer = read_file(path);

            // 检查是否返回错误字符串
            if (IsErrorString(_nativeBuffer))
            {
                throw new System.IO.IOException($"Failed to read file: {path}");
            }

            // 检查空指针
            if (_nativeBuffer == IntPtr.Zero)
            {
                throw new System.IO.IOException($"File read returned null pointer for: {path}");
            }

            return MarshalNativeBufferToString();
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
                // 只有当不是错误字符串时才释放
                if (!IsErrorString(_nativeBuffer))
                {
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
