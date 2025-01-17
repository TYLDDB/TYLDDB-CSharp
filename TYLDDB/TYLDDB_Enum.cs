namespace TYLDDB
{
    /// <summary>
    /// Cache mode.<br />
    /// 缓存模式。
    /// </summary>
    public enum CacheMode
    {
        /// <summary>
        /// Use concurrent dictionaries to achieve high concurrency stability.<br />
        /// 使用并发词典来实现高并发的稳定性。
        /// </summary>
        ConcurrentDictionary,

        /// <summary>
        /// Use semaphore based thread locks to achieve high concurrency stability.<br />
        /// 使用基于信号量的线程锁来实现高并发的稳定性。
        /// </summary>
        SemaphoreThreadLock,

        /// <summary>
        /// High concurrency stability is achieved by using concurrent dictionary as core and semaphore based thread lock.<br />
        /// 以并发词典为核，结合基于信号量的线程锁实现高并发的稳定性。
        /// </summary>
        TripleDictionaryCache,

        /// <summary>
        /// <see cref="ConcurrentDictionary"/> + <see cref="SemaphoreThreadLock"/>
        /// </summary>
        CDAndSTL,
    }

    /// <summary>
    /// Database pre-parsing process.<br />
    /// 数据库预解析流程。
    /// </summary>
    public enum DatabaseProcess
    {
        /// <summary>
        /// Based on regular expressions.<br />
        /// 基于正则表达式。
        /// </summary>
        V1,

        /// <summary>
        /// Parse database content directly based on string segmentation.<br />
        /// 直接基于字符串分割解析数据库内容。
        /// </summary>
        V2,
    }
}
