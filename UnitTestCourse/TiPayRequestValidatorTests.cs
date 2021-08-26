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
        public void Validate_InputWrongSignature_ShouldThrowsException()
        {

            var request = new TiPayRequest()
            {
                MerchantCode = "pchome",
                Amount = 999,
                CreatedOn = Convert.ToDateTime("2010-03-01T06:04:11z"),
                Signature = "234"
                // Signature = "c0a743710b803cab8a6cb7b6d54144bf"
            };

            _IMerchantRepository.GetMerchantKey(Arg.Any<string>()).Returns("asf123");

            Assert.Throws<Exception>(()=> _TiPayRequestValidator.Validate(request));

        }

        [Test]
        public void Validate_InputRightSignature_ShouldNotThrowsException()
        {

            var request = new TiPayRequest()
            {
                MerchantCode = "pchome",
                Amount = 999,
                CreatedOn = Convert.ToDateTime("2010-03-01T06:04:11z"),
                Signature = "c0a743710b803cab8a6cb7b6d54144bf"
            };

            _IMerchantRepository.GetMerchantKey(Arg.Any<string>()).Returns("asf123");

            Assert.DoesNotThrow(() => _TiPayRequestValidator.Validate(request));

        }
    }
}