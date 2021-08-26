using SystemUnderTest;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTestCourse
{
    [TestFixture]
    public class Md5HelperTests
    {
        // 1. write a test for Md5Helper
        // online md5 hash generator: https://www.md5hashgenerator.com/
        [Test]
        public void Hash_InputStringHello_ShouldReturnHelloMd5HashCode()
        {
            var helper = new Md5Helper();
            var expected = GetExpectedMd5HashCode();
            var actual = helper.Hash("Hello");
            actual.Should().Be(expected);
        }

        private string GetExpectedMd5HashCode()
        {
            var expected = "8b1a9953c4611296a827abf8c47804d7";
            return expected;
        }
    }
}