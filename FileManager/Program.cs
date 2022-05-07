using System;
using System.Activities.Statements;
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
            var Dir = new DirectoryInfo("C:\\ProgramData");
            var DirTo = new DirectoryInfo("C:\\1234");
            var Copy = Command.CopyAsync (Dir,DirTo);

            Console.WriteLine(Command.Message);
            do
            {
                Console.WriteLine("Программа ожидает завершения копирования...");
                System.Threading.Thread.Sleep(5000);
            }
            while (! Copy.IsCompleted );

            if (Command.Error)
            {
                Console.WriteLine(Command.Message);
            }
            else
            {
                Console.WriteLine("Успешно");
            }
            
            Console.WriteLine("Типа готово?");
            Copy.Dispose();

            Console.ReadKey(false);

            Console.WriteLine("Удаляю");
            Command.Delete (DirTo);

        }
    }
}
