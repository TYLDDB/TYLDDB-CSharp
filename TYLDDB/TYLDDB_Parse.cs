using System.Threading.Tasks;
using TYLDDB.Parser;
using System.Threading;
using System;

namespace TYLDDB
{
    public partial class LDDB
    {
        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        [Obsolete]
        public void Parse_V1()
        {
            switch (_cacheMode)
            {
                case CacheMode.CDAndSTL:
                    {
                        #region method
                        void String()
                        {
                            var dict = DataParser_V1.ParseString(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlStringDictionary.Set(key, value);
                                cdStringDictionary.Set(key, value);
                            }
                        }
                        void Short()
                        {
                            var dict = DataParser_V1.ParseShort(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlShortDictionary.Set(key, value);
                                cdShortDictionary.Set(key, value);
                            }
                        }
                        void Long()
                        {
                            var dict = DataParser_V1.ParseLong(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlLongDictionary.Set(key, value);
                                cdLongDictionary.Set(key, value);
                            }
                        }
                        void Int()
                        {
                            var dict = DataParser_V1.ParseInt(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlIntegerDictionary.Set(key, value);
                                cdIntegerDictionary.Set(key, value);
                            }
                        }
                        void Float()
                        {
                            var dict = DataParser_V1.ParseFloat(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlFloatDictionary.Set(key, value);
                                cdFloatDictionary.Set(key, value);
                            }
                        }
                        void Double()
                        {
                            var dict = DataParser_V1.ParseDouble(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlDoubleDictionary.Set(key, value);
                                cdDoubleDictionary.Set(key, value);
                            }
                        }
                        void Decimal()
                        {
                            var dict = DataParser_V1.ParseDecimal(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlDecimalDictionary.Set(key, value);
                                cdDecimalDictionary.Set(key, value);
                            }
                        }
                        void Char()
                        {
                            var dict = DataParser_V1.ParseChar(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlCharDictionary.Set(key, value);
                                cdCharDictionary.Set(key, value);
                            }
                        }
                        void Bool()
                        {
                            var dict = DataParser_V1.ParseBoolean(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlBooleanDictionary.Set(key, value);
                                cdBooleanDictionary.Set(key, value);
                            }
                        }
                        #endregion

                        #region thread
                        Thread _str = new Thread(String);
                        Thread _short = new Thread(Short);
                        Thread _long = new Thread(Long);
                        Thread _float = new Thread(Float);
                        Thread _double = new Thread(Double);
                        Thread _decimal = new Thread(Decimal);
                        Thread _char = new Thread(Char);
                        Thread _int = new Thread(Int);
                        Thread _bool = new Thread(Bool);

                        _str.Start();
                        _short.Start();
                        _long.Start();
                        _float.Start();
                        _double.Start();
                        _decimal.Start();
                        _char.Start();
                        _int.Start();
                        _bool.Start();

                        _str.Join();
                        _short.Join();
                        _long.Join();
                        _float.Join();
                        _double.Join();
                        _decimal.Join();
                        _char.Join();
                        _int.Join();
                        _bool.Join();
                        break;
                        #endregion
                    }

                case CacheMode.SemaphoreThreadLock:
                    {
                        #region method
                        void String()
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
                        void Short()
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
                        void Long()
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
                        void Int()
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
                        void Float()
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
                        void Double()
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
                        void Decimal()
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
                        void Char()
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
                        void Bool()
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
                        #endregion

                        #region thread
                        Thread _str = new Thread(String);
                        Thread _short = new Thread(Short);
                        Thread _long = new Thread(Long);
                        Thread _float = new Thread(Float);
                        Thread _double = new Thread(Double);
                        Thread _decimal = new Thread(Decimal);
                        Thread _char = new Thread(Char);
                        Thread _int = new Thread(Int);
                        Thread _bool = new Thread(Bool);

                        _str.Start();
                        _short.Start();
                        _long.Start();
                        _float.Start();
                        _double.Start();
                        _decimal.Start();
                        _char.Start();
                        _int.Start();
                        _bool.Start();

                        _str.Join();
                        _short.Join();
                        _long.Join();
                        _float.Join();
                        _double.Join();
                        _decimal.Join();
                        _char.Join();
                        _int.Join();
                        _bool.Join();
                        break;
                        #endregion
                    }

                case CacheMode.ConcurrentDictionary:
                    {
                        #region method
                        void String()
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
                        void Short()
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
                        void Long()
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
                        void Int()
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
                        void Float()
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
                        void Double()
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
                        void Decimal()
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
                        void Char()
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
                        void Bool()
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
                        #endregion

                        #region thread
                        Thread _str = new Thread(String);
                        Thread _short = new Thread(Short);
                        Thread _long = new Thread(Long);
                        Thread _float = new Thread(Float);
                        Thread _double = new Thread(Double);
                        Thread _decimal = new Thread(Decimal);
                        Thread _char = new Thread(Char);
                        Thread _int = new Thread(Int);
                        Thread _bool = new Thread(Bool);

                        _str.Start();
                        _short.Start();
                        _long.Start();
                        _float.Start();
                        _double.Start();
                        _decimal.Start();
                        _char.Start();
                        _int.Start();
                        _bool.Start();

                        _str.Join();
                        _short.Join();
                        _long.Join();
                        _float.Join();
                        _double.Join();
                        _decimal.Join();
                        _char.Join();
                        _int.Join();
                        _bool.Join();
                        break;
                        #endregion
                    }

#if NET8_0_OR_GREATER
                case CacheMode.TripleDictionaryCache:
                    TripleDictParse();
                    break;
#endif
            }
        }

        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        [Obsolete]
        public void ParseAsync_V1()
        {
            switch (_cacheMode)
            {
                case CacheMode.CDAndSTL:
                    CDAndSTL();
                    break;
                case CacheMode.SemaphoreThreadLock:
                    SemaphoreThreadLock();
                    break;
                case CacheMode.ConcurrentDictionary:
                    ConcurrentDictionary();
                    break;
#if NET8_0_OR_GREATER
                case CacheMode.TripleDictionaryCache:
                    throw new NotSupportedException("Three-value dictionaries do not yet support asynchronous methods.");
                    // break;
#endif
            }
            async void CDAndSTL()
            {
                #region tasks
                // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行
                Task StringCacheTask = Task.Factory.StartNew(() => String(), TaskCreationOptions.LongRunning);
                Task IntCacheTask = Task.Factory.StartNew(() => Int(), TaskCreationOptions.LongRunning);
                Task ShortCacheTask = Task.Factory.StartNew(() => Short(), TaskCreationOptions.LongRunning);
                Task LongCacheTask = Task.Factory.StartNew(() => Long(), TaskCreationOptions.LongRunning);
                Task FloatCacheTask = Task.Factory.StartNew(() => Float(), TaskCreationOptions.LongRunning);
                Task DoubleCacheTask = Task.Factory.StartNew(() => Double(), TaskCreationOptions.LongRunning);
                Task DecimalCacheTask = Task.Factory.StartNew(() => Decimal(), TaskCreationOptions.LongRunning);
                Task CharCacheTask = Task.Factory.StartNew(() => Char(), TaskCreationOptions.LongRunning);
                Task BoolCacheTask = Task.Factory.StartNew(() => Bool(), TaskCreationOptions.LongRunning);

                // 等待所有任务完成
                await Task.WhenAll(StringCacheTask,
                                   IntCacheTask,
                                   ShortCacheTask,
                                   LongCacheTask,
                                   FloatCacheTask,
                                   DoubleCacheTask,
                                   DecimalCacheTask,
                                   CharCacheTask,
                                   BoolCacheTask);
                #endregion

                #region method
                void String()
                {
                    var dict = DataParser_V1.ParseString(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlStringDictionary.Set(key, value);
                        cdStringDictionary.Set(key, value);
                    }
                }
                void Short()
                {
                    var dict = DataParser_V1.ParseShort(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlShortDictionary.Set(key, value);
                        cdShortDictionary.Set(key, value);
                    }
                }
                void Long()
                {
                    var dict = DataParser_V1.ParseLong(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlLongDictionary.Set(key, value);
                        cdLongDictionary.Set(key, value);
                    }
                }
                void Int()
                {
                    var dict = DataParser_V1.ParseInt(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlIntegerDictionary.Set(key, value);
                        cdIntegerDictionary.Set(key, value);
                    }
                }
                void Float()
                {
                    var dict = DataParser_V1.ParseFloat(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlFloatDictionary.Set(key, value);
                        cdFloatDictionary.Set(key, value);
                    }
                }
                void Double()
                {
                    var dict = DataParser_V1.ParseDouble(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlDoubleDictionary.Set(key, value);
                        cdDoubleDictionary.Set(key, value);
                    }
                }
                void Decimal()
                {
                    var dict = DataParser_V1.ParseDecimal(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlDecimalDictionary.Set(key, value);
                        cdDecimalDictionary.Set(key, value);
                    }
                }
                void Char()
                {
                    var dict = DataParser_V1.ParseChar(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlCharDictionary.Set(key, value);
                        cdCharDictionary.Set(key, value);
                    }
                }
                void Bool()
                {
                    var dict = DataParser_V1.ParseBoolean(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlBooleanDictionary.Set(key, value);
                        cdBooleanDictionary.Set(key, value);
                    }
                }
                #endregion
            }

            async void SemaphoreThreadLock()
            {
                #region tasks
                // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行
                Task StringCacheTask = Task.Factory.StartNew(() => String(), TaskCreationOptions.LongRunning);
                Task IntCacheTask = Task.Factory.StartNew(() => Int(), TaskCreationOptions.LongRunning);
                Task ShortCacheTask = Task.Factory.StartNew(() => Short(), TaskCreationOptions.LongRunning);
                Task LongCacheTask = Task.Factory.StartNew(() => Long(), TaskCreationOptions.LongRunning);
                Task FloatCacheTask = Task.Factory.StartNew(() => Float(), TaskCreationOptions.LongRunning);
                Task DoubleCacheTask = Task.Factory.StartNew(() => Double(), TaskCreationOptions.LongRunning);
                Task DecimalCacheTask = Task.Factory.StartNew(() => Decimal(), TaskCreationOptions.LongRunning);
                Task CharCacheTask = Task.Factory.StartNew(() => Char(), TaskCreationOptions.LongRunning);
                Task BoolCacheTask = Task.Factory.StartNew(() => Bool(), TaskCreationOptions.LongRunning);

                // 等待所有任务完成
                await Task.WhenAll(StringCacheTask,
                                   IntCacheTask,
                                   ShortCacheTask,
                                   LongCacheTask,
                                   FloatCacheTask,
                                   DoubleCacheTask,
                                   DecimalCacheTask,
                                   CharCacheTask,
                                   BoolCacheTask);
                #endregion

                #region method
                void String()
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
                void Short()
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
                void Long()
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
                void Int()
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
                void Float()
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
                void Double()
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
                void Decimal()
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
                void Char()
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
                void Bool()
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
                #endregion
            }

            async void ConcurrentDictionary()
            {
                #region tasks
                // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行
                Task StringCacheTask = Task.Factory.StartNew(() => String(), TaskCreationOptions.LongRunning);
                Task IntCacheTask = Task.Factory.StartNew(() => Int(), TaskCreationOptions.LongRunning);
                Task ShortCacheTask = Task.Factory.StartNew(() => Short(), TaskCreationOptions.LongRunning);
                Task LongCacheTask = Task.Factory.StartNew(() => Long(), TaskCreationOptions.LongRunning);
                Task FloatCacheTask = Task.Factory.StartNew(() => Float(), TaskCreationOptions.LongRunning);
                Task DoubleCacheTask = Task.Factory.StartNew(() => Double(), TaskCreationOptions.LongRunning);
                Task DecimalCacheTask = Task.Factory.StartNew(() => Decimal(), TaskCreationOptions.LongRunning);
                Task CharCacheTask = Task.Factory.StartNew(() => Char(), TaskCreationOptions.LongRunning);
                Task BoolCacheTask = Task.Factory.StartNew(() => Bool(), TaskCreationOptions.LongRunning);

                // 等待所有任务完成
                await Task.WhenAll(StringCacheTask,
                                   IntCacheTask,
                                   ShortCacheTask,
                                   LongCacheTask,
                                   FloatCacheTask,
                                   DoubleCacheTask,
                                   DecimalCacheTask,
                                   CharCacheTask,
                                   BoolCacheTask);
                #endregion

                #region method
                void String()
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
                void Short()
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
                void Long()
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
                void Int()
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
                void Float()
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
                void Double()
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
                void Decimal()
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
                void Char()
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
                void Bool()
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
                #endregion
            }
        }

#if NET8_0_OR_GREATER
        // 三值字典操作
        /// <summary>
        /// Parses the database and writes to the three-value dictionary cache.<br />
        /// 解析数据库并写入三值字典缓存。
        /// </summary>
        public void TripleDictParse()
        {
            #region 各数据类型字典
            var _str = DataParser_V1.ParseString(_databaseContent);
            var _short = DataParser_V1.ParseShort(_databaseContent);
            var _long = DataParser_V1.ParseLong(_databaseContent);
            var _int = DataParser_V1.ParseInt(_databaseContent);
            var _float = DataParser_V1.ParseFloat(_databaseContent);
            var _double = DataParser_V1.ParseDouble(_databaseContent);
            var _decimal = DataParser_V1.ParseDecimal(_databaseContent);
            var _char = DataParser_V1.ParseChar(_databaseContent);
            var _bool = DataParser_V1.ParseBoolean(_databaseContent);
            #endregion

            #region 方法
            void Str()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _str)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("string", key, value);
                }
            }

            void Short()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _short)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("short", key, value);
                }
            }

            void Long()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _long)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("long", key, value);
                }
            }

            void Int()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _int)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("int", key, value);
                }
            }

            void Float()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _float)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("float", key, value);
                }
            }

            void Double()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _double)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("double", key, value);
                }
            }

            void Decimal()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _decimal)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("decimal", key, value);
                }
            }

            void Char()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _char)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("char", key, value);
                }
            }

            void Bool()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _bool)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("bool", key, value);
                }
            }
            #endregion

            #region 线程
            #region 创建线程实例
            Thread strThread = new(Str);
            Thread shortThread = new(Short);
            Thread longThread = new(Long);
            Thread floatThread = new(Float);
            Thread doubleThread = new(Double);
            Thread decimalThread = new(Decimal);
            Thread charThread = new(Char);
            Thread intThread = new(Int);
            Thread boolThread = new(Bool);
            #endregion
            #region 启动线程
            strThread.Start();
            shortThread.Start();
            longThread.Start();
            floatThread.Start();
            doubleThread.Start();
            decimalThread.Start();
            charThread.Start();
            intThread.Start();
            boolThread.Start();
            #endregion
            #region 等待线程执行完毕
            strThread.Join();
            shortThread.Join();
            longThread.Join();
            floatThread.Join();
            doubleThread.Join();
            decimalThread.Join();
            charThread.Join();
            intThread.Join();
            boolThread.Join();
            #endregion
            #endregion
        }
#endif
    }
}
