﻿using System;
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
            var Dir = new DirectoryInfo("c:\\");
            //var DirTo = new DirectoryInfo("C:\\1234");
            //var Copy = Command.CopyAsync (Dir,DirTo);
            //var DirTo2 = new DirectoryInfo("C:\\123");
            //var Copy2 = Command.CopyAsync (Dir,DirTo2);

            //do
            //{
            //    Console.WriteLine("Программа ожидает завершения копирования...");
            //    System.Threading.Thread.Sleep(5000);
            //}
            //while (! (Copy.IsCompleted && Copy2.IsCompleted));

            //Console.WriteLine("Типа готово?");
            //Copy.Dispose();
            //Copy2.Dispose();
            ConsoleKey key = ConsoleKey.Backspace;
            int i = 1;
            var TopWindow = new Window(0, 0, 120, 30, Command.List(Dir), true);
            do
            {
                if (key == ConsoleKey.PageUp)
                {
                    i++;
                }
                if (key == ConsoleKey.PageDown)
                {
                    i--;
                }

                TopWindow.Draw(i);
                key = Console.ReadKey(false).Key;
            }
            while (key != ConsoleKey.Escape);

            //Console.ReadKey(true);

            //Console.WriteLine("Удаляю");
            //Command.Delete (DirTo);
            //Command.Delete(DirTo2);

        }
    }
}
