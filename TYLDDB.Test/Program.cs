using TimeRecord;
using TYLDDB;

string dbFilePath = "./example.lddb";
List<string> testData = [];

#region 实例化
///////////////////////////////////////////////////////////////////////////////////////////////////////// 实例化
var lddb = new LDDB();
#endregion

ReadFile();

ReadDatabase();

GetAllDatabaseName();

ParseDatabaseToTemp();

CdAllTypeSearch();

StlAllTypeSearch();

TripleDictionaryTest();


ExportTestData();
Console.ReadLine();

#region Test Method
void ReadFile()
{
    #region 读取文件
    ///////////////////////////////////////////////////////////////////////////////////////////////////////// 读取文件
    HighPrecisionTimer readDbTimer = new(); // 从发起读取文件到成功读取的总时间
    lddb.FilePath = dbFilePath;
    readDbTimer.Start();
    lddb.ReadingFile();
    readDbTimer.Stop();
    WriteTime("从发起读取文件指令到成功读取的总时间为: ", readDbTimer.ElapsedMilliseconds());
    #endregion
}

void ReadDatabase()
{
    #region 读取数据库
    ///////////////////////////////////////////////////////////////////////////////////////////////////////// 读取数据库
    HighPrecisionTimer loadDbTimer = new(); // 从发起读取数据库到成功返回读取内容的总时间
    loadDbTimer.Start();
    lddb.LoadDatabase("database1");
    Console.WriteLine(lddb.GetLoadingDatabaseContent()); // 输出database1内容
    loadDbTimer.Stop();
    WriteTime("从发起读取数据库指令到成功返回读取内容的总时间为(V1): ", loadDbTimer.ElapsedMilliseconds());

    HighPrecisionTimer loadDbV2Timer = new(); // 从发起读取数据库到成功返回读取内容的总时间
    loadDbV2Timer.Start();
    lddb.LoadDatabase_V2("database1");
    Console.WriteLine(lddb.GetLoadingDatabaseContent()); // 输出database1内容
    loadDbV2Timer.Stop();
    WriteTime("从发起读取数据库指令到成功返回读取内容的总时间为(V2): ", loadDbV2Timer.ElapsedMilliseconds());
    #endregion
}

void GetAllDatabaseName()
{
    #region 获取所有数据库名称
    ///////////////////////////////////////////////////////////////////////////////////////////////////////// 获取所有数据库名称
    HighPrecisionTimer readAllDbNameTimer = new(); // 从发起读取数据库名称到成功返回读取内容的总时间
    readAllDbNameTimer.Start();
    lddb.ReadAllDatabaseName();
    readAllDbNameTimer.Stop();
    if (lddb.AllDatabaseName != null)
    {
        foreach (var dbName in lddb.AllDatabaseName)
        {
            Console.WriteLine(dbName);
        }
    }
    WriteTime("从发起读取数据库名称到成功返回读取内容的总时间为: ", readAllDbNameTimer.ElapsedMilliseconds());
    #endregion
}

async void ParseDatabaseToTemp()
{
    #region 数据库解析缓存
    HighPrecisionTimer parseDbTimer = new();
    parseDbTimer.Start();
    lddb.Parse_V1();
    parseDbTimer.Stop();
    WriteTime("解析文件并写入缓存V1(同步): ", parseDbTimer.ElapsedMilliseconds());
    HighPrecisionTimer parseDbTimerAsync = new();
    parseDbTimerAsync.Start();
    await lddb.ParseAsync_V1();
    parseDbTimerAsync.Stop();
    WriteTime("解析文件并写入缓存V1(异步): ", parseDbTimerAsync.ElapsedMilliseconds());
    #endregion
}

void CdAllTypeSearch()
{
    #region 并发词典数据库全类型搜寻
    HighPrecisionTimer allTypeSearchFromConcurrentDictionaryTimer = new();
    allTypeSearchFromConcurrentDictionaryTimer.Start();
    string[] AllTypeSearchFromConcurrentDictionaryResult = lddb.AllTypeSearchFromConcurrentDictionary("str_name");
    allTypeSearchFromConcurrentDictionaryTimer.Stop();
    // 使用 foreach 输出数组的每个元素
    foreach (var str in AllTypeSearchFromConcurrentDictionaryResult)
    {
        Console.WriteLine(str);
    }
    WriteTime("并发词典数据库全类型同步搜寻: ", allTypeSearchFromConcurrentDictionaryTimer.ElapsedMilliseconds());
    #endregion
}

void StlAllTypeSearch()
{
    #region 信号量线程锁词典数据库全类型搜寻
    HighPrecisionTimer allTypeSearchFromSemaphoreThreadLockTimer = new();
    allTypeSearchFromSemaphoreThreadLockTimer.Start();
    string[] AllTypeSearchFromSemaphoreThreadLockResult = lddb.AllTypeSearchFromSemaphoreThreadLock("str_name");
    allTypeSearchFromSemaphoreThreadLockTimer.Stop();
    // 使用 foreach 输出数组的每个元素
    foreach (var str in AllTypeSearchFromSemaphoreThreadLockResult)
    {
        Console.WriteLine(str);
    }
    WriteTime("信号量线程锁词典数据库全类型搜寻: ", allTypeSearchFromSemaphoreThreadLockTimer.ElapsedMilliseconds());
    #endregion
}

void TripleDictionaryTest()
{
    HighPrecisionTimer parse = new();
    parse.Start();
    lddb.TripleDictParse();
    parse.Stop();
    WriteTime("三值字典解析并写入(同步): ", parse.ElapsedMilliseconds());
}
#endregion

///////////////////////////////////////////////////////////////////////////////////////////////////////// 工具
void WriteTime(string what, double time)
{
    string data = what + time + "ms\n";
    Console.WriteLine(data);
    AddTestData(data);
    Console.WriteLine();
}

void AddTestData(string data)
{
    testData.Add(data);
}

void ExportTestData()
{
    // 获取当前日期和时间，格式化为 "yyyy-MM-dd HH-mm"
    string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".txt";

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
