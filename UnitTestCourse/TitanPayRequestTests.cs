using System;
using NSubstitute;
using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class TitanPayRequestTests
    {
        private IMerchantKeyProvider _merchantKeyProvider;
        private const int Amount = 100;
        private DateTime _createdOn;
        private string _signature;
        private TitanPayRequest _titanPayRequest;
        private IDatetimeProvider _datetimeProvider;

        [SetUp]
        public void SetUp()
        {
            _merchantKeyProvider = Substitute.For<IMerchantKeyProvider>();
            _datetimeProvider = Substitute.For<IDatetimeProvider>();
            _titanPayRequest = new TitanPayRequest(Amount, _signature, _createdOn, _merchantKeyProvider, _datetimeProvider);
        }

        // 3. write tests for TitanPayRequest.Sign()
        [Test]
        public void Sign_WithValidInputs_ShouldGenerateCorrectSignature()
        {
            _titanPayRequest.Sign();
            const string expectedSignature = "fd98262a120ec2f1c7612f7fa0a5cb29";
            ShouldGenerateCorrectSignature(expectedSignature);
        }

        private void ShouldGenerateCorrectSignature(string expected)
        {
            Assert.AreEqual(expected, _titanPayRequest.Signature);
        }
        
        private void GivenMerchantKey(string merchantKey)
        {
             _merchantKeyProvider.Get().Returns(merchantKey);
        }
        

        // 4. write unit test for Sign2
        [Test]
        public void Sign2_WithKeyFromFile_ShouldGenerateCorrectSignature()
        {
            GivenMerchantKey("asdf1234");
            _titanPayRequest.Sign2();
            ShouldGenerateCorrectSignature("fd98262a120ec2f1c7612f7fa0a5cb29");
        }

        // 5. write unit test for Sign3
        [Test]
        public void Sign3_WithCustomCreatedOn_ShouldGenerateCorrectSignature()
        {
            _createdOn = new DateTime(2024, 11, 11, 11, 11, 11);
            GivenCreatedOn(_createdOn);
            _titanPayRequest.Sign3();
            ShouldGenerateCorrectSignature("49de8a54b2363ae42c6373d86570e13c");
        }

        private void GivenCreatedOn(DateTime createdOn)
        {
            _datetimeProvider.Get().Returns(createdOn);
        }
    }
}