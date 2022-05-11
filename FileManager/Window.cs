using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    internal class Window
    {
        private readonly int x;
        private readonly int y;
        private readonly int width;
        private readonly int height;
        private string[] data;
        private int pageTotal;
        private int page;
        private readonly bool showFooter;

        internal Window(int x, int y, int width, int height, StringBuilder data, bool showFooter = false)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.data = data.ToString().Split('\n');
            this.showFooter = showFooter;
            pageTotal = (this.data.Length + height - 3) / (height - 2);
            page = 1;
        }

        internal void DataUpdate (StringBuilder data)
        {
            this.data = data.ToString().Split('\n');
            pageTotal = (this.data.Length + height - 3) / (height - 2);
            page = 1;
        }

        internal void Draw(int page)
        {
            this.page = page;
            Draw();
        }

        internal void Draw()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(x, y);
            Console.Write("╔");

            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("═");
            }

            Console.Write("╗");

            Console.SetCursorPosition(x, y + 1);

            for (int i = 0; i < height - 2; i++)
            {
                Console.Write("║");
                for (int j = x + 1; j < x + width - 1; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("║");
            }

            Console.Write("╚");

            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("═");
            }

            Console.Write("╝");
            Console.SetCursorPosition(x, y);

            if (showFooter)
            {
                var footer = $"╡ {page} of {pageTotal} ╞";
                Console.SetCursorPosition(width / 2 - footer.Length / 2, height - 1);
                Console.Write(footer);
            }

            Console.SetCursorPosition(0, 1);

            for (int i = (this.page - 1) * height; i < (this.page * height) - 2; i++)
            {
                if (i > data.Length - 1)
                {
                    break;
                }
                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                if (data[i].Length > width - 2)
                {
                    Console.WriteLine(data[i].Remove(width + 2) + "..");
                }
                else
                {
                    Console.WriteLine(data[i]);
                }
            }
        }
    }
}
