using System;
using SystemUnderTest;
using NSubstitute;
using NUnit.Framework;

namespace UnitTestCourse
{
    [TestFixture]
    public class TiPayRequestValidatorTests
    {
        // 5. write unit tests for TiPayRequestValidator
        [Test]
        public void Validate_WrongSignature_ShouldThrowException()
        {

            var request = new TiPayRequest()
            {
                MerchantCode = "pchome",
                Amount = 999,
                Signature = "123"
            };
            var merchantRepository = Substitute.For<IMerchantRepository>();
            merchantRepository.GetMerchantKey(default).Returns("asdf1234");
            var validator = new TiPayRequestValidator(merchantRepository);
            var ex = Assert.Catch<Exception>(() => validator.Validate(request));
            StringAssert.Contains("Signature mismatch", ex.Message);
        }

        [Test]
        public void Validate_RightSignature_ShouldNotThrowException()
        {

            var request = new TiPayRequest()
            {
                MerchantCode = "pchome",
                Amount = 999,
                Signature = "137bdcd797c899a19e8b6abf5a81a901"
            };

            var merchantRepository = Substitute.For<IMerchantRepository>();
            
            var validator = new TiPayRequestValidator(merchantRepository);
            merchantRepository.GetMerchantKey(default).Returns("asdf1234");

            Assert.DoesNotThrow(()=>validator.Validate(request));
        }

    }

}