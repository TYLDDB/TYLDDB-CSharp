using System.Threading.Tasks;
using TYLDDB.Parser;

namespace TYLDDB
{
    public partial class LDDB
    {
        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        public async Task Parse_V1()
        {
            // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行

            // ConcurrentDictionary
            Task cdStringCacheTask = Task.Factory.StartNew(() => CdString(), TaskCreationOptions.LongRunning);
            Task cdIntCacheTask = Task.Factory.StartNew(() => CdInt(), TaskCreationOptions.LongRunning);
            Task cdShortCacheTask = Task.Factory.StartNew(() => CdShort(), TaskCreationOptions.LongRunning);
            Task cdLongCacheTask = Task.Factory.StartNew(() => CdLong(), TaskCreationOptions.LongRunning);
            Task cdFloatCacheTask = Task.Factory.StartNew(() => CdFloat(), TaskCreationOptions.LongRunning);
            Task cdDoubleCacheTask = Task.Factory.StartNew(() => CdDouble(), TaskCreationOptions.LongRunning);
            Task cdDecimalCacheTask = Task.Factory.StartNew(() => CdDecimal(), TaskCreationOptions.LongRunning);
            Task cdCharCacheTask = Task.Factory.StartNew(() => CdChar(), TaskCreationOptions.LongRunning);
            Task cdBoolCacheTask = Task.Factory.StartNew(() => CdBool(), TaskCreationOptions.LongRunning);

            // SemaphoreThreadLock
            Task stlStringCacheTask = Task.Factory.StartNew(() => StlString(), TaskCreationOptions.LongRunning);
            Task stlIntCacheTask = Task.Factory.StartNew(() => StlInt(), TaskCreationOptions.LongRunning);
            Task stlShortCacheTask = Task.Factory.StartNew(() => StlShort(), TaskCreationOptions.LongRunning);
            Task stlLongCacheTask = Task.Factory.StartNew(() => StlLong(), TaskCreationOptions.LongRunning);
            Task stlFloatCacheTask = Task.Factory.StartNew(() => StlFloat(), TaskCreationOptions.LongRunning);
            Task stlDoubleCacheTask = Task.Factory.StartNew(() => StlDouble(), TaskCreationOptions.LongRunning);
            Task stlDecimalCacheTask = Task.Factory.StartNew(() => StlDecimal(), TaskCreationOptions.LongRunning);
            Task stlCharCacheTask = Task.Factory.StartNew(() => StlChar(), TaskCreationOptions.LongRunning);
            Task stlBoolCacheTask = Task.Factory.StartNew(() => StlBool(), TaskCreationOptions.LongRunning);

            // 等待所有任务完成
            await Task.WhenAll(cdStringCacheTask,
                               cdIntCacheTask,
                               cdShortCacheTask,
                               cdLongCacheTask,
                               cdFloatCacheTask,
                               cdDoubleCacheTask,
                               cdDecimalCacheTask,
                               cdCharCacheTask,
                               cdBoolCacheTask,
                               stlStringCacheTask,
                               stlIntCacheTask,
                               stlShortCacheTask,
                               stlLongCacheTask,
                               stlFloatCacheTask,
                               stlDoubleCacheTask,
                               stlDecimalCacheTask,
                               stlCharCacheTask,
                               stlBoolCacheTask);

            // ConcurrentDictionary
            void CdString()
            {
                var dict = DataParser_V1.ParseString(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdStringDictionary.Set(key, value);
                }
            }
            void CdShort()
            {
                var dict = DataParser_V1.ParseShort(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdShortDictionary.Set(key, value);
                }
            }
            void CdLong()
            {
                var dict = DataParser_V1.ParseLong(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdLongDictionary.Set(key, value);
                }
            }
            void CdInt()
            {
                var dict = DataParser_V1.ParseInt(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdIntegerDictionary.Set(key, value);
                }
            }
            void CdFloat()
            {
                var dict = DataParser_V1.ParseFloat(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdFloatDictionary.Set(key, value);
                }
            }
            void CdDouble()
            {
                var dict = DataParser_V1.ParseDouble(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdDoubleDictionary.Set(key, value);
                }
            }
            void CdDecimal()
            {
                var dict = DataParser_V1.ParseDecimal(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdDecimalDictionary.Set(key, value);
                }
            }
            void CdChar()
            {
                var dict = DataParser_V1.ParseChar(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdCharDictionary.Set(key, value);
                }
            }
            void CdBool()
            {
                var dict = DataParser_V1.ParseBoolean(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdBooleanDictionary.Set(key, value);
                }
            }

            // SemaphoreThreadLock
            void StlString()
            {
                var dict = DataParser_V1.ParseString(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlStringDictionary.Set(key, value);
                }
            }
            void StlShort()
            {
                var dict = DataParser_V1.ParseShort(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlShortDictionary.Set(key, value);
                }
            }
            void StlLong()
            {
                var dict = DataParser_V1.ParseLong(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlLongDictionary.Set(key, value);
                }
            }
            void StlInt()
            {
                var dict = DataParser_V1.ParseInt(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlIntegerDictionary.Set(key, value);
                }
            }
            void StlFloat()
            {
                var dict = DataParser_V1.ParseFloat(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlFloatDictionary.Set(key, value);
                }
            }
            void StlDouble()
            {
                var dict = DataParser_V1.ParseDouble(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlDoubleDictionary.Set(key, value);
                }
            }
            void StlDecimal()
            {
                var dict = DataParser_V1.ParseDecimal(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlDecimalDictionary.Set(key, value);
                }
            }
            void StlChar()
            {
                var dict = DataParser_V1.ParseChar(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlCharDictionary.Set(key, value);
                }
            }
            void StlBool()
            {
                var dict = DataParser_V1.ParseBoolean(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlBooleanDictionary.Set(key, value);
                }
            }
        }

        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        public async Task ParseAsync_V1()
        {
            // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行

            // ConcurrentDictionary
            Task cdStringCacheTask = Task.Factory.StartNew(() => CdString(), TaskCreationOptions.LongRunning);
            Task cdIntCacheTask = Task.Factory.StartNew(() => CdInt(), TaskCreationOptions.LongRunning);
            Task cdShortCacheTask = Task.Factory.StartNew(() => CdShort(), TaskCreationOptions.LongRunning);
            Task cdLongCacheTask = Task.Factory.StartNew(() => CdLong(), TaskCreationOptions.LongRunning);
            Task cdFloatCacheTask = Task.Factory.StartNew(() => CdFloat(), TaskCreationOptions.LongRunning);
            Task cdDoubleCacheTask = Task.Factory.StartNew(() => CdDouble(), TaskCreationOptions.LongRunning);
            Task cdDecimalCacheTask = Task.Factory.StartNew(() => CdDecimal(), TaskCreationOptions.LongRunning);
            Task cdCharCacheTask = Task.Factory.StartNew(() => CdChar(), TaskCreationOptions.LongRunning);
            Task cdBoolCacheTask = Task.Factory.StartNew(() => CdBool(), TaskCreationOptions.LongRunning);

            // SemaphoreThreadLock
            Task stlStringCacheTask = Task.Factory.StartNew(() => StlString(), TaskCreationOptions.LongRunning);
            Task stlIntCacheTask = Task.Factory.StartNew(() => StlInt(), TaskCreationOptions.LongRunning);
            Task stlShortCacheTask = Task.Factory.StartNew(() => StlShort(), TaskCreationOptions.LongRunning);
            Task stlLongCacheTask = Task.Factory.StartNew(() => StlLong(), TaskCreationOptions.LongRunning);
            Task stlFloatCacheTask = Task.Factory.StartNew(() => StlFloat(), TaskCreationOptions.LongRunning);
            Task stlDoubleCacheTask = Task.Factory.StartNew(() => StlDouble(), TaskCreationOptions.LongRunning);
            Task stlDecimalCacheTask = Task.Factory.StartNew(() => StlDecimal(), TaskCreationOptions.LongRunning);
            Task stlCharCacheTask = Task.Factory.StartNew(() => StlChar(), TaskCreationOptions.LongRunning);
            Task stlBoolCacheTask = Task.Factory.StartNew(() => StlBool(), TaskCreationOptions.LongRunning);

            // 等待所有任务完成
            await Task.WhenAll(cdStringCacheTask,
                               cdIntCacheTask,
                               cdShortCacheTask,
                               cdLongCacheTask,
                               cdFloatCacheTask,
                               cdDoubleCacheTask,
                               cdDecimalCacheTask,
                               cdCharCacheTask,
                               cdBoolCacheTask,
                               stlStringCacheTask,
                               stlIntCacheTask,
                               stlShortCacheTask,
                               stlLongCacheTask,
                               stlFloatCacheTask,
                               stlDoubleCacheTask,
                               stlDecimalCacheTask,
                               stlCharCacheTask,
                               stlBoolCacheTask);

            // ConcurrentDictionary
            async void CdString()
            {
                var dict = DataParser_V1.ParseString(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdStringDictionary.SetAsync(key, value);
                }
            }
            async void CdShort()
            {
                var dict = DataParser_V1.ParseShort(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdShortDictionary.SetAsync(key, value);
                }
            }
            async void CdLong()
            {
                var dict = DataParser_V1.ParseLong(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdLongDictionary.SetAsync(key, value);
                }
            }
            async void CdInt()
            {
                var dict = DataParser_V1.ParseInt(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdIntegerDictionary.SetAsync(key, value);
                }
            }
            async void CdFloat()
            {
                var dict = DataParser_V1.ParseFloat(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdFloatDictionary.SetAsync(key, value);
                }
            }
            async void CdDouble()
            {
                var dict = DataParser_V1.ParseDouble(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdDoubleDictionary.SetAsync(key, value);
                }
            }
            async void CdDecimal()
            {
                var dict = DataParser_V1.ParseDecimal(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdDecimalDictionary.SetAsync(key, value);
                }
            }
            async void CdChar()
            {
                var dict = DataParser_V1.ParseChar(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdCharDictionary.SetAsync(key, value);
                }
            }
            async void CdBool()
            {
                var dict = DataParser_V1.ParseBoolean(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdBooleanDictionary.SetAsync(key, value);
                }
            }

            // SemaphoreThreadLock
            async void StlString()
            {
                var dict = DataParser_V1.ParseString(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlStringDictionary.SetAsync(key, value);
                }
            }
            async void StlShort()
            {
                var dict = DataParser_V1.ParseShort(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlShortDictionary.SetAsync(key, value);
                }
            }
            async void StlLong()
            {
                var dict = DataParser_V1.ParseLong(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlLongDictionary.SetAsync(key, value);
                }
            }
            async void StlInt()
            {
                var dict = DataParser_V1.ParseInt(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlIntegerDictionary.SetAsync(key, value);
                }
            }
            async void StlFloat()
            {
                var dict = DataParser_V1.ParseFloat(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlFloatDictionary.SetAsync(key, value);
                }
            }
            async void StlDouble()
            {
                var dict = DataParser_V1.ParseDouble(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlDoubleDictionary.SetAsync(key, value);
                }
            }
            async void StlDecimal()
            {
                var dict = DataParser_V1.ParseDecimal(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlDecimalDictionary.SetAsync(key, value);
                }
            }
            async void StlChar()
            {
                var dict = DataParser_V1.ParseChar(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlCharDictionary.SetAsync(key, value);
                }
            }
            async void StlBool()
            {
                var dict = DataParser_V1.ParseBoolean(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlBooleanDictionary.SetAsync(key, value);
                }
            }
        }

        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        public void Parse_V2()
        {
            var _str = DataParser_V2.ParseString(_databaseContent);
            var _int = DataParser_V2.ParseInt(_databaseContent);
            var _short = DataParser_V2.ParseShort(_databaseContent);
            var _long = DataParser_V2.ParseLong(_databaseContent);
            var _float = DataParser_V2.ParseFloat(_databaseContent);
            var _double = DataParser_V2.ParseDouble(_databaseContent);
            var _bool = DataParser_V2.ParseBoolean(_databaseContent);
            var _char = DataParser_V2.ParseChar(_databaseContent);
            var _decimal = DataParser_V2.ParseDecimal(_databaseContent);

            foreach (var kvp in _str)
            {
                var key = kvp.Key;   // 获取第一个值 (key)
                var value = kvp.Value;  // 获取第二个值 (value)

                // 将 key-value 对存储到缓存
                cdStringDictionary.Set(key, value);
                stlStringDictionary.Set(key, value);
            }

            foreach (var kvp in _int)
            {
                var key = kvp.Key;   // 获取第一个值 (key)
                var value = kvp.Value;  // 获取第二个值 (value)

                // 将 key-value 对存储到缓存
                cdIntegerDictionary.Set(key, value);
                stlIntegerDictionary.Set(key, value);
            }

            foreach (var kvp in _short)
            {
                var key = kvp.Key;   // 获取第一个值 (key)
                var value = kvp.Value;  // 获取第二个值 (value)

                // 将 key-value 对存储到缓存
                cdShortDictionary.Set(key, value);
                stlShortDictionary.Set(key, value);
            }

            foreach (var kvp in _long)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                cdLongDictionary.Set(key, value);
                stlLongDictionary.Set(key, value);
            }

            foreach(var kvp in _float)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                cdFloatDictionary.Set(key, value);
                stlFloatDictionary.Set(key, value);
            }

            foreach(var kvp in _double)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                cdDoubleDictionary.Set(key, value);
                stlDoubleDictionary.Set(key, value);
            }

            foreach(var kvp in _bool)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                cdBooleanDictionary.Set(key, value);
                stlBooleanDictionary.Set(key, value);
            }

            foreach(var kvp in _char)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                cdCharDictionary.Set(key, value);
                stlCharDictionary.Set(key, value);
            }

            foreach(var kvp in _decimal)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                cdDecimalDictionary.Set(key, value);
                stlDecimalDictionary.Set(key, value);
            }
        }

        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        public void Parse_V2Integrated()
        {

        }

        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        public void Parse_V2Nonintegrated()
        {

        }
    }
}
