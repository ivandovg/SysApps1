using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace SysApps2_1
{
    public partial class MainForm2_1 : Form
    {
        public MainForm2_1()
        {
            InitializeComponent();
            dgvProcess.MultiSelect = false;
            dgvProcess.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnGetProcess_Click(object sender, EventArgs e)
        {
            var processes = Process.GetProcesses()
                .Select(p=>new {p.ProcessName, p.MainWindowTitle, p.Id, p.Threads.Count, p.BasePriority, Process = p})
                .OrderBy(p => p.ProcessName).ToList();
            dgvProcess.DataSource = processes;
            
            dgvProcess.Columns["Process"].Visible = false;
        }

        private void dgvProcess_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //
        }

        private void btnKillProcess_Click(object sender, EventArgs e)
        {
            if (dgvProcess.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = dgvProcess.SelectedRows[0];
            Process p = row.Cells["Process"].Value as Process;
            if (p != null)
            {
                p.Kill();
                btnGetProcess_Click(null, null);
            }
        }

        private void btnRunProcess_Click(object sender, EventArgs e)
        {
            dlgOpen.InitialDirectory = "C:";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                //Process process = Process.Start(dlgOpen.FileName);
                
                ProcessStartInfo startInfo = new ProcessStartInfo(dlgOpen.FileName);
                //startInfo.Arguments = "";
                startInfo.UseShellExecute = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                if (Process.GetCurrentProcess().Id == GetParentProcessId(process.Id))
                {
                    process.EnableRaisingEvents = true;
                    process.Exited += Process_Exited;
                    SetChildWindowText(process.MainWindowHandle, "Child Window");
                    //SetChildWindowText(p.Handle, "Child Window");
                    MessageBox.Show("Процесс является дочерним!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Процесс не является дочерним!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            Process process = sender as Process;
            if (process != null)
            {
                MessageBox.Show($"Процесс {process.ProcessName} завершил работу!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Процесс завершил работу!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /*метод, получающий PID родительского процесса (использует WMI)*/
        private int GetParentProcessId(int Id)
        {
            int parentId = 0;
            using (ManagementObject obj = new ManagementObject("win32_process.handle=" + Id.ToString()))
            {
                obj.Get();
                parentId = Convert.ToInt32(obj["ParentProcessId"]);
            }
            return parentId;
        }

        //константа, идентифицирующая сообщение WM_SETTEXT
        const uint WM_SETTEXT = 0x0C;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg
            , uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        private void SetChildWindowText(IntPtr hWnd, string text)
        {
            //SendMessage(hWnd, WM_SETTEXT, 0, text);
            SetWindowText(hWnd, text);
        }
    }
}
