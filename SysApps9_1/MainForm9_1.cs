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
using static SysApps9_1.ExtClasses;

namespace SysApps9_1
{
    public partial class MainForm9_1 : Form
    {
        // дескриптор установленого хука
        private IntPtr hookKeyboard = IntPtr.Zero;
        private IntPtr hookMouse = IntPtr.Zero;

        public MainForm9_1()
        {
            InitializeComponent();
            //ShowInTaskbar = false;
            FormClosing += MainForm9_1_FormClosing;
        }

        private void MainForm9_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hookKeyboard != IntPtr.Zero)
                UnhookWindowsHookEx(hookKeyboard);

            if (hookMouse != IntPtr.Zero)
                UnhookWindowsHookEx(hookMouse);
        }

        private void btnSetHook_Click(object sender, EventArgs e)
        {
            //Task.Run(() =>
            //{
            //    hookKeyboard = SetHook(HookProcedure, HookType.WH_KEYBOARD_LL);
            //    if (hookKeyboard != IntPtr.Zero)
            //    {
            //        Action a = () =>
            //        {
            //            btnSetHook.Enabled = false;
            //            btnUnsetHook.Enabled = true;
            //            lsbHistory.Items.Insert(0, "Keybord Hook Setup!");
            //        };
            //        Invoke(a);
            //    }
            //});

            hookKeyboard = SetHook(HookProcedure, HookType.WH_KEYBOARD_LL);
            if (hookKeyboard != IntPtr.Zero)
            {
                btnSetHook.Enabled = false;
                btnUnsetHook.Enabled = true;
                lsbHistory.Items.Insert(0, "Keybord Hook Setup!");
            }
        }

        private void btnUnsetHook_Click(object sender, EventArgs e)
        {
            if (hookKeyboard != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hookKeyboard);
                hookKeyboard = IntPtr.Zero;

                btnSetHook.Enabled = true;
                btnUnsetHook.Enabled = false;
                lsbHistory.Items.Insert(0, "Keybord Hook destroy.");
            }
        }

        private IntPtr HookProcedure(int code, IntPtr wParam, IntPtr lParam)
        {
            if ((code >= 0) && wParam == (IntPtr)WM_KEYDOWN)
            {
                // прочитать виртуальный код нажатой клавиши
                //int vkCode = Marshal.ReadInt32(lParam);

                KBDLLHOOKSTRUCT kbd = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                // если это кнопка RWin/LWin, блокировать клавишу
                if (((Keys)kbd.vkCode == Keys.RWin) || ((Keys)kbd.vkCode == Keys.LWin))
                {
                    // вывести сообщение в листбокс
                    Action action = () =>
                    {
                        lsbHistory.Items.Insert(0, "Lock key: " + ((Keys)kbd.vkCode));
                    };
                    Invoke(action);

                    // сообщаем что мы обработали сообщение, не передаем далее по цепочке
                    return (IntPtr)1;
                }
            }

            return CallNextHookEx(hookKeyboard, code, wParam, lParam);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Visible = !Visible;
            if (Visible)
                BringToFront();
        }

        private IntPtr HookProcMouse(int code, IntPtr wParam, IntPtr lParam)
        {
            if ((code >= 0) && wParam == (IntPtr)WM_MOUSEMOVE)
            {
                // получить координаты указателя
                int x = Marshal.ReadInt32(lParam);
                //int y = Marshal.ReadInt32(lParam);
                if (x > 400) return (IntPtr)1;
            }

            return CallNextHookEx(hookMouse, code, wParam, lParam);
        }

        private void btnHookMouse_Click(object sender, EventArgs e)
        {
            hookMouse = SetHook(HookProcMouse, HookType.WH_MOUSE_LL);
            if (hookMouse != IntPtr.Zero)
            {
                btnHookMouse.Enabled = false;
                btnUnhookMouse.Enabled = true;

                lsbHistory.Items.Insert(0, "Mouse Setup Hook!");
            }
        }

        private void btnUnhookMouse_Click(object sender, EventArgs e)
        {

            if (hookMouse != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hookMouse);
                hookMouse = IntPtr.Zero;
                btnHookMouse.Enabled = true;
                btnUnhookMouse.Enabled = false;

                lsbHistory.Items.Insert(0, "Mouse Destroy Hook.");
            }
        }
    }
}
