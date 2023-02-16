using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoUtil.CryptoAlgoInstance;

namespace CryptoUtil
{
    public static class CryptoAlgoFactory
    {
        public static ICryptoAlgorithm Create(ECryptoAlgorithm algo, string secretKey)
        {
            switch (algo)
            {
                case ECryptoAlgorithm.AES256:
                    return new AES256(secretKey);
                case ECryptoAlgorithm.TripleDES192:
                    return new TripleDES192(secretKey);
                default:
                    throw new NotSupportedException(string.Format("Add 'CryptoAlgoInstance' of {0} to 'CryptoAlgoFactory'", algo.ToString()));
            }
        }
    }
}
