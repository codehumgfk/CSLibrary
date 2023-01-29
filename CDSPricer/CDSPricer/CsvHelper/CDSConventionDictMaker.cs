using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CDSPricer.Convention;

namespace CDSPricer.CsvHelper
{
    public static class CDSConventionDictMaker
    {
        public static Dictionary<string, CDSConvention> GetDict(string filepath, bool ignoreHeader = true)
        {
            var res = new Dictionary<string, CDSConvention>();
            var lines = File.ReadLines(filepath).Skip(1).ToList();

            foreach (var line in lines)
            {
                var splitted = line.Split(',');
                var kvpair = CsvParser.ParseCDSConvention(splitted);
                res.Add(kvpair.Key, kvpair.Value);
            }
            return res;
        }
    }
}
