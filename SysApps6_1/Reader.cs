using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysApps6_1
{
    internal class Reader
    {
        private static Semaphore semaphore = new Semaphore(3, 3);

        private Thread thread;
        private int count = 3;

        public delegate void ReaderMessageEvent(Reader reader, string message);
        public event ReaderMessageEvent ReaderMessage;

        public delegate void ReadCompleteEvent(Reader reader);
        public event ReadCompleteEvent ReadCompleted;
        public Reader(int id)
        {
            thread = new Thread(Read);
            thread.Name = "Reader " + id;
            thread.Start();
        }
        public override string ToString()
        {
            return thread.Name;
        }

        public string ReaderName => thread.Name;
        public void Read()
        {
            while (count > 0)
            {
                if (semaphore.WaitOne(200))
                {
                    //Console.WriteLine($"{thread.Name} входит в библиотеку");
                    ReaderMessage?.Invoke(this, "входит в библиотеку");
                    Thread.Sleep(400);
                    //Console.WriteLine($"{thread.Name} читает");
                    ReaderMessage?.Invoke(this, "читает");
                    Thread.Sleep(1000);
                    //Console.WriteLine($"{thread.Name} покидает библиотеку");
                    ReaderMessage?.Invoke(this, "покидает библиотеку");

                    semaphore.Release();
                    count--;
                    Thread.Sleep(1000);
                }
                else
                {
                    ReaderMessage?.Invoke(this, "вышел таймаут ожидания");
                    Thread.Sleep(500);
                }
            }

            ReadCompleted?.Invoke(this);
        }
    }
}
