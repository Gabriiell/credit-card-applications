namespace CreditCardApplications.FrequentFlyerNumberValidator
{
    public interface IFrequentFlyerNumberValidator
    {
        string LicenseKey { get; }
        bool IsValid(string frequentFlyerNumber);
        void IsValid(string frequentFlyerNumber, out bool isValid);
    }
}
