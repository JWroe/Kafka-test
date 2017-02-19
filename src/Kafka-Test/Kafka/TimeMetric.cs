using System;
using System.Diagnostics;

namespace Kafka
{
    public class TimeMetric
    {
        private readonly Stopwatch _stopwatch;

        public TimeMetric() : this(0)
        {
        }

        public TimeMetric(int count) : this(count, new Stopwatch())
        {
        }

        private TimeMetric(int count, Stopwatch stopwatch)
        {
            Count = count;
            _stopwatch = stopwatch;
        }

        public int Count { get; }

        public TimeSpan TimeTaken => _stopwatch.Elapsed;

        public TimeMetric Increment()
        {
            return new TimeMetric(Count + 1, _stopwatch);
        }

        public TimeMetric RunningMetric()
        {
            _stopwatch.Start();
            return new TimeMetric(Count, _stopwatch);
        }

        public TimeMetric StoppedMetric()
        {
            _stopwatch.Stop();
            return new TimeMetric(Count, _stopwatch);
        }
    }
}