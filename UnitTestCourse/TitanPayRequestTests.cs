using System;
using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class TitanPayRequestTests
    {
        private class FakeTitanPayRequest : TitanPayRequest
        {
            public DateTime FixedDateTime { get; set; }
            private const string _merchantKey = "asdf1234";
            public override string GetMerchantKey()
            {
                return _merchantKey;
            }

            protected override DateTime GetCurrentDateTime()
            {
                return FixedDateTime;
            }

        }
        
        // 3. write tests for TitanPayRequest.Sign()
        [Test]
        public void Sign_WithValidAmount_ShouldGenerateCorrectSignature()
        {
            var titanPayRequest = new TitanPayRequest()
            {
                Amount = 100
            };
            
            titanPayRequest.Sign();
            const string expected = "fd98262a120ec2f1c7612f7fa0a5cb29";

            var actual = titanPayRequest.Signature;
            ShouldGenerateCorrectSignature(expected, actual);
        }

        private static void ShouldGenerateCorrectSignature(string expected, string actual)
        {
            Assert.AreEqual(expected, actual);
        }

        // 4. write unit test for Sign2
        [Test]
        public void Sign2_WithValidMerchantKey_ShouldGenerateCorrectSignature()
        {
            var fakeTitanPayRequest = new FakeTitanPayRequest()
            {
                Amount = 100
            };
            fakeTitanPayRequest.Sign2();
            const string expected = "fd98262a120ec2f1c7612f7fa0a5cb29";
            ShouldGenerateCorrectSignature(expected, fakeTitanPayRequest.Signature);
        }

        // 5. write unit test for Sign3
        [Test]
        public void Sign3_WithValidCreatedOnShouldGenerateCorrectSignature()
        {
            var fakeTitanPayRequest = new FakeTitanPayRequest()
            {
                Amount = 100,
                FixedDateTime = new DateTime(2023, 11, 11, 11, 11, 11)
            };
            
            fakeTitanPayRequest.Sign3();
            const string expected = "82363f06b5f4b652d81b37cc28562fce";
            ShouldGenerateCorrectSignature(expected, fakeTitanPayRequest.Signature);

        }
    }
}