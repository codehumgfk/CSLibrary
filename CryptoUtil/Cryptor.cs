using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtil
{
    public class Cryptor
    {
        private ICryptoAlgorithm Algorithm;

        #region Constructor
        public Cryptor(ECryptoAlgorithm algo, string secretKey) 
        {
            Algorithm = CryptoAlgoFactory.Create(algo, secretKey);
        }
        #endregion

        public string Encrypt(string val) { return Algorithm.Encrypt(val); }
        public string Decrypt(string val) { return Algorithm.Decrypt(val); }
    }
}
