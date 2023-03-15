using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SysApps8_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - write to registry, 2 - read from registry, 3 - delete from registry");
            Console.Write("Select action >> ");
            int action = int.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    {
                        RegistryKey keySoftware = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                        RegistryKey keyMyApp = keySoftware.CreateSubKey("MySystemApps");
                        keyMyApp.SetValue("login", "user1");
                        keyMyApp.SetValue("password", "1234");
                        keyMyApp.SetValue("value", 100, RegistryValueKind.DWord);
                        RegistryKey keySubMyApp = keyMyApp.CreateSubKey("apps");
                        keySubMyApp.SetValue("app1", "appname.exe");
                        keySubMyApp.Close();
                        keyMyApp.Close();
                        keySoftware.Close();
                        break;
                    }
                case 2:
                    {
                        // read
                        using (RegistryKey keyMyApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MySystemApps"))
                        {
                            string login  = keyMyApp.GetValue("login").ToString();
                            string password = keyMyApp.GetValue("password", "").ToString();
                            Console.WriteLine($"Login: {login}");
                            Console.WriteLine($"Password: {password}");
                        }
                        break;
                    }
                case 3:
                    {
                        // delete
                        break;
                    }

                default:
                    Console.WriteLine("Error action, exit");
                    break;
            }

            Console.WriteLine("Press Enter Key...");
            Console.ReadLine();
        }
    }
}
