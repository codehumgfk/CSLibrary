using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdsPricerApplication.CdsAppUtils
{
    public static class DefaultValueSetter
    {
        public static List<string> Currency
        {
            get { return new List<string> { "JPY" }; }
        }
        public static List<string> Tenor
        {
            get { return new List<string> { "5Y" }; }
        }
        public static List<string> Protection
        {
            get { return new List<string> { "Buy", "Sell" }; }
        }
        public static List<int> TenorY
        {
            get { return Enumerable.Range(0, 6).ToList(); }
        }
        public static List<int> TenorMY0
        {
            get { return Enumerable.Range(0, 3).Select(i => 3 + 3 * i).ToList(); }
        }
        public static List<int> TenorMY1
        {
            get { return Enumerable.Range(0, 4).Select(i => 3 * i).ToList(); }
        }
        public static List<int> TenorMY5
        {
            get { return Enumerable.Range(0, 1).ToList(); }
        }
    }
}
