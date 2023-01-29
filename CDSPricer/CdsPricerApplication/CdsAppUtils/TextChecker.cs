using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CdsPricerApplication.CdsAppUtils
{
    public static class TextChecker
    {
        public static void Double(string txt)
        {
            if (txt.Split('.').Length > 2) throw new ArgumentException("The dot must be unique.");
        }
    }
}
