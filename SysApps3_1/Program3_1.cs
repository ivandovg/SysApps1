using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SysApps3_1
{
    internal class Program3_1
    {
        static void Main(string[] args)
        {
            //TestThread();
            //TestThreadFactorial();
            CalcuteFunction();
            Console.WriteLine("Main Thread End");
        }

        private static void CalcuteFunction()
        {
            Console.Write("Enter first value = ");
            double x1 = double.Parse(Console.ReadLine());
            Console.Write("Enter second value = ");
            double x2 = double.Parse(Console.ReadLine());

            Console.Write("Enter count = ");
            uint count = uint.Parse(Console.ReadLine());
            FunctionCalc function = new FunctionCalc() { 
                StartX1 = x1, StartX2 = x2, Count = count 
            };
            function.StartCalculation += Function_CalculationEvent;
            function.EndCalculation += Function_EndCalculation;
            Thread thread = new Thread(function.Calculate);
            thread.Start();
            Console.WriteLine("\nWait Calculation...");
            //thread.Join();
            //Console.WriteLine("\nList values:");
            //for (int i = 0; i < function.Y.Count; i++)
            //{
            //    Console.WriteLine($"Y{i} = {function.Y[i]}");
            //}
            Console.WriteLine("\n\nPress Enter key...");
            Console.ReadLine();
        }

        private static void Function_EndCalculation(FunctionCalc function, string message)
        {
            Console.WriteLine("\n\t" + message);
            Console.WriteLine("\nList values:");
            for (int i = 0; i < function.Y.Count; i++)
            {
                Console.WriteLine($"Y{i} = {function.Y[i]}");
            }
        }

        private static void Function_CalculationEvent(FunctionCalc function, string message)
        {
            Console.WriteLine("\n\t" + message);
        }

        private static void TestThread()
        {
            //Thread thread = new Thread(new ThreadStart(Calculate));
            Thread calculateThread = new Thread(Calculate);
            calculateThread.IsBackground = true;
            calculateThread.Start();
            Console.WriteLine("TestThread continue working....");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\nTestThread, Value = {i}");
                Thread.Sleep(100);
                if (i == 3)
                {
                    calculateThread.Suspend();
                    Console.WriteLine("\nCalculateThread is Suspended!");
                }
                if (i == 7)
                {
                    calculateThread.Resume();
                    Console.WriteLine("\nCalculateThread is Resumed!");
                }
            }
            Console.WriteLine("\nWait CalculateThread...");
            calculateThread.Join();
            Console.WriteLine("\nTestThread End!");
        }
        private static void Calculate()
        {
            Console.WriteLine("\n\tStart CalculateThread");
            int a = 1, b = 1, x;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\t\tCalculateThread Value = {a + b}");
                Thread.Sleep(200);
                x = a;
                a = b;
                b = x + a;
            }
            Console.WriteLine("\n\tStop CalculateThread");
        }

        private static void TestThreadFactorial()
        {
            Console.Write("Enter n = ");
            int n = int.Parse(Console.ReadLine());
            //Thread factorial = new Thread(new ParameterizedThreadStart(CalculateFactorial));
            Thread factorial = new Thread(CalculateFactorial);
            factorial.Start(n);
            Console.WriteLine("\nWait CalculateFactorial...");
            factorial.Join();
            Console.WriteLine("\nTestThreadFactorial End! Press Enter key...");
            Console.ReadLine();
        }

        private static void CalculateFactorial(object value)
        {
            Console.WriteLine("\n\tStart CalculateFactorial");
            int a = 1, n;

            n = (int)value;

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\t\tCalculateFactorial Value = {a}");
                a *= (i + 1);
                Thread.Sleep(200);
            }
            Console.WriteLine("\n\tStop CalculateFactorial");
        }
    }
}
