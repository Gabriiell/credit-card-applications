using System;
using Xunit;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            var sut = new CreditCarApplicationEvaluator();
            var application = new CreditCarApplication { GrossAnualInput = 100_000 };
            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCarApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {
            var sut = new CreditCarApplicationEvaluator();
            var application = new CreditCarApplication { Age = 19 };
            var decision = sut.Evaluate(application);

            Assert.Equal(CreditCarApplicationDecision.ReferredToHuman, decision);
        }
    }
}
