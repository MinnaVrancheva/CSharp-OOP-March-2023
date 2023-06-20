namespace Telephony;

public class Smartphone : ICallable, IBrowsable
{
    public string Browse(string url)
    {
        if (!ValidateUrl(url))
        {
            throw new ArgumentException($"Invalid URL!");
        }

        return $"Browsing: {url}!";
    }

    public string Call(string phoneNumber)
    {
        if (!ValidatePhoneNumber(phoneNumber))
        {
            throw new ArgumentException($"Invalid number!");
        }

        return $"Calling... {phoneNumber}";
    }

    private bool ValidateUrl(string url)
        => url.All(x => !char.IsDigit(x));

    private bool ValidatePhoneNumber(string phoneNumber)
        => phoneNumber.All(x => char.IsDigit(x));
}
