using System;
using Xunit;
using Moq;
using CreditCardApplications.FrequentFlyerNumberValidator;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication { GrossAnualInput = 100_000 };
            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCarApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.DefaultValue = DefaultValue.Mock;

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication { Age = 19 };
            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCarApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.DefaultValue = DefaultValue.Mock;

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication
            {
                Age = 42,
                GrossAnualInput = 19_000
            };
            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCarApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ReferWhenLicenseExpired()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns(GetLicense);

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication
            {
                Age = 42
            };

            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCarApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void UseDetailedLookupWithOlderApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns(GetLicense);
            mockValidator.SetupProperty(x => x.ValidationMode);

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication
            {
                Age = 42
            };

            var decision = sut.Evaluate(application);

            Assert.Equal(ValidationMode.Detailed, mockValidator.Object.ValidationMode);
        }

        [Fact]
        public void ValidateFrequentFlyerNumberForLowIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication
            {
                GrossAnualInput = 10_000,
                FrecuentFlyerNumber = "1234"
            };

            sut.Evaluate(application);

            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void NotValidateFrequentFlyerNumberForHighIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication
            {
                GrossAnualInput = 100_000,
                FrecuentFlyerNumber = "1234"
            };

            sut.Evaluate(application);

            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void CheckLicenseForLowIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication
            {
                GrossAnualInput = 10_000,
                FrecuentFlyerNumber = "1234"
            };

            sut.Evaluate(application);

            mockValidator.VerifyGet(x => x.ServiceInformation.License.LicenseKey, Times.Once);
        }

        [Fact]
        public void SetDetailedLookupForOlderApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication
            {
                Age = 30
            };

            sut.Evaluate(application);

            mockValidator.VerifySet(x => x.ValidationMode = It.IsAny<ValidationMode>(), Times.Once);
        }

        string GetLicense()
        {
            return "EXPIRED";
        }
    }
}
