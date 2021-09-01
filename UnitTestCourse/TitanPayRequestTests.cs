using System;
using SystemUnderTest;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTestCourse
{
    class FakeFileManager : IFileManager
    {
        public string MerchantKey { get; set; }
        public string GetMerchantKey()
        {
            return MerchantKey;
        }
    }

    class FakeDateManager : IDateManager
    {
        public DateTime GetDate()
        {
            return FakeDateTime;
        }

        public DateTime FakeDateTime { get; set; }
    }

    [TestFixture]
    public class TitanPayRequestTests
    {
        // 2. write tests for TitanPayRequest.Sign()
        [TestCase(999, "137bdcd797c899a19e8b6abf5a81a901")]
        [TestCase(99, "2d396053e6c95272ed2a8b3e90cc1b07")]
        [TestCase(0, "15b8227044a11d6edd31f0684375f313")]
        public void Sign_InputAnyAmount_ShouldReturnRightMd5HashCode(int amount, string expected)
        {
            var payRequest = new TitanPayRequest()
            {
                Amount = amount
            };

            payRequest.Sign();

            Assert.AreEqual(expected, payRequest.Signature);
        }

        // 3. write unit test for Sign2
        [Test]
        public void Sign2_InputAnyAmount_ShouldReturnRightMd5HashCode()
        {
            var payRequest = new TitanPayRequest()
            {
                Amount = 999
            };

            var fileManager = new FakeFileManager(){MerchantKey = "asdf1234" };
            payRequest.FileManager = fileManager;

            payRequest.Sign2();

            Assert.AreEqual("137bdcd797c899a19e8b6abf5a81a901", payRequest.Signature);
        }


        // 4. write unit test for Sign3
        [Test]
        public void Sign3_InputAnyAmount_ShouldReturnRightMd5HashCode()
        {
            var payRequest = new TitanPayRequest()
            {
                Amount = 999
            };
            var dateManager = new FakeDateManager()
            {
                FakeDateTime = Convert.ToDateTime("2021-03-10T10:14:55")
            };

            payRequest.DateManager = dateManager;

            payRequest.Sign3();
            Assert.AreEqual("108bcd5c402ae02264e5c5787b1595a9", payRequest.Signature);

        }
    }
}