using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    internal class Program
    {
        static void Main()
        {
            var Dir = new DirectoryInfo("H:\\123");
            Command.Delete(Dir);
            Console.WriteLine(Command.Message);

            Console.ReadKey(false);
        }
    }
}
