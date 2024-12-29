using TimeRecord;
using TYLDDB.Parser;

string content = @"
string::""str_name""=""name1"";
int::""int_value""=""123"";
short::""short_value""=""32767"";
long::""long_value""=""2147483647"";
float::""float_value""=""3.14f"";
double::""double_value""=""3.141592653589793"";
boolean::""bool""=""true"";
char::""char_value""=""127"";
decimal::decima_value = 19.99m"";
internaldb::""db_name""={};
";

var parser = new Parser();
var time = new HighPrecisionTimer();
time.Start();
// 调用 Parse 方法进行解析
var result = Parser.ParseString(content);
time.Stop();
// 输出找到的所有键值对
if (result.Count > 0)
{
    foreach (var entry in result)
    {
        Console.WriteLine($"{entry.Key}: {entry.Value}");
    }
}

Console.WriteLine($"Time: {time.ElapsedMilliseconds()}ms");
Console.ReadLine();