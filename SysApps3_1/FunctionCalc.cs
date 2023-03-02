using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysApps3_1
{
    internal class FunctionCalc
    {
        private double startX1, startX2;
        private uint count;
        private List<double> y;
        public double StartX1
        {
            get => startX1;
            set
            {
                if (value != startX1) startX1 = value;
            }
        }

        public double StartX2
        {
            get => startX2;
            set
            {
                if (value != startX2) startX2 = value;
            }
        }
        public List<double> Y => y;
        public uint Count
        {
            get=> count;
            set
            {
                if (value != count) count = value;
            }
        }

        public FunctionCalc()
        {
            startX1 = startX2 = 0;
            count = 1;
            y = new List<double>();
        }

        public event Action<FunctionCalc, string> StartCalculation;
        public event Action<FunctionCalc, string> EndCalculation;
        public void Calculate()
        {
            StartCalculation?.Invoke(this, "Start calculation");
            double f;
            for (int i = 0; i < count; i++)
            {
                f = Math.Pow((1 - startX1), 2) + 100 * Math.Pow((startX2 - startX1 * startX1), 2);
                startX1 += 0.2;
                startX2 += 0.3;
                y.Add(f);
                Thread.Sleep(10);
            }
            EndCalculation?.Invoke(this, "End calculation");
        }
    }
}
