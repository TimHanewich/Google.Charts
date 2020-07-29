using System;
using Google.Charts;
using System.Collections.Generic;
using System.IO;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\TaHan\\Downloads\\TestChart.png";

            List<float> Vals = new List<float>();
            Vals.Add(10);
            Vals.Add(40);
            Vals.Add(20);
            Vals.Add(15);
            Vals.Add(60);
            
            LineChartRequest lcr = new LineChartRequest();
            lcr.LoadData(Vals.ToArray());
            lcr.ShowAxes = true;
            lcr.AxisX.Minimum = 0;
            lcr.AxisX.Maximum = 10;
            lcr.AxisY.Minimum = 5;
            lcr.AxisY.Maximum = 15;

            Stream s = lcr.DownloadChartAsync().Result;

            Stream tocopyto = System.IO.File.OpenWrite(path);
            s.Seek(0, SeekOrigin.Begin);
            s.CopyTo(tocopyto);
            s.Dispose();
            tocopyto.Dispose();


        }
    }
}
