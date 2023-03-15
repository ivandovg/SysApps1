using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysApps7_1
{
    internal class Program7_1
    {
        static void Main(string[] args)
        {
            Console.Title = "Task Test";
            // Test1Tasks();
            // Test2Tasks();
            // Test3Tasks();
            // Test4TasksContinue();
            // Test5TasksCanceled();
            // Test6TasksCanceled();
            // Test7TasksCanceled();

            //Console.WriteLine("использование Parallel.For>> ");
            //Parallel.For(1, 10, FactorialParallel);

            Console.WriteLine("\nиспользование Parallel.ForEach>> ");
            ParallelLoopResult result = Parallel.ForEach<int>(
                new List<int>() { 1, 3, 5, 8, 7, 11 },
                FactorialParallel);
            if (result.IsCompleted)
            {
                Console.WriteLine("Цикл выполнил все итерации");
            }
            else
            {
                Console.WriteLine("Цикл прерван на итерации #" + result.LowestBreakIteration);
            }
            // тест использования паралельных запросов Parallel LINQ
            Test1PLINQ();

            Console.WriteLine("Press Enter key...");
            Console.ReadLine();
        }

        private static void Test1Tasks()
        {
            // #1 - создание задачи
            // Task task = new Task(CalcSum);
            // запуск задачи
            // task.Start();

            // #2 - создание задачи
            // Task task = Task.Run(CalcSum);

            // #3 - создание задачи
            Task task = Task.Factory.StartNew(CalcSum);

            Console.WriteLine("\nMain continue...");
            
            int sum = random.Next(10, 100);
            for (int i = 0; i < 10; i++)
            {
                sum += i;
                Thread.Sleep(100);
            }

            Console.WriteLine("Wait task finished...");
            task.Wait();

            Console.WriteLine("Main end, Sum = " + sum);
        }
        private static void Test2Tasks()
        {
            Console.WriteLine("Create tasks");
            // запуск нескольких задач
            Task[] tasks = new Task[5];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(CalcSum);
            }
            Console.WriteLine("\nMain continue...");

            int sum = random.Next(10, 100);
            for (int i = 0; i < 10; i++)
            {
                sum += i;
                Thread.Sleep(100);
            }

            Console.WriteLine("Wait task finished...");
            Task.WaitAll(tasks);

            Console.WriteLine("Main end, Sum = " + sum);
        }
        private static Random random = new Random(1000);
        private static void CalcSum()
        {
            Console.WriteLine("Start TaskId = " + Thread.CurrentThread.ManagedThreadId);
            int sum = random.Next(10, 100);
            for (int i = 0; i < 10; i++)
            {
                sum += i;
                Thread.Sleep(500);
            }
            Console.WriteLine($"Stop TaskId = {Thread.CurrentThread.ManagedThreadId}, Sum = {sum}");
        }

        private static void Test3Tasks()
        {
            do
            {
                Console.Write("Enter number (0 - Exit) = ");
                int n = int.Parse(Console.ReadLine());
                if (n == 0)
                    break;

                Task<int> task = new Task<int>(() => Factorial(n));
                task.Start();
                Task.Run(() =>
                {
                    Console.WriteLine("Wait calculation...");
                    Console.WriteLine($"{n}! = {task.Result}");
                });
            } while (true);
        }

        private static int Factorial(int x)
        {
            Thread.Sleep(200);
            if (x == 1) return 1;
            else return x * Factorial(x - 1);
        }

        private static void Test4TasksContinue()
        {
            List<int> ints = new List<int>();
            Task task1 = new Task(() =>
            {
                Console.WriteLine($"TaskId {Task.CurrentId}. Generate numbers");
                for (int i = 0; i < 20; i++)
                {
                    ints.Add(random.Next(100, 1000));
                    Thread.Sleep(100);
                }
                Console.WriteLine($"TaskId {Task.CurrentId}. End");
            });

            Task task2 = task1.ContinueWith((t) =>
            {
                Console.WriteLine($"TaskId {Task.CurrentId}. Print numbers");
                for (int i = 0; i < ints.Count; i++)
                {
                    Console.Write("\t" + ints[i]);
                }
                Console.WriteLine($"\nTaskId {Task.CurrentId}. End");
            });

            Task task3 = task2.ContinueWith((t) =>
            {
                Console.WriteLine($"TaskId {Task.CurrentId}. Calculate sum");
                int sum = 0;
                for (int i = 0; i < ints.Count; i++)
                {
                    sum += ints[i];
                }
                Console.WriteLine($"TaskId {Task.CurrentId}. End, Sum = {sum}");
            });

            Task task4 = task3.ContinueWith((t) =>
            {
                Console.WriteLine($"TaskId {Task.CurrentId}. Calculate sum");
                double avg = 0;
                for (int i = 0; i < ints.Count; i++)
                {
                    avg += ints[i];
                }
                Console.WriteLine($"TaskId {Task.CurrentId}. End, Avg = {avg / ints.Count}");
            });

            task1.Start();

            Console.WriteLine("All tasks start, wait completed...");
            task4.Wait();
            Console.WriteLine("\nAll task is finished!!!");
        }

        private static void Test5TasksCanceled()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Run(() =>
            {
                int mult = 1;
                for (int i = 1; i <= 20; i++)
                {
                    mult *= i;
                    Thread.Sleep(200);
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("\nCancellationRequested, stop task");
                        return;
                    }
                    Console.WriteLine("\nMult = " + mult);
                    Thread.Sleep(200);
                }
            }, token);

            Thread.Sleep(1000);
            cancellationTokenSource.Cancel();
            task.Wait();
            Console.WriteLine($"Task status = {task.Status}");
            cancellationTokenSource.Dispose();
        }

        private static void Test6TasksCanceled()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Run(() =>
            {
                int mult = 1;
                for (int i = 1; i <= 20; i++)
                {
                    mult *= i;
                    Thread.Sleep(200);
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("\nCancellationRequested, stop task");
                        // генерируем исключение при поступлении запроса на отмену задачи
                        token.ThrowIfCancellationRequested();
                    }
                    Console.WriteLine("\nMult = " + mult);
                    Thread.Sleep(200);
                }
            }, token);

            Thread.Sleep(1000);
            try
            {
                cancellationTokenSource.Cancel();
                task.Wait();
            }
            catch(AggregateException ae)
            {
                foreach (Exception ex in ae.InnerExceptions)
                {
                    if(ex is TaskCanceledException)
                        Console.WriteLine("Task is canceled");
                    else
                        Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine($"Task status = {task.Status}");
                cancellationTokenSource.Dispose();
            }
        }

        private static void Test7TasksCanceled()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Factory.StartNew(()=> MultNumbers(token), token);

            Thread.Sleep(1000);
            cancellationTokenSource.Cancel();
            task.Wait();
            Console.WriteLine($"Task status = {task.Status}");
            cancellationTokenSource.Dispose();
        }

        private static void MultNumbers(CancellationToken token)
        {
            int mult = 1;
            for (int i = 1; i <= 20; i++)
            {
                mult *= i;
                Thread.Sleep(200);
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("\nCancellationRequested, stop task");
                    return;
                }
                Console.WriteLine("\nMult = " + mult);
                Thread.Sleep(200);
            }
        }

        private static void FactorialParallel(int x, ParallelLoopState parallelLoopState)
        {
            int result = 1;
            if (x == 5) parallelLoopState.Break();
            //if (x == 5) parallelLoopState.Stop();
            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine("Выполняется задача {0}", Task.CurrentId);
            Console.WriteLine("Факториал числа {0} равен {1}", x, result);
            Thread.Sleep(2000);
        }

        private static void Test1PLINQ()
        {
            //int[] ints = { 4, 6, 7, 2, 8, 9, 3, 12, 9 };
            int[] ints = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var factorials = (from n in ints.AsParallel()
            //                  select Factorial(n));
            //foreach (int n in factorials)
            //{
            //    Console.Write(n+"\t");
            //}

            //ints.AsParallel().Select(n => Factorial(n)).ForAll(Console.WriteLine);
            //ints.AsParallel().Select(n => FactorialCycle(n)).ForAll(Console.WriteLine);
            ints.AsParallel().Select(n => FactorialCycle(n)).ForAll(f =>
            {
                Console.WriteLine("\t! = " + f);
            });
        }

        private static int FactorialCycle(int x)
        {
            Thread.Sleep(200);
            int n = 1;
            for (int i = 1; i < x; i++)
            {
                n *= i;
            }
            Console.WriteLine($"End ThreadId = {Thread.CurrentThread.ManagedThreadId}");
            return n;
        }
    }
}
