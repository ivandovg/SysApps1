using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysApps5_1
{
    internal class Program5_1
    {
        private static object lockObject = new object();
        private static Mutex mutex;
        private static Mutex extMutex;
        static void Main(string[] args)
        {
            Console.Title = "Sync Test";
            string assemblyName = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            bool created;
            extMutex = new Mutex(true, assemblyName, out created);
            if (created)
            {
                Console.WriteLine("This is a first!");
            }
            else
            {
                Console.WriteLine("App is Worked! Press Enter...");
                Console.ReadLine();
                return;
            }

            MakeAndRunThreads();
            
            Console.WriteLine("Press Enter key...");
            Console.ReadLine();
        }

        private static void MakeAndRunThreads()
        {
            // создание и инициализация мьютекса
            //mutex = new Mutex(false);
            mutex = new Mutex();

            Counter.value = 0;
            Thread[] threads = new Thread[5];
            Console.WriteLine("Create Threads");
            for (int i = 0; i < threads.Length; i++)
            {
                //threads[i] = new Thread(CounterIncrement);
                //threads[i] = new Thread(CounterIncrementMonitor);
                //threads[i] = new Thread(CounterIncrementInterlocked);
                threads[i] = new Thread(CounterIncrementMutex);
            }

            Console.WriteLine("Start Threads");
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }
            Thread.Sleep(1000);

            // освободить мьютекс
            // mutex.ReleaseMutex();
            Thread.Sleep(3000);
            Console.WriteLine("Wait threads...");
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine("All Threads finished, Value = " + Counter.value);
        }
        private static void CounterIncrementMutex()
        {
            Console.WriteLine($"Enter Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
            // установка блокировки или перевод мьютекса в "несигнальное" состояние

            mutex.WaitOne();
            try
            {
                Console.WriteLine($"Start Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 1000000; i++)
                {
                    Counter.value++;
                    //Console.WriteLine($"Work Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
                    //Thread.Sleep(200);
                }
            }
            finally
            {
                // снятие блокировки или перевод мьютекса в "сигнальное" состояние
                mutex.ReleaseMutex();
            }

            Console.WriteLine($"End Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void CounterIncrementInterlocked()
        {
            Console.WriteLine($"Enter Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Start Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 10; i++)
            {
                Interlocked.Increment(ref Counter.value);
                Console.WriteLine($"Work Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(200);
            }

            Console.WriteLine($"End Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void CounterIncrement()
        {
            Console.WriteLine($"Enter Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
            // установка блокировки
            lock (lockObject)
            {
                Console.WriteLine($"Start Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 1000000; i++)
                {
                    Counter.value++;
                }
            }
            Console.WriteLine($"End Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void CounterIncrementMonitor()
        {
            Console.WriteLine($"Enter Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");

            // блокируем ресурс, если он доступен
            Monitor.Enter(lockObject);
            try
            {
                Console.WriteLine($"Start Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 1000000; i++)
                {
                    Counter.value++;
                    Thread.Sleep(1000);
                    //throw new Exception("Test");
                    //return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERR: " + ex.Message);
            }
            finally
            {
                // освобождаем ресурс, снимаем блокировку
                Monitor.Exit(lockObject);
            }
            Console.WriteLine($"End Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
        }
    }

    internal class Counter
    {
        public static int value;
    }
}
