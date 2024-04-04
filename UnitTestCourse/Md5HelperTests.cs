using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class Md5HelperTests
    {
        // 1. write a test for Md5Helper
        // online md5 hash generator: https://www.md5hashgenerator.com/
        [Test]
        public void Hash_InputAnySpecificString_ShouldReturnRightHashedString()
        {
            var helper = new Md5Helper();
            var inputString = "test";
            var hashString = helper.Hash(inputString);
            Assert.AreEqual("098f6bcd4621d373cade4e832627b4f6", hashString);

        }
    }
}