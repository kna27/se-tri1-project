using NUnit.Framework;

namespace Tests
{
    public class time_format
    {
        [Test]
        public void formats_1_minute_30_seconds()
        {
            // ACT
            string formattedTime = TimeFormat.FormatTime(1, 30);

            // ASSERT
            Assert.AreEqual("1:30", formattedTime);
        }

        [Test]
        public void formats_2_minutes_5_seconds()
        {
            // ACT
            string formattedTime = TimeFormat.FormatTime(2, 5);

            // ASSERT
            Assert.AreEqual("2:05", formattedTime);
        }
    }
}

