using System;
using NSubstitute;
using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class TiPayRequestValidatorTests
    {
        private IMerchantRepository _merchantRepository;
        private TiPayRequestValidator _tiPayRequestValidator;
        private int _amount;
        private string _merchantCode;

        [SetUp]
        public void SetUp()
        {
            _merchantRepository = Substitute.For<IMerchantRepository>();
            _tiPayRequestValidator = new TiPayRequestValidator(_merchantRepository);
        }
        // 2. write unit tests for TiPayRequestValidator
        [Test]
        public void Validate_InputRightSignature_ShouldNotThrowException()
        {
            // Given
            GivenMerchantKey("merchantKey");
            GivenAmount(100);
            GivenMerchantCode("merchantCode");

            ShouldNotThrowException("dc1cb83ee10fb9d943d6694363ea8586");
        }

        [Test]
        public void Validate_InputWrongSignature_ShouldThrowException()
        {
            // Given
            GivenMerchantKey("merchantKey");
            GivenAmount(100);
            GivenMerchantCode("merchantCode");

            ShouldThrowException("adc1cb83ee10fb9d943d6694363ea858");
        }

        private void ShouldThrowException(string expected)
        {
            Assert.Throws<Exception>(() =>
            {
                _tiPayRequestValidator.Validate(new TiPayRequest()
                {
                    MerchantCode = _merchantCode,
                    Amount = _amount,
                    Signature = expected
                });
            });
        }

        private void GivenMerchantCode(string merchantcode)
        {
            _merchantCode = merchantcode;
        }

        private void ShouldNotThrowException(string expected)
        {
            Assert.DoesNotThrow(() =>
            {
                _tiPayRequestValidator.Validate(new TiPayRequest()
                {
                    MerchantCode = _merchantCode,
                    Amount = _amount,
                    Signature = expected
                });
            });
        }

        private void GivenMerchantKey(string merchant)
        {
            _merchantRepository.GetMerchantKey(Arg.Any<string>()).Returns(merchant);
        }

        private void GivenAmount(int amount)
        {
            _amount = amount;
        }
    }
}