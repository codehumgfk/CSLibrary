using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.Parsers;

namespace MarketDataHelper.InterestRate
{
    public class InterestRateIndex
    {
        public readonly EIndexType IndexType;
        public readonly double Tenor; // months
        public readonly string Currency;

        public InterestRateIndex(EIndexType eIndexType, double tenor, string currency)
        {
            IndexType = eIndexType;
            Tenor = tenor;
            Currency = currency;
        }

        public InterestRateIndex(string sIndexType, string tenor, string currency)
        {
            IndexType = (EIndexType)Enum.Parse(typeof(EIndexType), sIndexType);
            Tenor = TenorParser.GetDoubleTenorFromString(tenor);
            Currency = currency;
        }
        public override bool Equals(object obj)
        {
            var irObj = obj as InterestRateIndex;
            return IndexType == irObj.IndexType && Tenor == irObj.Tenor && Currency == irObj.Currency;
        }
        public override int GetHashCode()
        {
            return IndexType.GetHashCode() + Tenor.GetHashCode() + Currency.GetHashCode();
        }
        public override string ToString()
        {
            return string.Join('.', new List<string> { IndexType.ToString(), Tenor.ToString(), Currency.ToString()});
        }
    }
}
