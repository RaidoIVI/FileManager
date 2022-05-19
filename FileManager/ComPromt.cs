using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    internal static class ComPromt
    {
        private static List<StringBuilder> comList;

        private static void ProcessEnterCommand(int width)
        {

            StringBuilder command = new StringBuilder();
            int index = comList.Count+1;

            do
            {
                var currentLeft = Console.CursorLeft;
                var currentTop = Console.CursorTop;

                var key = Console.ReadKey();

                switch (key.Key)
                    {
                    case ConsoleKey.Backspace :

                        if (command.Length > 0)
                        {
                            command.Remove(command.Length - 1, 1);
                            Console.SetCursorPosition(currentLeft - 1, currentTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(currentLeft - 1, currentTop);
                        }
                        else
                        {
                            Console.SetCursorPosition(currentLeft,currentTop);
                        }
                        break;

                    case ConsoleKey.Enter:
                        comList.Add(command);
                        ParseCommandString();
                        break;
                    case ConsoleKey.PageUp:

                        break;
                    case ConsoleKey.PageDown:

                        break;
                    case ConsoleKey.Escape:

                        break;
                    case ConsoleKey.UpArrow:

                        break;
                    case ConsoleKey.DownArrow:

                        break;
                    case ConsoleKey.LeftArrow:

                        break;
                    case ConsoleKey.RightArrow:

                        break;
                    default: 
                        command.Append(key.Key);
                        break;
                }

                //(int currentLeft, int currentTop) = GetCursorPosition();

                if (currentLeft == width - 2)
                {
                    Console.SetCursorPosition(currentLeft - 1, top);
                    Console.Write(" ");
                    Console.SetCursorPosition(currentLeft - 1, top);
                }
               
            }
            while (key != (char)13);
            ParseCommandString(command.ToString());

        }

        private static void ParseCommandString()
        {
            throw new NotImplementedException();
        }

        static void ParseCommandString(string command)
        {
            string[] commandParams = command.ToLower().Split(' ');
            if (commandParams.Length > 0)
            {
                switch (commandParams[0])
                {
                    case "cd":
                        if (commandParams.Length > 1 && Directory.Exists(commandParams[1]))
                        {
                            currentDir = commandParams[1];
                        }

                        break;
                    case "ls":
                        if (commandParams.Length > 1 && Directory.Exists(commandParams[1]))
                        {
                            if (commandParams.Length > 3 && commandParams[2] == "-p" && int.TryParse(commandParams[3], out int n))
                            {
                                DrawTree(new DirectoryInfo(commandParams[1]), n);
                            }
                            else
                            {
                                DrawTree(new DirectoryInfo(commandParams[1]), 1);
                            }
                        }
                        break;
                }
            }
            UpdateConsole();
        }

        static void DrawConsole(string dir, int x, int y, int width, int height)
        {
            DrawWindow(x, y, width, height);
            Console.SetCursorPosition(x + 1, y + height / 2);
            Console.Write($"{dir}>");
        }

    }
}
