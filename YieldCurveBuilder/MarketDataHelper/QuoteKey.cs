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
        public override bool Equals(object obj)
        {
            var qkObj = obj as QuoteKey;
            return IRIndex.Equals(qkObj.IRIndex) && Product == qkObj.Product && ProductTerm == qkObj.ProductTerm;
        }
        public override int GetHashCode()
        {
            return IRIndex.GetHashCode() + Product.GetHashCode() + ProductTerm.GetHashCode();
        }
        public override string ToString()
        {
            return IRIndex.ToString() + string.Format(",{0},{1}", Product, ProductTerm);
        }
    }
}
