using System;
using System.IO;

namespace Exec
{
    class Program
    {
        static void Main(string[] args)
        {
            var cwd = Path.GetFullPath(".");
            Console.WriteLine(cwd);
        }
    }
}
