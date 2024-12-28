using System.Diagnostics;

namespace TimeRecord
{
    public class HighPrecisionTimer
    {
        private Stopwatch stopwatch;

        public HighPrecisionTimer()
        {
            stopwatch = new Stopwatch();
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        public void Start()
        {
            stopwatch.Start();
        }

        /// <summary>
        /// 停止计时器
        /// </summary>
        public void Stop()
        {
            stopwatch.Stop();
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void Reset()
        {
            stopwatch.Reset();
        }

        /// <summary>
        /// 获取已用时间（毫秒）
        /// </summary>
        /// <returns></returns>
        public double ElapsedMilliseconds()
        {
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// 获取已用时间（秒）
        /// </summary>
        /// <returns></returns>
        public double ElapsedSeconds()
        {
            return stopwatch.Elapsed.TotalSeconds;
        }
    }
}
