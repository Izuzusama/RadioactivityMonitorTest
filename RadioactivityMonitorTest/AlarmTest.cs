using RadioactivityMonitor;

namespace RadioactivityMonitorTest
{
    public class MockSensor : ISensor
    {
        public double MockedSensorValue { get; set; }
        public double NextMeasure()
        {
            return MockedSensorValue;
        }
    }
    [TestClass]
    public class AlarmTest
    {
        private const double LowValue = 1;
        private const double HighValue = 25;
        private const double NormalValue = 20;
        MockSensor mockedSensor;
        public AlarmTest()
        {
            mockedSensor = new MockSensor();
        }
        [TestMethod]
        public void TestCheck_HighThreshold_ShouldGetAlarm()
        {
            mockedSensor.MockedSensorValue = HighValue;
            Alarm alarm = new Alarm(mockedSensor);
            // Default state false
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();

            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
        }
        [TestMethod]
        public void TestCheck_LowThreshold_ShouldGetAlarm()
        {
            mockedSensor.MockedSensorValue = LowValue;
            Alarm alarm = new Alarm(mockedSensor);
            // Default state false
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();

            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
        }
        [TestMethod]
        public void TestCheck_LowThenNormal_ShouldGetAlarmButNoIncrement()
        {
            mockedSensor.MockedSensorValue = LowValue;
            Alarm alarm = new Alarm(mockedSensor);
            // Default state false
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            // Low, should alarm
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
            mockedSensor.MockedSensorValue = NormalValue;
            alarm.Check();
            // Normal, alarm should not reset
            // and alarm count should not increment
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
        }
        [TestMethod]
        public void TestCheck_HighThenNormal_ShouldGetAlarmButNoIncrement()
        {
            mockedSensor.MockedSensorValue = HighValue;
            Alarm alarm = new Alarm(mockedSensor);
            // Default state false
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            // Low, should alarm
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
            mockedSensor.MockedSensorValue = NormalValue;
            alarm.Check();
            // Normal, alarm should not reset
            // and alarm count should not increment
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
        }
        [TestMethod]
        public void TestCheck_LowThenHigh_ShouldGetAlarmAndIncrement()
        {
            mockedSensor.MockedSensorValue = LowValue;
            Alarm alarm = new Alarm(mockedSensor);
            // Default state false
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            // Low, should alarm
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
            mockedSensor.MockedSensorValue = HighValue;
            alarm.Check();
            // High, should alarm
            // and alarm should increment
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(2, alarm.AlarmCount);
        }
        [TestMethod]
        public void TestCheck_HighThenLow_ShouldGetAlarmAndIncrement()
        {
            mockedSensor.MockedSensorValue = HighValue;
            Alarm alarm = new Alarm(mockedSensor);
            // Default state false
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            // Low, should alarm
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
            mockedSensor.MockedSensorValue = LowValue;
            alarm.Check();
            // High, should alarm
            // and alarm should increment
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(2, alarm.AlarmCount);
        }
        [TestMethod]
        public void TestCheck_LowThenHighThenNormal_ShouldGetAlarmAndIncrementThenNoIncrement()
        {
            mockedSensor.MockedSensorValue = HighValue;
            Alarm alarm = new Alarm(mockedSensor);
            // Default state false
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            // Low, should alarm
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(1, alarm.AlarmCount);
            mockedSensor.MockedSensorValue = LowValue;
            alarm.Check();
            // High, should alarm
            // and alarm should increment
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(2, alarm.AlarmCount);
            mockedSensor.MockedSensorValue = NormalValue;
            alarm.Check();
            // Normal, should alarm
            // and alarm should not increment
            Assert.IsTrue(alarm.AlarmOn);
            Assert.AreEqual(2, alarm.AlarmCount);
        }
    }
}