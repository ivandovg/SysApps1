using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysApps4_1
{
    internal class Program4_1
    {
        static void Main(string[] args)
        {
            Console.Title = "Test ThreadPool";
            // вывод информации про пул потоков
            ThreadPoolInfo();

            // запуск потока с использованием ThreadPool
            Console.WriteLine($"Start Main working ({Thread.CurrentThread.ManagedThreadId})...");

            #region Тест 1 - запуск одного дополнительного потока
            // запускаем в отдельный поток
            //ThreadPool.QueueUserWorkItem(new WaitCallback(CalculateUseThreadPool));
            //Console.Write("Enter number = ");
            //int n = int.Parse(Console.ReadLine());
            //ThreadPool.QueueUserWorkItem(CalculateUseThreadPool, n);
            //Console.WriteLine($"Continue Main working ({Thread.CurrentThread.ManagedThreadId})...");
            //Thread.Sleep(1000);
            //Console.WriteLine($"\nEnd Main working ({Thread.CurrentThread.ManagedThreadId})!");
            #endregion

            #region Тест 2 - запуск несколько потоков
            //Random random = new Random();
            //// количество потоков для запуска
            //int nMax = random.Next(5, 20), n;
            //Console.WriteLine($"\nThreads count = {nMax}");
            //for (int i = 0; i < nMax; i++)
            //{
            //    n = random.Next(5, 10);
            //    ThreadPool.QueueUserWorkItem(CalculateUseThreadPool, n);
            //}

            //Console.WriteLine($"Continue Main working ({Thread.CurrentThread.ManagedThreadId})...");
            //Thread.Sleep(1000);
            //Console.WriteLine($"\nEnd Main working ({Thread.CurrentThread.ManagedThreadId})!");
            #endregion

            #region Тест 3 - синхронные делегаты
            //Console.WriteLine("Sync call Display!");
            //DisplayDelegate display = new DisplayDelegate(Display);
            //// sync call
            //int result = display.Invoke();
            //Console.WriteLine("Result = " + result);
            //Console.WriteLine("End Main!");
            #endregion

            //#region Тест 4 - асинхронные делегаты
            //Console.WriteLine("Async call Display!");
            //DisplayDelegate display = new DisplayDelegate(Display);
            //// async call
            //IAsyncResult asyncResult = display.BeginInvoke(null, null);
            //Console.WriteLine("Continue Main...");
            //int result = display.EndInvoke(asyncResult);
            //Console.WriteLine("Result = " + result);
            //Console.WriteLine("End Main!");
            //#endregion

            #region Тест 5 - асинхронные делегаты с параметрами
            //Console.WriteLine("Async call CalculateSum!");
            //Func<int, int, int> calc = new Func<int, int, int>(CalculateSum);
            //// async call
            //IAsyncResult asyncResult = calc.BeginInvoke(7, 10, null, null);
            //Console.WriteLine("Continue Main...");
            //int result = calc.EndInvoke(asyncResult);
            //Console.WriteLine("Result = " + result);
            //Console.WriteLine("End Main!");
            #endregion

            #region Тест 6 - асинхронные делегаты с параметрами и методом обратного вызова
            Console.WriteLine("Async call CalculateSum!");
            //Func<int, int, int> calc = new Func<int, int, int>(CalculateSum);
            CalculateSumDelegate calc = new CalculateSumDelegate(CalculateSum);
            // async call
            IAsyncResult asyncResult = calc.BeginInvoke(17, 20, AsynCallbackCalculate, calc);
            Console.WriteLine("Continue Main...");
            Thread.Sleep(1000);
            Console.WriteLine("End Main!");
            #endregion

            Console.ReadKey();
        }

        private static void ThreadPoolInfo()
        {
            int w, c;
            ThreadPool.GetMaxThreads(out w, out c);
            Console.WriteLine($"Max Worker Thteads = {w}, Max Completion Port Threads = {c}");
            ThreadPool.GetMinThreads(out w, out c);
            Console.WriteLine($"Min Worker Thteads = {w}, Min Completion Port Threads = {c}");
            ThreadPool.GetAvailableThreads(out w, out c);
            Console.WriteLine($"Available Worker Thteads = {w}, Available Completion Port Threads = {c}\n");

            //ThreadPool.SetMaxThreads(100, 100);
            //ThreadPool.GetMaxThreads(out w, out c);
            //Console.WriteLine($"Max Worker Thteads = {w}, Max Completion Port Threads = {c}");
            //ThreadPool.GetAvailableThreads(out w, out c);
            //Console.WriteLine($"Available Worker Thteads = {w}, Available Completion Port Threads = {c}");
        }

        private static void CalculateUseThreadPool(object state)
        {
            int n;
            try
            {
                //n = int.Parse(state.ToString());
                n = (int)state;
            }
            catch 
            {
                n = 5;
            }
            Console.WriteLine($"\t\tStart another thread, n = {n}, id = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\t\tWork another thread, id = {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(50);
            }
            Console.WriteLine($"\t\tEnd another thread, id = {Thread.CurrentThread.ManagedThreadId}");
            //ThreadPoolInfo();
        }

        private static int CalculateSum(int count, int start)
        {
            Console.WriteLine($"\t\tStart another thread, count = {count}, id = {Thread.CurrentThread.ManagedThreadId}");
            int s = start;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\t\tWork another thread, id = {Thread.CurrentThread.ManagedThreadId}");
                s += i * 5;
                Thread.Sleep(50);
            }
            Thread.Sleep(1000);
            Console.WriteLine($"\t\tEnd another thread, id = {Thread.CurrentThread.ManagedThreadId}");
            return s;
        }

        public delegate int CalculateSumDelegate(int count, int start);
        
        private static void AsynCallbackCalculate(IAsyncResult asyncResult)
        {
            //Func<int, int, int> calc = asyncResult.AsyncState as Func<int, int, int>;
            CalculateSumDelegate calc = asyncResult.AsyncState as CalculateSumDelegate;
            int resultSum = calc.EndInvoke(asyncResult);
            Console.WriteLine($"Result callback = {resultSum}");
        }

        public delegate int DisplayDelegate();
        private static int Display()
        {
            Console.WriteLine();
            int s = 0;
            Random random = new Random();
            for (int i = 0; i < random.Next(20, 40); i++)
            {
                s += i * 10;
            }
            Thread.Sleep(2000);
            return s;
        }
    }
}
