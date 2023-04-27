using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysApps9_2
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            await Console.Out.WriteLineAsync("Start Main");
            await PrintAsync("Hello World!");
            await Console.Out.WriteLineAsync("End Main, press Enter key");
            Console.ReadLine();
        }

        private static async void Print(string s)
        {
            await Task.Delay(2000);
            await Console.Out.WriteLineAsync(s);

        }

        private static async Task PrintAsync(string s)
        {
            Console.WriteLine("Start PrintAsync");
            await Task.Run(() => Print(s));
            Console.WriteLine("End PrintAsync");
        }
    }
}
