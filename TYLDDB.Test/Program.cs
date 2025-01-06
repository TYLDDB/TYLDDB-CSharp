using TimeRecord;
using TYLDDB;

string dbFilePath = "./example.lddb";

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
WriteTime("从发起读取数据库指令到成功返回读取内容的总时间为: ", loadDbTimer.ElapsedMilliseconds());

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
HighPrecisionTimer parseDbTimer = new(); // 从发起解析文件到成功解析并写入缓存的总时间
parseDbTimer.Start();
await lddb.ParseAsync();
parseDbTimer.Stop();
WriteTime("从发起解析文件到成功解析并写入缓存的总时间: ", parseDbTimer.ElapsedMilliseconds());

///////////////////////////////////////////////////////////////////////////////////////////////////////// 并发词典数据库全类型异步搜寻
HighPrecisionTimer allTypeSearchTimer = new();
allTypeSearchTimer.Start();
string[] AllTypeSearchResult = lddb.AllTypeSearchFromConcurrentDictionary("str_name");
allTypeSearchTimer.Stop();
// 使用 foreach 输出数组的每个元素
foreach (var str in AllTypeSearchResult)
{
    Console.WriteLine(str);
}
WriteTime("并发词典数据库全类型异步搜寻: ", parseDbTimer.ElapsedMilliseconds());







Console.ReadLine();

////////////////////////////////////////////////////////////////////////////////////////////////////////// Test Method

///////////////////////////////////////////////////////////////////////////////////////////////////////// 工具
static void WriteTime(string what, double time)
{
    Console.WriteLine(what + time + "ms\n");
    Console.WriteLine();
}
