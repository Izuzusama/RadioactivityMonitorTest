namespace RadioactivityMonitor
{
    public class Alarm
    {
        private const double LowThreshold = 17;
        private const double HighThreshold = 21;

        ISensor _sensor;

        public bool AlarmOn { get; private set; }
        public long AlarmCount { get; private set; }
        public Alarm(ISensor sensor)
        {
            _sensor = sensor;
        }
        public void Check()
        {
            double value = _sensor.NextMeasure();

            if (value < LowThreshold || HighThreshold < value)
            {
                AlarmOn = true;
                AlarmCount += 1;
            }
        }
    }
}
