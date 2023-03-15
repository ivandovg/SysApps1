using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysApps8_1
{
    internal class Program8_1
    {
        static void Main(string[] args)
        {
            unsafe
            {
                int d = 12;
                int* p = &d;
                char*[] ch = new char*[100];
            }

            IntPtr ptr;
            unsafe
            {
                int x = 10, y = 20;
                Pow(&x, 6);
                ptr = (IntPtr)x;
            }
            Console.WriteLine("ptr = " + ptr);
            Console.ReadLine();
        }

        unsafe static void Pow(int* x, int n)
        {
            int y = *x;
            for (int i = 0; i < n; i++)
            {
                *x *= y;
            }
        }
    }

    //Небезопасная структура, 
    //внутри которой обьявлены небезопасные поля
    public unsafe struct MyStruct
    {
        public int Val1;
        public unsafe char* Val2;
        public unsafe MyStruct* Val3;
    }

}
