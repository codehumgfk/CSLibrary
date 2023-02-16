using System.Globalization;

namespace CryptoUtil
{
    public interface ICryptoAlgorithm
    {
        string Encrypt(string val);
        string Decrypt(string val);
    }
}