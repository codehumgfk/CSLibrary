using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public class QuoteKey
    {
        public InterestRateIndex IRIndex;
        public EProduct Product;
        public double ProductTerm; // months

        public QuoteKey(InterestRateIndex irIndex, EProduct eProduct, double productTerm)
        {
            IRIndex = irIndex;
            Product = eProduct;
            ProductTerm = productTerm;
        }

        public QuoteKey(InterestRateIndex irIndex, string sProduct, double productTerm)
        {
            IRIndex = irIndex;
            Product = EnumParser.ParseEProduct(sProduct);
            ProductTerm = productTerm;
        }

        public override string ToString()
        {
            return IRIndex.ToString() + string.Format(",{0},{1}", Product, ProductTerm);
        }
    }
}
