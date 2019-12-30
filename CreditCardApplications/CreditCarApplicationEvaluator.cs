using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardApplications
{
    public class CreditCarApplicationEvaluator
    {
        private const int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;

        public CreditCarApplicationDecision Evaluate(CreditCarApplication application)
        {
            if (application.GrossAnualInput >= HighIncomeThreshold)
            {
                return CreditCarApplicationDecision.AutoAccepted;
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
