using System;

namespace CreditCardApplications.FrequentFlyerNumberValidator
{
    public class FrequentFlyerNumberValidator : IFrequentFlyerNumberValidator
    {
        public IServiceInformation ServiceInformation => throw new NotImplementedException();

        public ValidationMode ValidationMode
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

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
