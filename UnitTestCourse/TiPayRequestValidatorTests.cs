using System;
using SystemUnderTest;
using NSubstitute;
using NUnit.Framework;

namespace UnitTestCourse
{
    [TestFixture]
    public class TiPayRequestValidatorTests
    {
        private IMerchantRepository _IMerchantRepository;
        private TiPayRequestValidator _TiPayRequestValidator;

        [SetUp]
        public void Setup()
        {
            _IMerchantRepository = Substitute.For<IMerchantRepository>();
            _TiPayRequestValidator = new TiPayRequestValidator(_IMerchantRepository);
        }

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

            _IMerchantRepository.GetMerchantKey(Arg.Any<string>()).Returns("asdf1234");

            var ex = Assert.Catch<Exception>(() => _TiPayRequestValidator.Validate(request));
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

            _IMerchantRepository.GetMerchantKey(Arg.Any<string>()).Returns("asdf1234");
            Assert.DoesNotThrow(()=> _TiPayRequestValidator.Validate(request));
        }

    }

}