using TimeRecord;
using TYLDDB;

string dbFilePath = "./example.lddb";
List<string> testData = [];

///////////////////////////////////////////////////////////////////////////////////////////////////////// 实例化
var lddb = new LDDB();

///////////////////////////////////////////////////////////////////////////////////////////////////////// 读取文件
HighPrecisionTimer readDbTimer = new(); // 从发起读取文件到成功读取的总时间
lddb.FilePath = dbFilePath;
readDbTimer.Start();
lddb.ReadingFile();
readDbTimer.Stop();
WriteTime("从发起读取文件指令到成功读取的总时间为: ", readDbTimer.ElapsedMilliseconds());

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

///////////////////////////////////////////////////////////////////////////////////////////////////////// 获取所有数据库名称
HighPrecisionTimer readAllDbNameTimer = new(); // 从发起读取数据库名称到成功返回读取内容的总时间
readAllDbNameTimer.Start();
lddb.ReadAllDatabaseName();
readAllDbNameTimer.Stop();
if(lddb.AllDatabaseName != null)
{
    foreach (var dbName in lddb.AllDatabaseName)
    {
        Console.WriteLine(dbName);
    }
}
WriteTime("从发起读取数据库名称到成功返回读取内容的总时间为: ", readAllDbNameTimer.ElapsedMilliseconds());

///////////////////////////////////////////////////////////////////////////////////////////////////////// 数据库解析缓存
HighPrecisionTimer parseDbTimer = new(); // 从发起解析文件到成功解析并写入缓存的总时间(同步)
parseDbTimer.Start();
await lddb.Parse();
parseDbTimer.Stop();
WriteTime("从发起解析文件到成功解析并写入缓存的总时间(同步): ", parseDbTimer.ElapsedMilliseconds());
HighPrecisionTimer parseDbTimerAsync = new(); // 从发起解析文件到成功解析并写入缓存的总时间(异步)
parseDbTimerAsync.Start();
await lddb.ParseAsync();
parseDbTimerAsync.Stop();
WriteTime("从发起解析文件到成功解析并写入缓存的总时间(异步): ", parseDbTimerAsync.ElapsedMilliseconds());

///////////////////////////////////////////////////////////////////////////////////////////////////////// 并发词典数据库全类型同步搜寻
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

///////////////////////////////////////////////////////////////////////////////////////////////////////// 信号量线程锁词典数据库全类型同步搜寻
HighPrecisionTimer allTypeSearchFromSemaphoreThreadLockTimer = new();
allTypeSearchFromSemaphoreThreadLockTimer.Start();
string[] AllTypeSearchFromSemaphoreThreadLockResult = lddb.AllTypeSearchFromSemaphoreThreadLock("str_name");
allTypeSearchFromSemaphoreThreadLockTimer.Stop();
// 使用 foreach 输出数组的每个元素
foreach (var str in AllTypeSearchFromSemaphoreThreadLockResult)
{
    Console.WriteLine(str);
}
WriteTime("信号量线程锁词典数据库全类型同步搜寻: ", allTypeSearchFromSemaphoreThreadLockTimer.ElapsedMilliseconds());























ExportTestData();
Console.ReadLine();

////////////////////////////////////////////////////////////////////////////////////////////////////////// Test Method

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
