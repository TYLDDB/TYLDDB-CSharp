using TimeRecord;
using TYLDDB.Utils;

string largeString = new('A', 1);
string syncFilePath = "sync.txt";
string asyncFilePath = "async.txt";
string syncChunkFilePath = "sync-chunk.txt";
string asyncChunkFilePath = "async-chunk.txt";

var syncTime = new HighPrecisionTimer();
var asyncTime = new HighPrecisionTimer();
var syncChunkTime = new HighPrecisionTimer();
var asyncChunkTime = new HighPrecisionTimer();

syncTime.Start();
Writer.WriteStringToFile(syncFilePath, largeString);
syncTime.Stop();

asyncTime.Start();
await Writer.WriteStringToFileAsync(asyncFilePath, largeString);
asyncTime.Stop();

syncChunkTime.Start();
Writer.WriteStringToFileInChunks(syncChunkFilePath, largeString);
syncChunkTime.Stop();

asyncChunkTime.Start();
await Writer.WriteStringToFileInChunksAsync(asyncChunkFilePath, largeString);
asyncChunkTime.Stop();

Console.WriteLine("测试结果：");
Console.WriteLine($"同步写入：{syncTime.ElapsedMilliseconds()}ms");
Console.WriteLine($"异步写入：{asyncTime.ElapsedMilliseconds()}ms");
Console.WriteLine($"同步分块写入：{syncChunkTime.ElapsedMilliseconds()}ms");
Console.WriteLine($"异步分块写入：{asyncChunkTime.ElapsedMilliseconds()}ms");

Console.ReadLine();
