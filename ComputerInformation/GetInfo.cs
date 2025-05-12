using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace ComputerInformation
{
    public class GetInfo
    {
        public static string SystemInfoHash() => ComputeSha256Hash(GetSystemInfo());

        // 获取系统信息，并拼接为一个单行字符串
        public static string GetSystemInfo()
        {
            // 获取系统信息并拼接
            string osVersion = Environment.OSVersion.ToString();
            string osArchitecture = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
            string userDomainName = Environment.UserDomainName;
            string machineName = Environment.MachineName;
            string userName = Environment.UserName;
            string cpuModel = GetCpuModel();
            string cpuCores = GetCpuCoreCount().ToString();
            string totalMemory = GetTotalMemory().ToString();
            string memoryFrequency = GetMemoryFrequency();

            // 拼接为一个单行字符串，用英文分号分隔
            return $"{osVersion};{osArchitecture};{userDomainName};{machineName};{userName};{cpuModel};{cpuCores};{totalMemory};{memoryFrequency}";
        }

        // 获取 CPU 型号
        public static string GetCpuModel()
        {
            string cpuModel;
            // 获取 CPU 型号的方法是通过读取注册表
            try
            {
                var cpu = Process.Start(new ProcessStartInfo
                {
                    FileName = "wmic",
                    Arguments = "cpu get caption",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                cpu!.WaitForExit();
                cpuModel = cpu.StandardOutput.ReadToEnd().Trim().Split('\n')[1].Trim();
            }
            catch
            {
                cpuModel = "Unknown CPU";
            }

            return cpuModel;
        }

        // 获取 CPU 内核数
        public static int GetCpuCoreCount()
        {
            int coreCount = 0;
            try
            {
                var core = Process.Start(new ProcessStartInfo
                {
                    FileName = "wmic",
                    Arguments = "cpu get NumberOfCores",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                core!.WaitForExit();
                string output = core.StandardOutput.ReadToEnd().Trim();
                if (output.Contains('\n'))
                {
                    coreCount = int.Parse(output.Split('\n')[1].Trim());
                }
            }
            catch
            {
                coreCount = 0;
            }

            return coreCount;
        }

        // 获取总内存大小（以 GB 为单位）
        public static double GetTotalMemory()
        {
            double totalMemory;
            try
            {
                // 使用 wmic 命令获取物理内存
                var memory = Process.Start(new ProcessStartInfo
                {
                    FileName = "wmic",
                    Arguments = "MemoryChip get Capacity",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                memory!.WaitForExit();
                string output = memory.StandardOutput.ReadToEnd().Trim();
                string[] lines = output.Split('\n');
                double totalCapacity = 0;
                foreach (var line in lines)
                {
                    if (line.Trim() != "")
                    {
                        totalCapacity += double.Parse(line.Trim());
                    }
                }
                totalMemory = totalCapacity / (1024 * 1024 * 1024); // 转换为 GB
            }
            catch
            {
                totalMemory = 0;
            }

            return totalMemory;
        }

        // 获取内存频率
        public static string GetMemoryFrequency()
        {
            string memoryFrequency = "Unknown";
            try
            {
                var memorySpeed = Process.Start(new ProcessStartInfo
                {
                    FileName = "wmic",
                    Arguments = "MemoryChip get Speed",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                memorySpeed!.WaitForExit();
                string output = memorySpeed.StandardOutput.ReadToEnd().Trim();
                string[] lines = output.Split('\n');
                if (lines.Length > 1)
                {
                    memoryFrequency = lines[1].Trim();
                }
            }
            catch
            {
                memoryFrequency = "Unknown";
            }

            return memoryFrequency;
        }

        // 计算字符串的SHA-256哈希值
        static string ComputeSha256Hash(string rawData)
        {
            // 计算哈希值
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

            // 将字节数组转换为十六进制字符串
            var builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
