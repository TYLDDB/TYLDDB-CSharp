#if NET8_0_OR_GREATER
#nullable enable
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TYLDDB.Utils.Read
{
    internal class Read_C_VS : IDisposable
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct MappedFileResult
        {
            public IntPtr MappedView;
            public nuint FileSize;
            public IntPtr InternalHandles;
        }

        [DllImport("libs/vs/ReadFile.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private static extern int open_mapped_file(
            [MarshalAs(UnmanagedType.LPWStr)] string filePath,
            out MappedFileResult result);

        [DllImport("libs/vs/ReadFile.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void free_mapped_file(ref MappedFileResult result);

        private MappedFileResult _result;
        private bool _disposed;
        private string? _cachedString;
        private Encoding? _detectedEncoding;

        public IntPtr MappedView => _result.MappedView;
        public nuint FileSize => _result.FileSize;
        public bool IsMapped => MappedView != IntPtr.Zero;

        public unsafe ReadOnlySpan<byte> DataSpan
        {
            get
            {
                if (!IsMapped) return ReadOnlySpan<byte>.Empty;
                return new ReadOnlySpan<byte>((void*)MappedView, (int)FileSize);
            }
        }

        public int Open(string filePath)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Read_C_VS));
            Close();

            int error = open_mapped_file(filePath, out _result);
            if (error == 0)
            {
                DetectEncoding();
            }
            return error;
        }

        private void DetectEncoding()
        {
            if (!IsMapped || FileSize == 0) return;

            ReadOnlySpan<byte> data = DataSpan;
            _detectedEncoding = DetectEncodingFromBOM(data);
        }

        private static Encoding? DetectEncodingFromBOM(ReadOnlySpan<byte> data)
        {
            if (data.Length >= 3)
            {
                // UTF-8 BOM: EF BB BF
                if (data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF)
                    return Encoding.UTF8;
            }

            if (data.Length >= 2)
            {
                // UTF-16 Little Endian BOM: FF FE
                if (data[0] == 0xFF && data[1] == 0xFE)
                    return Encoding.Unicode;

                // UTF-16 Big Endian BOM: FE FF
                if (data[0] == 0xFE && data[1] == 0xFF)
                    return Encoding.BigEndianUnicode;
            }

            if (data.Length >= 4)
            {
                // UTF-32 Little Endian BOM: FF FE 00 00
                if (data[0] == 0xFF && data[1] == 0xFE && data[2] == 0 && data[3] == 0)
                    return Encoding.UTF32;

                // UTF-32 Big Endian BOM: 00 00 FE FF
                if (data[0] == 0 && data[1] == 0 && data[2] == 0xFE && data[3] == 0xFF)
                    return new UTF32Encoding(true, true);
            }

            // 默认使用UTF-8 (无BOM时)
            return Encoding.UTF8;
        }

        public void Close()
        {
            if (!_disposed && (_result.MappedView != IntPtr.Zero || _result.InternalHandles != IntPtr.Zero))
            {
                free_mapped_file(ref _result);
                _cachedString = null;
                _detectedEncoding = null;
            }
        }

        // 高性能字符串转换（使用缓存）
        public string GetText(Encoding? encoding = null)
        {
            if (!IsMapped) return string.Empty;

            // 使用缓存
            if (_cachedString != null) return _cachedString;

            ReadOnlySpan<byte> data = DataSpan;

            // 使用检测到的编码或指定的编码
            Encoding finalEncoding = encoding ?? _detectedEncoding ?? Encoding.UTF8;

            // 跳过BOM（如果存在）
            int bomLength = finalEncoding.Preamble.Length;
            if (bomLength > 0 && data.Length >= bomLength)
            {
                if (data.Slice(0, bomLength).SequenceEqual(finalEncoding.Preamble))
                {
                    data = data.Slice(bomLength);
                }
            }

            // 直接转换（避免复制）
            _cachedString = finalEncoding.GetString(data);
            return _cachedString;
        }

        // 流式处理文本的替代方案 - 使用 ReadOnlySpan<byte> 和逐行处理
        public void ProcessTextByLines(Action<string> lineProcessor, Encoding? encoding = null)
        {
            if (!IsMapped) return;

            ReadOnlySpan<byte> data = DataSpan;
            Encoding finalEncoding = encoding ?? _detectedEncoding ?? Encoding.UTF8;

            // 跳过BOM
            int bomLength = finalEncoding.Preamble.Length;
            if (bomLength > 0 && data.Length >= bomLength)
            {
                if (data.Slice(0, bomLength).SequenceEqual(finalEncoding.Preamble))
                {
                    data = data.Slice(bomLength);
                }
            }

            // 高效逐行处理
            int start = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '\n' || (data[i] == '\r' && (i + 1 >= data.Length || data[i + 1] != '\n')))
                {
                    // 处理单行
                    ReadOnlySpan<byte> lineBytes = data.Slice(start, i - start);

                    // 移除可能的回车符
                    if (lineBytes.Length > 0 && lineBytes[lineBytes.Length - 1] == '\r')
                    {
                        lineBytes = lineBytes.Slice(0, lineBytes.Length - 1);
                    }

                    // 转换为字符串
                    string line = finalEncoding.GetString(lineBytes);
                    lineProcessor(line);

                    // 移动到下一行开始
                    start = i + 1;
                }
                else if (data[i] == '\r' && i + 1 < data.Length && data[i + 1] == '\n')
                {
                    // 处理Windows换行符
                    ReadOnlySpan<byte> lineBytes = data.Slice(start, i - start);
                    string line = finalEncoding.GetString(lineBytes);
                    lineProcessor(line);

                    // 跳过两个字符
                    start = i + 2;
                    i++; // 额外前进一个位置
                }
            }

            // 处理最后一行
            if (start < data.Length)
            {
                ReadOnlySpan<byte> lineBytes = data.Slice(start);
                string line = finalEncoding.GetString(lineBytes);
                lineProcessor(line);
            }
        }

        // 替代方案2：使用内存安全API处理大文本
        public unsafe void ProcessTextWithMemory(Action<IntPtr, int> processor, Encoding? encoding = null)
        {
            if (!IsMapped) return;

            ReadOnlySpan<byte> data = DataSpan;
            Encoding finalEncoding = encoding ?? _detectedEncoding ?? Encoding.UTF8;

            // 跳过BOM
            int bomLength = finalEncoding.Preamble.Length;
            if (bomLength > 0 && data.Length >= bomLength)
            {
                if (data.Slice(0, bomLength).SequenceEqual(finalEncoding.Preamble))
                {
                    data = data.Slice(bomLength);
                }
            }

            // 转换整个缓冲区
            fixed (byte* pData = data)
            {
                int charCount = finalEncoding.GetCharCount(pData, data.Length);
                char[] buffer = new char[charCount];

                fixed (char* pBuffer = buffer)
                {
                    finalEncoding.GetChars(pData, data.Length, pBuffer, charCount);
                    processor((IntPtr)pBuffer, charCount);
                }
            }
        }

        public void Dispose()
        {
            Close();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        ~Read_C_VS() => Dispose();
    }
}
#nullable restore
#endif
