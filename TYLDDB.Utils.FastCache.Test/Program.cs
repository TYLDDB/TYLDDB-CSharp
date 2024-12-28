using TimeRecord;
using TYLDDB.Utils.FastCache;

Console.WriteLine("基于信号量的缓存读写运行");
SemaphoreSlimDefault();

Console.WriteLine();
Console.WriteLine("基于信号量的缓存读写运行");
ConcurrentDictionary();
Console.ReadLine();

// 基于信号量的缓存读写运行
static async void SemaphoreSlimDefault()
{
    var setTime = new HighPrecisionTimer();
    var setAsyncTime = new HighPrecisionTimer();
    var getByKeyTime = new HighPrecisionTimer();
    var getByKeyAsyncTime = new HighPrecisionTimer();
    var getKeysByValueTime = new HighPrecisionTimer();
    var getKeysByValueAsyncTime = new HighPrecisionTimer();
    var getAllCacheTime = new HighPrecisionTimer();
    var getAllCacheAsyncTime = new HighPrecisionTimer();

    var cache = new SemaphoreThreadLock();

    setTime.Start();
    cache.Set("TESTKEY1", "TESTVALUE1");
    setTime.Stop();

    setAsyncTime.Start();
    await cache.SetAsync("TESTKEY2", "TESTVALUE2");
    setAsyncTime.Stop();

    getByKeyTime.Start();
    Console.WriteLine("TESTKEY1对应的值：" + cache.GetByKey("TESTKEY1"));
    getByKeyTime.Stop();

    getByKeyAsyncTime.Start();
    Console.WriteLine("TESTKEY2对应的值：" + await cache.GetByKeyAsync("TESTKEY2"));
    getByKeyAsyncTime.Stop();

    cache.Set("TESTKEY3", "TESTVALUE2");

    Console.WriteLine("TESTVALUE2对应的所有键：");
    getKeysByValueTime.Start();
    Console.WriteLine(string.Join(", ", cache.GetKeysByValue("TESTVALUE2")));
    getKeysByValueTime.Stop();

    Console.WriteLine("TESTVALUE2对应的所有键：");
    getKeysByValueAsyncTime.Start();
    Console.WriteLine(string.Join(", ", await cache.GetKeysByValueAsync("TESTVALUE2")));
    getKeysByValueAsyncTime.Stop();

    // 获取并输出所有缓存项（同步方法）
    getAllCacheTime.Start();
    var allCacheSync = cache.GetAllCache();
    Console.WriteLine("同步获取所有缓存项:");
    foreach (var kvp in allCacheSync)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
    getAllCacheTime.Stop();

    // 获取并输出所有缓存项（异步方法）
    getAllCacheAsyncTime.Start();
    var allCacheAsync = await cache.GetAllCacheAsync();
    Console.WriteLine("异步获取所有缓存项:");
    foreach (var kvp in allCacheAsync)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
    getAllCacheAsyncTime.Stop();


    Console.WriteLine("时间消耗：");
    Console.WriteLine($"设置键值(同步)：{setTime.ElapsedMilliseconds()}ms 设置键值(异步)：{setAsyncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"根据键获取值(同步)：{getByKeyTime.ElapsedMilliseconds()}ms 根据键获取值(异步)：{getByKeyAsyncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"根据值获取键(同步)：{getKeysByValueTime.ElapsedMilliseconds()}ms 根据值获取键(异步)：{getKeysByValueAsyncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"获取所有缓存项(同步)：{getAllCacheTime.ElapsedMilliseconds()}ms 获取所有缓存项(异步)：{getAllCacheAsyncTime.ElapsedMilliseconds()}ms");
}

// 基于并发词典的缓存读写运行
static async void ConcurrentDictionary()
{
    var setTime = new HighPrecisionTimer();
    var setAsyncTime = new HighPrecisionTimer();
    var getByKeyTime = new HighPrecisionTimer();
    var getByKeyAsyncTime = new HighPrecisionTimer();
    var getKeysByValueTime = new HighPrecisionTimer();
    var getKeysByValueAsyncTime = new HighPrecisionTimer();
    var getAllCacheTime = new HighPrecisionTimer();
    var getAllCacheAsyncTime = new HighPrecisionTimer();

    var cache = new ConcurrentDictionary();

    setTime.Start();
    cache.Set("TESTKEY1", "TESTVALUE1");
    setTime.Stop();

    setAsyncTime.Start();
    await cache.SetAsync("TESTKEY2", "TESTVALUE2");
    setAsyncTime.Stop();

    getByKeyTime.Start();
    Console.WriteLine("TESTKEY1对应的值：" + cache.GetByKey("TESTKEY1"));
    getByKeyTime.Stop();

    getByKeyAsyncTime.Start();
    Console.WriteLine("TESTKEY2对应的值：" + await cache.GetByKeyAsync("TESTKEY2"));
    getByKeyAsyncTime.Stop();

    cache.Set("TESTKEY3", "TESTVALUE2");

    Console.WriteLine("TESTVALUE2对应的所有键：");
    getKeysByValueTime.Start();
    Console.WriteLine(string.Join(", ", cache.GetKeysByValue("TESTVALUE2")));
    getKeysByValueTime.Stop();

    Console.WriteLine("TESTVALUE2对应的所有键：");
    getKeysByValueAsyncTime.Start();
    Console.WriteLine(string.Join(", ", await cache.GetKeysByValueAsync("TESTVALUE2")));
    getKeysByValueAsyncTime.Stop();

    // 获取并输出所有缓存项（同步方法）
    getAllCacheTime.Start();
    var allCacheSync = cache.GetAllCache();
    Console.WriteLine("同步获取所有缓存项:");
    foreach (var kvp in allCacheSync)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
    getAllCacheTime.Stop();

    // 获取并输出所有缓存项（异步方法）
    getAllCacheAsyncTime.Start();
    var allCacheAsync = await cache.GetAllCacheAsync();
    Console.WriteLine("异步获取所有缓存项:");
    foreach (var kvp in allCacheAsync)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
    getAllCacheAsyncTime.Stop();

    Console.WriteLine("时间消耗：");
    Console.WriteLine($"设置键值(同步)：{setTime.ElapsedMilliseconds()}ms 设置键值(异步)：{setAsyncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"根据键获取值(同步)：{getByKeyTime.ElapsedMilliseconds()}ms 根据键获取值(异步)：{getByKeyAsyncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"根据值获取键(同步)：{getKeysByValueTime.ElapsedMilliseconds()}ms 根据值获取键(异步)：{getKeysByValueAsyncTime.ElapsedMilliseconds()}ms");
    Console.WriteLine($"获取所有缓存项(同步)：{getAllCacheTime.ElapsedMilliseconds()}ms 获取所有缓存项(异步)：{getAllCacheAsyncTime.ElapsedMilliseconds()}ms");
}
