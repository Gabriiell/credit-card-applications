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

            var sut = new CreditCarApplicationEvaluator(mockValidator.Object);
            var application = new CreditCarApplication
            {
                Age = 42,
                GrossAnualInput = 19_000
            };
            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCarApplicationDecision.AutoDeclined, decision);
        }
    }
}
