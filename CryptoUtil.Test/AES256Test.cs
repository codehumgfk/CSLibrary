using CryptoUtil;

namespace CryptoUtil.Test
{
    public static class AES256Test
    {
        public static void EncryptThenDecryptTest()
        {
            var secretKey = "testing";
            var plain = "AES256";
            var arrow = "{0} -> {1}";

            Console.WriteLine("plain text: " + plain);
            var cryptor = new Cryptor(ECryptoAlgorithm.AES256, secretKey);
            var encrypted = cryptor.Encrypt(plain);
            Console.WriteLine("Encryption: " + string.Format(arrow, plain, encrypted));
            var decrypted = cryptor.Decrypt(encrypted);
            Console.WriteLine("Decryption: " + string.Format(arrow, encrypted, decrypted));
            var result = "Test " + (plain == decrypted ? "Successed" : "Failed");
            Console.WriteLine(result);
        }
    }
}