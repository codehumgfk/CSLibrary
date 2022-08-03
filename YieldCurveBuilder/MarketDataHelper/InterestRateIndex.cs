using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public class InterestRateIndex
    {
        public readonly EIndexKind IndexKind;
        public readonly double Tenor; // months
        public readonly string Currency;

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
        public override bool Equals(object obj)
        {
            var irObj = obj as InterestRateIndex;
            return IndexKind == irObj.IndexKind && Tenor == irObj.Tenor && Currency == irObj.Currency;
        }
        public override int GetHashCode()
        {
            return IndexKind.GetHashCode() + Tenor.GetHashCode() + Currency.GetHashCode();
        }
        public override string ToString()
        {
            return string.Join('.', new List<string> { IndexKind.ToString(), Tenor.ToString(), Currency.ToString()});
        }
    }
}
