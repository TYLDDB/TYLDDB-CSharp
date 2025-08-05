using ComputerInformation;
using QingYi.Core.Timer;
using TYLDDB;

internal class Program
{
    private static void Main()
    {
        InitTemp();

        var test = new Test();
        try
        {
            test.TestMethod();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        Console.ReadLine();
    }

    private static void InitTemp()
    {
        // 获取当前程序所在目录
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // 创建一个用于缓存的子文件夹路径，例如 "temp"
        string tempDirectory = Path.Combine(baseDirectory, "temp");

        // 确保该目录存在，如果不存在则创建
        if (!Directory.Exists(tempDirectory))
        {
            Directory.CreateDirectory(tempDirectory);
        }

        // 设置当前进程的 JIT 缓存目录 (只影响当前进程)
        Environment.SetEnvironmentVariable("CORECLR_JIT_CACHE", tempDirectory, EnvironmentVariableTarget.Process);
    }
}

class Test
{
    private readonly LDDB lddb = new();
    private readonly string dbFilePath = "./example.lddb";
    private readonly List<string> testData = [];

    public void TestMethod()
    {
        testData.Add($"Computer Id: {GetInfo.SystemInfoHash()}\n");

        testData.Add($"CPU: {GetInfo.GetCpuModel()} {GetInfo.GetCpuCoreCount()}");

        testData.Add($"Memory: {GetInfo.GetTotalMemory()} {GetInfo.GetMemoryFrequency()}");

        ReadFile();
        ReadFile_C_MinGW();
        ReadFile_C_VS();
        ReadFile_C_MinGW_Asm();

        ReadDatabase();

        GetAllDatabaseName();

        ParseDatabaseToTemp();

        CdAllTypeSearch();

        StlAllTypeSearch();

        TripleDictionaryTest();

        ExportTestData();
    }

    #region Test Method
    #region 读取文件
    private void ReadFile()
    {
        HighPrecisionTimer readDbTimer = new(); // 从发起读取文件到成功读取的总时间
        lddb.FilePath = dbFilePath;
        readDbTimer.Start();
        lddb.ReadingFile();
        readDbTimer.Stop();
        WriteTime("从发起读取文件指令到成功读取的总时间为: ", readDbTimer.GetElapsedMilliseconds());
    }

    private void ReadFile_C_MinGW()
    {
        HighPrecisionTimer readDbTimer = new(); // 从发起读取文件到成功读取的总时间
        lddb.FilePath = dbFilePath;
        readDbTimer.Start();
        lddb.ReadingFile_C_MinGW();
        readDbTimer.Stop();
        WriteTime("从发起读取文件指令到成功读取的总时间为(MinGW): ", readDbTimer.GetElapsedMilliseconds());
    }

    private void ReadFile_C_VS()
    {
        HighPrecisionTimer readDbTimer = new(); // 从发起读取文件到成功读取的总时间
        lddb.FilePath = dbFilePath;
        readDbTimer.Start();
        lddb.ReadingFile_C_VisualStudio();
        readDbTimer.Stop();
        WriteTime("从发起读取文件指令到成功读取的总时间为(VS): ", readDbTimer.GetElapsedMilliseconds());
    }

    private void ReadFile_C_MinGW_Asm()
    {
        HighPrecisionTimer readDbTimer = new(); // 从发起读取文件到成功读取的总时间
        lddb.FilePath = dbFilePath;
        readDbTimer.Start();
        lddb.ReadingFile_C_MinGW_Asm();
        readDbTimer.Stop();
        WriteTime("从发起读取文件指令到成功读取的总时间为(MinGW Asm): ", readDbTimer.GetElapsedMilliseconds());
    }
    #endregion

    private void ReadDatabase()
    {
        #region 读取数据库
        ///////////////////////////////////////////////////////////////////////////////////////////////////////// 读取数据库
        HighPrecisionTimer loadDbTimer = new(); // 从发起读取数据库到成功返回读取内容的总时间
        loadDbTimer.Start();
        lddb.LoadDatabase_V1("database1");
        // Console.WriteLine(lddb.GetLoadingDatabaseContent()); // 输出database1内容
        loadDbTimer.Stop();
        WriteTime("从发起读取数据库指令到成功返回读取内容的总时间为(V1): ", loadDbTimer.GetElapsedMilliseconds());

        HighPrecisionTimer loadDbV2Timer = new(); // 从发起读取数据库到成功返回读取内容的总时间
        loadDbV2Timer.Start();
        lddb.LoadDatabase_V2("database1");
        // Console.WriteLine(lddb.GetLoadingDatabaseContent()); // 输出database1内容
        loadDbV2Timer.Stop();
        WriteTime("从发起读取数据库指令到成功返回读取内容的总时间为(V2): ", loadDbV2Timer.GetElapsedMilliseconds());
        #endregion
    }

    private void GetAllDatabaseName()
    {
        #region 获取所有数据库名称
        ///////////////////////////////////////////////////////////////////////////////////////////////////////// 获取所有数据库名称
        HighPrecisionTimer readAllDbNameTimer = new(); // 从发起读取数据库名称到成功返回读取内容的总时间
        readAllDbNameTimer.Start();
        lddb.ReadAllDatabaseName();
        readAllDbNameTimer.Stop();
        if (lddb.AllDatabaseName != null)
        {
            /*
            foreach (var dbName in lddb.AllDatabaseName)
            {
                Console.WriteLine(dbName);
            }
            */
        }
        WriteTime("从发起读取数据库名称到成功返回读取内容的总时间为: ", readAllDbNameTimer.GetElapsedMilliseconds());
        #endregion
    }

    void ParseDatabaseToTemp()
    {
        #region 数据库解析缓存
        HighPrecisionTimer parseDbTimer = new();
        parseDbTimer.Start();
#pragma warning disable CS0612 // 类型或成员已过时
        lddb.Parse_V1();
#pragma warning restore CS0612 // 类型或成员已过时
        parseDbTimer.Stop();
        WriteTime("解析文件并写入缓存V1(同步): ", parseDbTimer.GetElapsedMilliseconds());
        HighPrecisionTimer parseDbTimerAsync = new();
        parseDbTimerAsync.Start();
#pragma warning disable CS0612 // 类型或成员已过时
        lddb.ParseAsync_V1();
#pragma warning restore CS0612 // 类型或成员已过时
        parseDbTimerAsync.Stop();
        WriteTime("解析文件并写入缓存V1(异步): ", parseDbTimerAsync.GetElapsedMilliseconds());
        #endregion
    }

    private void CdAllTypeSearch()
    {
        #region 并发词典数据库全类型搜寻
        HighPrecisionTimer allTypeSearchFromConcurrentDictionaryTimer = new();
        allTypeSearchFromConcurrentDictionaryTimer.Start();
        string[] AllTypeSearchFromConcurrentDictionaryResult = lddb.AllTypeSearchFromConcurrentDictionary("str_name");
        allTypeSearchFromConcurrentDictionaryTimer.Stop();
        // 使用 foreach 输出数组的每个元素
        foreach (var str in AllTypeSearchFromConcurrentDictionaryResult)
        {
            // Console.WriteLine(str);
        }
        WriteTime("并发词典数据库全类型同步搜寻: ", allTypeSearchFromConcurrentDictionaryTimer.GetElapsedMilliseconds());
        #endregion
    }

    private void StlAllTypeSearch()
    {
        #region 信号量线程锁词典数据库全类型搜寻
        HighPrecisionTimer allTypeSearchFromSemaphoreThreadLockTimer = new();
        allTypeSearchFromSemaphoreThreadLockTimer.Start();
        string[] AllTypeSearchFromSemaphoreThreadLockResult = lddb.AllTypeSearchFromSemaphoreThreadLock("str_name");
        allTypeSearchFromSemaphoreThreadLockTimer.Stop();
        // 使用 foreach 输出数组的每个元素
        foreach (var str in AllTypeSearchFromSemaphoreThreadLockResult)
        {
            // Console.WriteLine(str);
        }
        WriteTime("信号量线程锁词典数据库全类型搜寻: ", allTypeSearchFromSemaphoreThreadLockTimer.GetElapsedMilliseconds());
        #endregion
    }

    private void TripleDictionaryTest()
    {
        HighPrecisionTimer parse = new();
        parse.Start();
        lddb.TripleDictParse();
        parse.Stop();
        WriteTime("三值字典解析并写入(同步): ", parse.GetElapsedMilliseconds());
    }
    #endregion

    #region 工具
    private void WriteTime(string what, decimal time)
    {
        string data = what + time + "ms\n";
        Console.WriteLine(data);
        testData.Add(data);
        Console.WriteLine();
    }

    private void ExportTestData()
    {
        // 获取当前日期和时间，格式化为 "yyyy-MM-dd HH-mm"
        string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt";

        // 指定文件路径
        string directoryPath = "./testdata/";
        string filePath = Path.Combine(directoryPath, fileName);

        // 确保目录存在，如果不存在则创建
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // 将 List<string> 中的每一行写入文件
        File.WriteAllLines(filePath, testData);

        Console.WriteLine($"数据已成功写入文件: {filePath}");
    }
    #endregion
}
