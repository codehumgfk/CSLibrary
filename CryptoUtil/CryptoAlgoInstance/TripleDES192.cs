using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtil.CryptoAlgoInstance
{
    internal class TripleDES192 : ICryptoAlgorithm
    {
        public readonly int KeySize = 192;
        public readonly int BlockSize = 96;
        private readonly byte[] _byteSecretKey;
        private readonly byte[] _byteIV;

        #region Constructor
        /// <summary>
        /// set <paramref name="secretKey"/> and an initialized vector, derived from a hash of <paramref name="secretKey"/> calculated with sha1.
        /// </summary>
        /// <param name="secretKey">secret key</param>
        public TripleDES192(string secretKey)
        {
            _byteSecretKey = new byte[KeySize / 8];
            _byteIV = new byte[BlockSize / 8];
            var secretByte = Encoding.UTF8.GetBytes(secretKey);
            var len = secretByte.Length > KeySize ? KeySize : secretByte.Length;
            Array.Copy(secretByte, _byteSecretKey, len);
            var sha1 = SHA1.Create();
            var secret1 = sha1.ComputeHash(secretByte);
            Array.Copy(secret1, _byteIV, _byteIV.Length);
        }
        #endregion

        public string Encrypt(string val)
        {
            var tripledes = TripleDES.Create();
            tripledes.KeySize = KeySize;
            tripledes.BlockSize = BlockSize;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;

            var byteVal = Encoding.UTF8.GetBytes(val);
            string result;
            using (var memStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memStream, tripledes.CreateEncryptor(_byteSecretKey, _byteIV), CryptoStreamMode.Write))
            {
                cryptoStream.Write(byteVal, 0, byteVal.Length);
                result = Convert.ToBase64String(byteVal);
            }

            return result;
        }
        public string Decrypt(string val)
        {
            var tripledes = TripleDES.Create();
            tripledes.KeySize = KeySize;
            tripledes.BlockSize = BlockSize;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;

            var byteVal = Convert.FromBase64String(val);
            string result;
            using (var memStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memStream, tripledes.CreateEncryptor(_byteSecretKey, _byteIV), CryptoStreamMode.Write))
            {
                cryptoStream.Write(byteVal, 0, byteVal.Length);
                result = Encoding.UTF8.GetString(byteVal);
            }

            return result;
        }
    }
}

