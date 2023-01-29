using System;
using System.Collections.Generic;
using System.Text;

namespace CDSPricer.Market
{
    public class CDSQuoteKey
    {
        public string Entity;
        public string Currency;
        public int Tenor;

        public CDSQuoteKey(string entity, string currency, int tenor)
        {
            Entity = entity;
            Currency = currency;
            Tenor = tenor;
        }
    }
}
