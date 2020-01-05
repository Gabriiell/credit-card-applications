using System;

namespace CreditCardApplications.FrequentFlyerNumberValidator
{
    public class FrequentFlyerNumberValidator : IFrequentFlyerNumberValidator
    {
        public string LicenseKey => throw new NotImplementedException();

        public bool IsValid(string frequentFlyerNumber)
        {
            throw new NotImplementedException("For demo purposes!");
        }

        public void IsValid(string frequentFlyerNumber, out bool isValid)
        {
            throw new NotImplementedException("For demo purposes!");
        }
    }
}
