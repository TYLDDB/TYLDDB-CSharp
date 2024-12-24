using TYLDDB;
using TYLDDB.Test;
using TYLDDB.Utils;

string dbFilePath = "./example.lddb";

ReadFileMethodTest();

///////////////////////////////////////////////////////////////////////////////////////////////////////// 实例化
LDDB lddb = new LDDB();

///////////////////////////////////////////////////////////////////////////////////////////////////////// 读取文件
HighPrecisionTimer readDbTimer = new(); // 从发起读取文件到成功读取的总时间
readDbTimer.Start();
lddb.FilePath = dbFilePath;
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















Console.ReadLine();

////////////////////////////////////////////////////////////////////////////////////////////////////////// Test Method

void ReadFileMethodTest()
{
    HighPrecisionTimer readSmallFileFuncTime = new();
    HighPrecisionTimer readMediumFileFuncTime = new();
    HighPrecisionTimer readLarge8FileFuncTime = new();
    HighPrecisionTimer readLarge64FileFuncTime = new();
    HighPrecisionTimer readLarge128FileFuncTime = new();
    HighPrecisionTimer readVeryLargeFileFuncTime = new();
    readSmallFileFuncTime.Start();
    ReadFile.ReadSmallFile(dbFilePath);
    readSmallFileFuncTime.Stop();
    readMediumFileFuncTime.Start();
    ReadFile.ReadMediumFile(dbFilePath);
    readMediumFileFuncTime.Stop();
    readLarge8FileFuncTime.Start();
    ReadFile.ReadLargeFile_8(dbFilePath);
    readLarge8FileFuncTime.Stop();
    readLarge64FileFuncTime.Start();
    ReadFile.ReadLargeFile_64(dbFilePath);
    readLarge64FileFuncTime.Stop();
    readLarge128FileFuncTime.Start();
    ReadFile.ReadLargeFile_128(dbFilePath);
    readLarge128FileFuncTime.Stop();
    readVeryLargeFileFuncTime.Start();
    ReadFile.ReadVeryLargeFile(dbFilePath);
    readVeryLargeFileFuncTime.Stop();
    Console.WriteLine("不同读取文件的方法测试：");
    Console.WriteLine($"小文件读取：{readSmallFileFuncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"中文件读取：{readMediumFileFuncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"大文件读取8kb：{readLarge8FileFuncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"大文件读取64kb：{readLarge64FileFuncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"大文件读取256kb：{readLarge128FileFuncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"特大文件读取：{readVeryLargeFileFuncTime.ElapsedMilliseconds()}ms");

    // 获取文件信息
    FileInfo fileInfo = new FileInfo(dbFilePath);
    // 获取文件大小 (单位字节)
    long fileSize = fileInfo.Length;

    Console.WriteLine("使用的测试文件大小为：" + fileSize / 1024);
}

///////////////////////////////////////////////////////////////////////////////////////////////////////// 工具
static void WriteTime(string what, double time)
{
    Console.WriteLine(what + time + "ms\n");
    Console.WriteLine();
}
