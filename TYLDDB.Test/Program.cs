using TYLDDB;
using TYLDDB.Test;

///////////////////////////////////////////////////////////////////////////////////////////////////////// 实例化
LDDB lddb = new LDDB();

///////////////////////////////////////////////////////////////////////////////////////////////////////// 读取文件
HighPrecisionTimer readDbTimer = new(); // 从发起读取文件指令到成功读取的总时间
readDbTimer.Start();
lddb.FilePath = "./example.lddb";
lddb.ReadingFile();
readDbTimer.Stop();
WriteTime("从发起读取文件指令到成功读取的总时间为: ", readDbTimer.ElapsedMilliseconds());

///////////////////////////////////////////////////////////////////////////////////////////////////////// 读取数据库
HighPrecisionTimer loadDbTimer = new(); // 从发起读取数据库指令到成功返回读取内容的总时间
loadDbTimer.Start();
lddb.LoadDatabase("database1");
Console.WriteLine(lddb.GetLoadingDatabaseContent()); // 输出database1内容
loadDbTimer.Stop();
WriteTime("从发起读取数据库指令到成功返回读取内容的总时间为: ", loadDbTimer.ElapsedMilliseconds());

///////////////////////////////////////////////////////////////////////////////////////////////////////// 获取所有数据库名称
HighPrecisionTimer readAllDbNameTimer = new(); // 从发起读取数据库指令到成功返回读取内容的总时间
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
WriteTime("从发起读取数据库指令到成功返回读取内容的总时间为: ", readAllDbNameTimer.ElapsedMilliseconds());















Console.ReadLine();





///////////////////////////////////////////////////////////////////////////////////////////////////////// 工具
static void WriteTime(string what, double time)
{
    Console.WriteLine(what + time + "ms\n");
    Console.WriteLine();
}
