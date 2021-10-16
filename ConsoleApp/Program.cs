using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static object locker = new object();
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var task1 = Task.Run(() => { FallingString(1); });
            
            Console.ReadLine();

        }
        public static void RandomString(int lineLength, int leftShift)
        {
            lock (locker)
            {
                var random = new Random();
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                char[] arrayOfChars = Enumerable.Repeat(chars, lineLength).Select(s => s[random.Next(s.Length)]).ToArray();
                arrayOfChars[0] = ' ';
                for (int i = 0; i < arrayOfChars.Length; i++)
                {
                    if (i == arrayOfChars.Length - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.CursorLeft = leftShift;
                        Console.WriteLine(arrayOfChars[i]);
                        Console.ResetColor();
                    }
                    else if (i == arrayOfChars.Length - 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorLeft = leftShift;
                        Console.WriteLine(arrayOfChars[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.CursorLeft = leftShift;
                        Console.WriteLine(arrayOfChars[i]);
                        Console.ResetColor();
                    }
                }
            }
        }
        public static void FallingString(int leftShift)
        {

            while (true)
            {
                int lineLength = new Random().Next(8, 15);
                lock (locker)
                {
                    Console.CursorTop = 0;
                }
                for (int j = 0; j < 30; j++)
                {

                    lock (locker)
                    {
                        Console.CursorTop = j;
                    }
                    lock (locker)
                    {
                        RandomString(lineLength, leftShift);
                    }
                    Thread.Sleep(50);

                }
                lock (locker)
                {
                    Console.Clear();
                }
            }
        }
    }
}