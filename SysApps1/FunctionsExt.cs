using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SysApps1
{
    internal static class FunctionsExt
    {
        #region constants
        // To indicate the buttons displayed in the message box, specify one of the following values.
        public const int MB_OK = 0x00000000;
        public const int MB_OKCANCEL = 0x00000001;
        public const int MB_YESNO = 0x00000004;

        // To display an icon in the message box, specify one of the following values.
        public const int MB_ICONEXCLAMATION = 0x00000030;
        public const int MB_ICONWARNING = 0x00000030;
        public const int MB_ICONINFORMATION = 0x00000040;
        public const int MB_ICONQUESTION = 0x00000020;
        public const int MB_ICONHAND = 0x00000010;

        // Controls how the window is to be shown
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOW = 5;
        #endregion


        #region functions
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "MessageBox")]
        public static extern int MessageBoxUnicode(IntPtr hWnd, string text, string caption, uint type);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        #endregion
    }
}
