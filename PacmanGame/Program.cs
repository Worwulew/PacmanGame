using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacmanGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            char[,] map = ReadMap("map.txt");
            bool working = true;
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            Task.Run(() =>
            {
                while (working)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            int pacmanX = 1, pacmanY = 1;
            int score = 0;

            while (working)
            {
                Console.Clear();

                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);

                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write('@');

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(37, 0);
                Console.Write($"Score: {score}");

                Thread.Sleep(500);
            }
        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("map.txt");

            char[,] map = new char[GetMaxLengthOfLines(file), file.Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = file[j][i];
                }
            }
            return map;
        }

        private static void DrawMap(char[,] map)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static void HandleInput(ConsoleKeyInfo key, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(key);

            int nextPacPositionX = pacmanX + direction[0];
            int nextPacPositionY = pacmanY + direction[1];

            char nextCell = map[nextPacPositionX, nextPacPositionY];

            if (nextCell != '#')
            {
                pacmanX = nextPacPositionX;
                pacmanY = nextPacPositionY;
                if (nextCell == '.')
                {
                    score++;
                    map[nextPacPositionX, nextPacPositionY] = ' ';
                }
            }
        }

        private static int[] GetDirection(ConsoleKeyInfo key)
        {
            int[] direction = { 0, 0 };

            switch (key.Key)
            {
                case ConsoleKey.W:
                    direction[1] = -1;
                    break;
                case ConsoleKey.S:
                    direction[1] = 1;
                    break;
                case ConsoleKey.A:
                    direction[0] = -1;
                    break;
                case ConsoleKey.D:
                    direction[0] = 1;
                    break;
            }
            return direction;
        }

        private static int GetMaxLengthOfLines(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
            {
                if (line.Length > maxLength)
                {
                    maxLength = line.Length;
                }
            }
            return maxLength;
        }
    }
}
