using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TYLDDB.Utils.Read
{
    internal unsafe class Read_Cpp_MinGW : IDisposable
    {
        [DllImport("libs/mingw/libReadFileCpp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr OpenTextFile(string filePath);

        [DllImport("libs/mingw/libReadFileCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern long GetTextFileSize(IntPtr handle);

        [DllImport("libs/mingw/libReadFileCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetFileContent(IntPtr handle);

        [DllImport("libs/mingw/libReadFileCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CloseTextFile(IntPtr handle);

        private IntPtr _fileHandle = IntPtr.Zero;
        private byte* _contentPointer = null;
        private long _fileSize = 0;

        public bool IsLoaded => _contentPointer != null && _fileSize > 0;
        public long FileSize => _fileSize;

        public void Open(string filePath)
        {
            Close();

            _fileHandle = OpenTextFile(filePath);
            if (_fileHandle == IntPtr.Zero)
            {
                throw new Exception($"无法打开文件: {filePath}");
            }

            _fileSize = GetTextFileSize(_fileHandle);
            if (_fileSize == 0)
            {
                Close();
                throw new Exception("文件为空");
            }

            IntPtr contentPtr = GetFileContent(_fileHandle);
            _contentPointer = (byte*)contentPtr.ToPointer();
        }

        // 方法1: 返回整个文件内容作为字符串
        public string GetContentAsString(Encoding encoding = null)
        {
            if (!IsLoaded)
                return string.Empty;

            encoding ??= Encoding.UTF8;
            return encoding.GetString(_contentPointer, (int)_fileSize);
        }

        // 方法2: 返回文件内容作为行列表
        public List<string> GetContentAsLines(Encoding encoding = null)
        {
            var lines = new List<string>();
            if (!IsLoaded)
                return lines;

            encoding ??= Encoding.UTF8;
            long position = 0;
            long lineStart = 0;

            while (position < _fileSize)
            {
                if (_contentPointer[position] == '\n')
                {
                    // 计算行长度（排除\r\n）
                    long lineLength = position - lineStart;
                    if (lineLength > 0 && _contentPointer[position - 1] == '\r')
                    {
                        lineLength--;
                    }

                    // 添加行到列表
                    string line = encoding.GetString(_contentPointer + lineStart, (int)lineLength);
                    lines.Add(line);

                    // 移动到下一行
                    lineStart = position + 1;
                }
                position++;
            }

            // 添加最后一行（如果没有换行符结尾）
            if (lineStart < _fileSize)
            {
                long lineLength = _fileSize - lineStart;
                string line = encoding.GetString(_contentPointer + lineStart, (int)lineLength);
                lines.Add(line);
            }

            return lines;
        }

        // 方法3: 返回文件内容作为字节数组
        public byte[] GetContentAsBytes()
        {
            if (!IsLoaded)
                return Array.Empty<byte>();

            byte[] result = new byte[_fileSize];
            Marshal.Copy((IntPtr)_contentPointer, result, 0, (int)_fileSize);
            return result;
        }

        // 方法4: 返回文件内容作为内存指针（最高性能）
        public IntPtr GetContentPointer()
        {
            return IsLoaded ? (IntPtr)_contentPointer : IntPtr.Zero;
        }

        public void Close()
        {
            if (_fileHandle != IntPtr.Zero)
            {
                CloseTextFile(_fileHandle);
                _fileHandle = IntPtr.Zero;
            }

            _contentPointer = null;
            _fileSize = 0;
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        // 析构函数（安全网）
        ~Read_Cpp_MinGW()
        {
            Close();
        }
    }
}
