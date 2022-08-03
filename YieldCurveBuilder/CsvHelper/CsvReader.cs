using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace CsvHelper
{
    public static class CsvReader
    {
        public static List<string> ReadCsv(string filepath, bool ignoreHeader=true)
        {
            CheckFileExistence(filepath);
            if (ignoreHeader) return File.ReadLines(filepath).Skip(1).ToList();
            return File.ReadLines(filepath).ToList();
        }

        private static void CheckFileExistence(string filepath)
        {
            if (File.Exists(filepath)) return;
            throw new ArgumentException("The specified file does not exist. File:" + filepath);
        }
    }
}
