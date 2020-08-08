using System;
using System.Collections.Generic;

namespace TimHanewich.Google.Charts
{
    public class StandardChartRequest : ChartRequest, IChartRequestComponent
    {
        public AxisSpecification AxisX {get; set;}
        public AxisSpecification AxisY {get; set;}
        public bool ShowAxes {get; set;}
        public float[] Data {get; set;}

        public StandardChartRequest()
        {
            AxisX = new AxisSpecification();
            AxisY = new AxisSpecification();
            ShowAxes = false;
        }

        public void LoadData(float[] data)
        {
            //Get min and max
            float min = float.MaxValue;
            float max = float.MinValue;
            foreach (float val in data)
            {
                if (val < min)
                {
                    min = val;
                }
                
                if (val > max)
                {
                    max = val;
                }
            }
            
            //Condense them
            List<float> condensed = new List<float>();
            foreach (float val in data)
            {
                float aspercentage = (val - min) / (max - min);
                condensed.Add(aspercentage * 100);
            }

            //Set the values
            Data = condensed.ToArray();
            AxisY.Maximum = max;
            AxisY.Minimum = min;
        }
    
        public new KeyValuePair<string, string>[] GenerateFormContent()
        {
            //The paramters we have to worry about in this class
            //chd - chart data
            //chxt - what axises to show
            //chxl - axis labels
            //chxp - axis label positions
            //chxr - axis value range

            List<KeyValuePair<string, string>> KVPs = new List<KeyValuePair<string, string>>();

            #region "Axis Data - chxt, chxr, chxl, chxp"

            if (ShowAxes)
            {
                //Say there are going to be the axis measurements and an axis label too.
                KVPs.Add(new KeyValuePair<string, string>("chxt","x,x,y,y"));

                //Axis ranges
                string ar = "0," + AxisX.Minimum.ToString() + "," + AxisX.Maximum.ToString() + "|" + "2," + AxisY.Minimum.ToString() + "," + AxisY.Maximum.ToString();
                KVPs.Add(new KeyValuePair<string, string>("chxr",ar));

                //Labels
                string xLabel = "?";
                string yLabel = "?";
                if (AxisX.Label != null)
                {
                    if (AxisX.Label != "")
                    {
                        xLabel = AxisX.Label;
                    }
                }
                if (AxisY.Label != null)
                {
                    if (AxisY.Label != "")
                    {
                        yLabel = AxisY.Label;
                    }
                }
                string alspec = "1:|" + xLabel + "|3:|" + yLabel + "|";
                KVPs.Add(new KeyValuePair<string, string>("chxl", alspec));


                //Label positions
                KVPs.Add(new KeyValuePair<string, string>("chxp", "1,50|3,50"));
            
            }

            #endregion
            
            #region "Data  chd"

            //Fill in the data
            string datastr = "";
            foreach (float val in Data)
            {
                datastr = datastr + val + ",";
            }
            datastr = datastr.Substring(0, datastr.Length-1);
            datastr = "t:" + datastr;
            KVPs.Add(new KeyValuePair<string, string>("chd", datastr));

            #endregion

            return KVPs.ToArray();
        }
    }
}