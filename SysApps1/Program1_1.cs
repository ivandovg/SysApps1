using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysApps1
{
    internal class Program1_1
    {
        static void Main(string[] args)
        {
            Console.Title = "System Applicatins - 1";
            //TestMessageBox();
            //TestShowWindow();
            TestGetindowText();

            Console.WriteLine("press ENTER key...");
            Console.ReadLine();
        }

        static void TestMessageBox()
        {
            // тест функции MessageBox
            int result = FunctionsExt.MessageBoxUnicode(IntPtr.Zero, "Hello world!!!", "Test message",
                FunctionsExt.MB_YESNO | FunctionsExt.MB_ICONQUESTION);

            Console.WriteLine($"result = {result}");

            result = FunctionsExt.MessageBoxUnicode(IntPtr.Zero, "Hello world!!!", "Test message",
                FunctionsExt.MB_OKCANCEL | FunctionsExt.MB_ICONHAND);

            Console.WriteLine($"result = {result}");

            result = FunctionsExt.MessageBoxUnicode(IntPtr.Zero, "Hello world!!!", "Test message",
                FunctionsExt.MB_OK | FunctionsExt.MB_ICONINFORMATION);

            Console.WriteLine($"result = {result}");
        }

        private static void TestShowWindow()
        {
            FunctionsExt.ShowWindow((IntPtr)0x00121000, FunctionsExt.SW_HIDE);
            Console.WriteLine("press ENTER to show window");
            Console.ReadLine();
            FunctionsExt.ShowWindow((IntPtr)0x00121000, FunctionsExt.SW_SHOW);
        }

        private static void TestGetindowText()
        {
            StringBuilder sb = new StringBuilder();
            int h;
            do
            {
                Console.Write("Enter handle: ");
                h = int.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
                int len = FunctionsExt.GetWindowText((IntPtr)h, sb, 1024);
                Console.WriteLine($"String length (answer): {len}\nString: {sb}");
            } while (h != -1);
            //Console.ReadLine();
        }
    }
}
