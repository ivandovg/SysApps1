using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysApps3_2
{
    public partial class MainForm3_2 : Form
    {
        public MainForm3_2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FunctionCalc function = new FunctionCalc()
            {
                StartX1 = (double)numericUpDown1.Value,
                StartX2 = (double)numericUpDown2.Value,
                Count = (uint)numericUpDown3.Value
            };
            function.EndCalculation += Function_EndCalculation;
            btnCalculate.Enabled = false;
            Thread thread = new Thread(function.Calculate);
            lsbValues.Items.Clear();
            thread.Start();
        }

        private void Function_EndCalculation(FunctionCalc function, string message)
        {
            MessageBox.Show("Accept message: " + message);
            Action a = () =>
            {
                lsbValues.BeginUpdate();
                try
                {
                    for (int i = 0; i < function.Y.Count; i++)
                    {
                        lsbValues.Items.Add("Y = " + function.Y[i]);
                    }
                    btnCalculate.Enabled = true;
                }
                finally
                {
                    lsbValues.EndUpdate();
                }
            };

            if (InvokeRequired)
                Invoke(a);
            else
                a();
        }
    }
}
