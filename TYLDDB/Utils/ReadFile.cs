using System.IO;
using TYLDDB.Basic;

namespace TYLDDB.Utils
{
    internal class ReadFile
    {
        public static string ReadTtypdbFile(string filePath)
        {
#if NETSTANDARD1_6
            return NetStandard1_6(filePath);
#elif NETSTANDARD2_0_OR_GREATER
            return NetStandard2_0_Greater(filePath);
#elif NET6_0
            return Net6_0(filePath);
#elif NET8_0_OR_GREATER
            return Net8_0(filePath);
#endif
        }

#if NETSTANDARD1_6
        private static string NetStandard1_6(string filePath)
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
            catch (TtypdbException ex)
            {
                throw new FileReadingFailException($"Error reading file: {ex.Message}");
            }
        }
#endif

#if NETSTANDARD2_0_OR_GREATER
        private static string NetStandard2_0_Greater(string filePath)
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
            catch (TtypdbException ex)
            {
                throw new FileReadingFailException($"Error reading file: {ex.Message}");
            }
        }
#endif

#if NET6_0
        private static string Net6_0(string filePath)
        {
            try
            {
                using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new(fs);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (TtypdbException ex)
            {
                throw new FileReadingFailException($"Error reading file: {ex.Message}");
            }
        }
#endif

#if NET8_0_OR_GREATER
        private static string Net8_0(string filePath)
        {
            try
            {
                using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new(fs);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (TtypdbException ex)
            {
                throw new FileReadingFailException($"Error reading file: {ex.Message}");
            }
        }
#endif
    }
}
