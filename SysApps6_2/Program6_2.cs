using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysApps6_2
{
    internal class Program6_2
    {
        private static ManualResetEvent mre;
        private static AutoResetEvent are;
        static void Main(string[] args)
        {
            Console.Title = "Test Event SyncMethod ";

            //TestManualReset();
            TestAutoResetEvent();

            Console.WriteLine("Press Enter key...");
            Console.ReadLine();
        }
        #region Test ManualResetEvent
        private static void TestManualReset()
        {
            mre = new ManualResetEvent(false);
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(CounterIncrement);
                threads[i].Name = "Counter " + i;
                threads[i].Start();
            }
            mre.Set();
            Thread.Sleep(5000);
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine("Value = " + Counter.value);
        }

        private static void CounterIncrement()
        {
            Console.WriteLine($"Enter Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
            // ожидаем освобождение ресурса
            mre.WaitOne();
            // установка блокировки, "ручной сброс"
            mre.Reset();
            Console.WriteLine($"Start Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 1000000; i++)
            {
                Counter.value++;
            }
            mre.Set();
            Console.WriteLine($"End Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
        }
        #endregion


        #region Test AutoResetEvent
        private static void TestAutoResetEvent()
        {
            are = new AutoResetEvent(true);
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(CounterIncrementAuto);
                threads[i].Name = "Counter " + i;
                threads[i].Start();
            }

            Thread.Sleep(5000);
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine("Value = " + Counter.value);
        }

        private static void CounterIncrementAuto()
        {
            Console.WriteLine($"Enter Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
            // ожидаем освобождение ресурса
            are.WaitOne();
            // установка блокировки, "ручной сброс"
            // are.Reset();
            Console.WriteLine($"Start Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 1000000; i++)
            {
                Counter.value++;
            }
            are.Set();
            Console.WriteLine($"End Counter, Thread = {Thread.CurrentThread.ManagedThreadId}");
        }
        #endregion
    }

    internal class Counter
    {
        public static int value;
    }
}
