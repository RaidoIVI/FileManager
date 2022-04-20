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
            var DirTo = new DirectoryInfo("H:\\1234");
            Command.Copy (Dir,DirTo);

            Console.WriteLine(Command.Message);

            Console.ReadKey(false);
        }
    }
}
