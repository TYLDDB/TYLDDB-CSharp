using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TYLDDB.Utils.Read
{
    internal class Read_C_MinGW_Asm : IDisposable
    {
        private const string ERROR_STRING = "FILE_READ_ERROR";
        private IntPtr _nativeBuffer = IntPtr.Zero;
        private bool _disposed = false;
        private static readonly object _lock = new object();

        // 修复调用约定和字符串处理
        [DllImport("libs/mingw/libRFAsmM.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Ansi,
            EntryPoint = "read_file",
            ExactSpelling = false)]
        private static extern IntPtr NativeReadFile(string filename);

        [DllImport("libs/mingw/libRFAsmM.dll",
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "free_file_buffer",
            ExactSpelling = false)]
        private static extern void NativeFreeFileBuffer(IntPtr buffer);

        public string ReadFile(string path)
        {
            lock (_lock) // 添加线程安全锁
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(path))
                        throw new ArgumentException("文件路径不能为空", nameof(path));

                    // 获取完整路径确保格式正确
                    string fullPath = Path.GetFullPath(path);
                    Console.WriteLine($"尝试读取文件: {fullPath}");

                    // 确保文件存在
                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException($"文件不存在: {fullPath}");
                    }

                    ReleaseResources();

                    // 调用原生函数
                    _nativeBuffer = NativeReadFile(fullPath);

                    // 检查空指针
                    if (_nativeBuffer == IntPtr.Zero)
                    {
                        throw new IOException($"文件读取失败: {fullPath} (返回空指针)");
                    }

                    // 检查是否为错误字符串
                    string errorCheck = SafePtrToStringAnsi(_nativeBuffer, 50);
                    if (errorCheck == ERROR_STRING)
                    {
                        throw new IOException($"文件读取失败: {fullPath} (原生代码错误)");
                    }

                    // 安全地转换结果
                    return SafeMarshalNativeBufferToString();
                }
                catch (Exception ex) // 捕获所有类型的异常
                {
                    // 释放资源并重新抛出
                    ReleaseResources();
                    throw new IOException($"读取文件时出错: {path}", ex);
                }
            }
        }

        // 安全的 PtrToStringAnsi 替代方法
        private string SafePtrToStringAnsi(IntPtr ptr, int maxLength = 1024)
        {
            if (ptr == IntPtr.Zero) return string.Empty;

            int length = 0;
            while (length < maxLength && Marshal.ReadByte(ptr, length) != 0)
                length++;

            if (length == 0) return string.Empty;

            byte[] buffer = new byte[length];
            Marshal.Copy(ptr, buffer, 0, length);
            return Encoding.ASCII.GetString(buffer);
        }

        // 安全地将原生缓冲区转换为字符串
        private string SafeMarshalNativeBufferToString()
        {
            if (_nativeBuffer == IntPtr.Zero)
                throw new InvalidOperationException("原生缓冲区为空");

            // 更安全的长度计算方法
            int length = 0;
            while (length < int.MaxValue - 1 && Marshal.ReadByte(_nativeBuffer, length) != 0)
                length++;

            if (length == 0) return string.Empty;

            // 使用字节数组复制内容
            byte[] buffer = new byte[length];
            Marshal.Copy(_nativeBuffer, buffer, 0, length);

            // 尝试检测编码
            Encoding encoding = DetectEncoding(buffer) ?? Encoding.UTF8;
            return encoding.GetString(buffer);
        }

        // 简单的编码检测
        private Encoding DetectEncoding(byte[] buffer)
        {
            if (buffer.Length >= 3 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
            {
                return Encoding.UTF8;
            }
            else if (buffer.Length >= 2 && buffer[0] == 0xFF && buffer[1] == 0xFE)
            {
                return Encoding.Unicode;
            }
            else if (buffer.Length >= 2 && buffer[0] == 0xFE && buffer[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode;
            }
            return null; // 无法确定编码
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
                try
                {
                    // 仅释放非错误字符串的缓冲区
                    string errorCheck = SafePtrToStringAnsi(_nativeBuffer, 20);
                    if (errorCheck != ERROR_STRING)
                    {
                        NativeFreeFileBuffer(_nativeBuffer);
                    }
                    else
                    {
                        Console.WriteLine("跳过释放错误字符串缓冲区");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"释放资源时出错: {ex.Message}");
                    // 即使检查失败也尝试释放
                    try { NativeFreeFileBuffer(_nativeBuffer); } catch { }
                }
                finally
                {
                    _nativeBuffer = IntPtr.Zero;
                }
            }
        }

        ~Read_C_MinGW_Asm()
        {
            Dispose(false);
        }
    }
}
