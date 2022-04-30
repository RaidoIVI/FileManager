using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileManager
{
    internal class Program
    {
        static void Main()

        {
            //var Dir = new DirectoryInfo("C:\\ProgramData");
            var DirTo = new DirectoryInfo("C:\\123");
            //var dir = new Command(Commands.DirectoryList, Dir);
            //foreach (var item in dir.fileList)
            //{
            //    Console.WriteLine(item.FullName);
            //}
            var tmp = new Command (Commands.Delete, DirTo);

            Console.ReadKey(true);
        }
    }
}

