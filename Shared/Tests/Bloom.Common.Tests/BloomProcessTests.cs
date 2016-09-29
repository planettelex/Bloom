using NUnit.Framework;

namespace Bloom.Common.Tests
{
    /// <summary>
    /// Tests for the Bloom process class.
    /// </summary>
    [TestFixture]
    public class BloomProcessTests
    {
        /// <summary>
        /// Tests the Bloom process properties.
        /// </summary>
        [Test]
        public void BloomProcessPropertiesTest()
        {
            var process1 = new BloomProcess(ProcessType.Analytics);
            var process2 = new BloomProcess("Bloom.Browser");
            var process3 = new BloomProcess("Player");
            
            Assert.AreEqual(process1.Type, ProcessType.Analytics);
            Assert.AreEqual(process1.Name, "Bloom.Analytics");
            Assert.AreEqual(process2.Type, ProcessType.Browser);
            Assert.AreEqual(process2.Name, "Bloom.Browser");
            Assert.AreEqual(process3.Type, ProcessType.Player);
            Assert.AreEqual(process3.Name, "Bloom.Player");
        }
    }
}
