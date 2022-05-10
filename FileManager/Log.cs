using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    internal static class Log 
    {


        public static void Write(Exception Value)

        {
            Console.WriteLine ($"Возникла ошибка {Value.Message}");
        }

    }
}
