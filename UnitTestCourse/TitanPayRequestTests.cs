using System;
using SystemUnderTest;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace UnitTestCourse
{
    [TestFixture]
    public class TitanPayRequestTests
    {
        public class FakeTitanPayRequest : TitanPayRequest
        {
            protected override string GetMerchantKey()
            {
                return MerchantKey;
            }

            protected override DateTime GetCreatedTime()
            {
                return DateTime;
            }


            public string MerchantKey { get; set; }

            public DateTime DateTime { get; set; }
        }

        // 2. write tests for TitanPayRequest.Sign()
        [Test]
        public void sign_RequestWithLess1000_ShouldReturnCorrectMd5HashCode()
        {
            var payHelper = new TitanPayRequest()
            {
                Amount = 999
            }; 

            payHelper.Sign();
            var expected = "137bdcd797c899a19e8b6abf5a81a901";
            var actual = payHelper.Signature;
            Assert.AreEqual(expected, actual);

        }

        // 3. write unit test for Sign2
        [Test]
        public void calculate_signature_with_key_from_file()
        {
            var linePayRequest = new FakeTitanPayRequest()
            {
                Amount = 999,
                MerchantKey = "asdf1234"
            };
            
            linePayRequest.Sign2();

            Assert.AreEqual("137bdcd797c899a19e8b6abf5a81a901", linePayRequest.Signature);
        }


        // 4. write unit test for Sign3
        [Test]
        public void calculate_signature_with_created_on()
        {
            var payHelper = new FakeTitanPayRequest()
            {
                Amount = 999,
                DateTime = Convert.ToDateTime("2010-03-01T06:04:11")
            };

            payHelper.Sign3();

            Assert.AreEqual("d0ce6e9ee457fae7b49c27e6ca6ced79", payHelper.Signature);
        }
    }
}