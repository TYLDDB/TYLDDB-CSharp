using System.Diagnostics;

namespace TYLDDB.Test
{
    internal class HighPrecisionTimer
    {
        private Stopwatch stopwatch;

        public HighPrecisionTimer()
        {
            stopwatch = new Stopwatch();
        }

        // 启动计时器
        public void Start()
        {
            stopwatch.Start();
        }

        // 停止计时器
        public void Stop()
        {
            stopwatch.Stop();
        }

        // 重置计时器
        public void Reset()
        {
            stopwatch.Reset();
        }

        // 获取已用时间（毫秒）
        public double ElapsedMilliseconds()
        {
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        // 获取已用时间（秒）
        public double ElapsedSeconds()
        {
            return stopwatch.Elapsed.TotalSeconds;
        }
    }
}
