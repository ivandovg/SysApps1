using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysApps9_1
{
    public partial class MainForm9_1 : Form
    {
        // сообщение нажатия клавиши
        private const int WM_KEYDOWN = 0x0100;

        // дескриптор установленого хука
        private IntPtr hook = IntPtr.Zero;

        public MainForm9_1()
        {
            InitializeComponent();
            FormClosing += MainForm9_1_FormClosing;
        }

        private void MainForm9_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hook != IntPtr.Zero)
                ExtClasses.UnhookWindowsHookEx(hook);
        }

        private void btnSetHook_Click(object sender, EventArgs e)
        {
            hook = ExtClasses.SetHook(HookProcedure, ExtClasses.HookType.WH_KEYBOARD_LL);
            if (hook != IntPtr.Zero)
            {
                btnSetHook.Enabled = false;
                btnUnsetHook.Enabled = true;
                lsbHistory.Items.Insert(0, "Hook setup!");
            }
        }

        private void btnUnsetHook_Click(object sender, EventArgs e)
        {
            if (hook != IntPtr.Zero)
            {
                ExtClasses.UnhookWindowsHookEx(hook);
                hook = IntPtr.Zero;

                btnSetHook.Enabled = true;
                btnUnsetHook.Enabled = false;
                lsbHistory.Items.Insert(0, "Hook destroy!");
            }
        }

        private IntPtr HookProcedure(int code, IntPtr wParam, IntPtr lParam)
        {
            if ((code >= 0) && wParam == (IntPtr)WM_KEYDOWN)
            {
                // прочитать виртуальный код нажатой клавиши
                int vkCode = Marshal.ReadInt32(lParam);

                // если это кнопка RWin/LWin, блокировать клавишу
                if(((Keys)vkCode == Keys.RWin) || ((Keys)vkCode == Keys.LWin))
                {
                    // вывести сообщение в листбокс
                    Action action = () =>
                    {
                        lsbHistory.Items.Insert(0, "Lock key: " + ((Keys)vkCode));
                    };
                    Invoke(action);

                    // сообщаем что мы обработали сообщение, не передаем далее по цепочке
                    return (IntPtr)1;
                }
            }

            return ExtClasses.CallNextHookEx(hook, code, wParam, lParam);
        }
    }
}
