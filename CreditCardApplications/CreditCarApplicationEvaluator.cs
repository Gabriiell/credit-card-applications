using CreditCardApplications.FrequentFlyerNumberValidator;

namespace CreditCardApplications
{
    public class CreditCarApplicationEvaluator
    {
        private readonly IFrequentFlyerNumberValidator _frequentFlyerNumberValidator;

        private const int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;

        public CreditCarApplicationEvaluator(IFrequentFlyerNumberValidator frequentFlyerNumberValidator)
        {
            _frequentFlyerNumberValidator = frequentFlyerNumberValidator;
        }

        public CreditCarApplicationDecision Evaluate(CreditCarApplication application)
        {
            if (application.GrossAnualInput >= HighIncomeThreshold)
            {
                return CreditCarApplicationDecision.AutoAccepted;
            }

            if (_frequentFlyerNumberValidator.IsValid(application.FrecuentFlyerNumber))
            {
                return CreditCarApplicationDecision.ReferredToHuman;
            }

            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditCarApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnualInput < LowIncomeThreshold)
            {
                return CreditCarApplicationDecision.AutoDeclined;
            }

            return CreditCarApplicationDecision.ReferredToHuman;
        }
    }
}
