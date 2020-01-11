namespace CreditCardApplications.FrequentFlyerNumberValidator
{
    public interface ILicenseData
    {
        string LicenseKey { get; }
    }

    public interface IServiceInformation
    {
        ILicenseData License { get; set; }
    }

    public enum ValidationMode
    {
        Quick,
        Detailed
    }

    public interface IFrequentFlyerNumberValidator
    {
        IServiceInformation ServiceInformation { get; }
        ValidationMode ValidationMode { get; set; }
        bool IsValid(string frequentFlyerNumber);
        void IsValid(string frequentFlyerNumber, out bool isValid);
    }
}
