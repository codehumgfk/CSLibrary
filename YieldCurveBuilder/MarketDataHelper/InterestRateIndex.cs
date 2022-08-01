using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public class InterestRateIndex
    {
        public EIndexKind IndexKind;
        public double Tenor; // months
        public string Currency;

        public InterestRateIndex(EIndexKind eIndexKind, double tenor, string currency)
        {
            IndexKind = eIndexKind;
            Tenor = tenor;
            Currency = currency;
        }

        public InterestRateIndex(string sIndexKind, double tenor, string currency)
        {
            IndexKind = EnumParser.ParseEIndexKind(sIndexKind);
            Tenor = tenor;
            Currency = currency;
        }

        public InterestRateIndex Copy()
        {
            return new InterestRateIndex(IndexKind, Tenor, Currency);
        }

        public override string ToString()
        {
            return string.Join('.', new List<string> { IndexKind.ToString(), Tenor.ToString(), Currency.ToString()});
        }
    }
}
