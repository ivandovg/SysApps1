using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysApps6_1
{
    internal class Program6_1
    {
        /*
        задача: есть некоторое число читателей, которые приходят в
        библиотеку три раза в день и что-то там читают. И пусть у нас будет ограничение, что
        единовременно в библиотеке не может находиться больше трех читателей.
         */

        static int readerCount;
        static void Main(string[] args)
        {
            Console.Title = "Test Semaphore";
            Console.ForegroundColor = ConsoleColor.White;
            readerCount = 5;
            for (int i = 0; i < 5; i++)
            {
                Reader reader = new Reader(i + 1);
                reader.ReaderMessage += Reader_ReaderMessage;
                reader.ReadCompleted += Reader_ReadCompleted;
            }
            Console.WriteLine("Press Enter key...");
            Console.ReadLine();
        }

        private static void Reader_ReadCompleted(Reader reader)
        {
            Console.WriteLine($"{reader.ReaderName} - завершил чтение на сегодня");
            int value = Interlocked.Decrement(ref readerCount);
            if (value == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nВсе читатели использовали свои попытки");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void Reader_ReaderMessage(Reader sender, string message)
        {
            Console.WriteLine($"{sender.ReaderName} - {message}");
        }
    }
}
