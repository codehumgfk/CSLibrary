using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public static class EnumParser
    {
        public static EIndexKind ParseEIndexKind(string sIndexKind)
        {
            switch (sIndexKind)
            {
                case "Libor":
                    return EIndexKind.Libor;
                case "LIBOR":
                    return EIndexKind.Libor;
                case "Tonar":
                    return EIndexKind.Tonar;
                case "TONAR":
                    return EIndexKind.Tonar;
                default:
                    throw new ArgumentException(string.Format("The selected IndexKind {0} is not in EIndexKind.", sIndexKind));
            }
        }

        public static EProduct ParseEProduct(string sProduct)
        {
            switch (sProduct)
            {
                case "Depo":
                    return EProduct.Depo;
                case "Fra":
                    return EProduct.Fra;
                case "Swap":
                    return EProduct.Swap;
                case "Ois":
                    return EProduct.Ois;
                default:
                    throw new ArgumentException(string.Format("The product, {0}, is not in EProduct.", sProduct));
            }
        }
    }
}
