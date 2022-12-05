using NUnit.Framework;

namespace Tests
{
    public class wantedness_calculations
    {
        [Test]
        public void adds_10_wantedness_to_20()
        {
            // ACT
            float finalWantedness = WantednessCalculations.AddWantedness(20, 10);

            // ASSERT
            Assert.AreEqual(30, finalWantedness);
        }

        [Test]
        public void adds_negative_10_wantedness_to_0()
        {
            // ACT
            float finalWantedness = WantednessCalculations.AddWantedness(-10, 0);

            // ASSERT
            Assert.AreEqual(0, finalWantedness);
        }

        [Test]
        public void adds_12_wantedness_to_90()
        {
            // ACT
            float finalWantedness = WantednessCalculations.AddWantedness(12, 90);

            // ASSERT
            Assert.AreEqual(100, finalWantedness);
        }
    }
}

