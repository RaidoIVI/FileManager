using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    internal static class UI
    {
        private static int pageTop;
        private static Window topWindow;
        private static Window middleWindow;
        private static Window consoleWindow;

        internal static void Update()
        {
            UpdateTop();
            UpdateMiddle();
            UpdateConsole();
        }

        internal static void Update(ConsoleKey Key)
        {
            switch (Key)
            {
                case ConsoleKey.PageUp:
                    
            }
        }

        private static void UpdateMiddle()
        {
            throw new NotImplementedException();
        }

        private static void UpdateTop()
        {
            throw new NotImplementedException();
        }

        private static void UpdateConsole()
        {
            throw new NotImplementedException();
        }
    }
}
