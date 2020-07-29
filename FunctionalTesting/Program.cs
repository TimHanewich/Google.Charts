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
            
            ChartRequest cr = new ChartRequest();
            cr.AxisX.Minimum = 1;
            cr.AxisX.Maximum = 5;
            cr.ShowAxes = false;
            cr.AxisX.Label = "Hello";
            cr.AxisY.Label = "der";
            cr.LoadData(Vals.ToArray());

            Stream s = cr.DownloadChartAsync().Result;
            Stream tocopyto = System.IO.File.OpenWrite(path);
            s.Seek(0, SeekOrigin.Begin);
            s.CopyTo(tocopyto);
            s.Dispose();
            tocopyto.Dispose();


        }
    }
}
