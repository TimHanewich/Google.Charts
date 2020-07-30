using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace Google.ImageCharts
{
    public class LineChartRequest : ChartRequest, IDownloadableChart
    {
        public AxisSpecification AxisX {get; set;}
        public AxisSpecification AxisY {get; set;}
        public bool ShowAxes {get; set;}
        public float[] Data {get; set;}

        public LineChartRequest()
        {
            AxisX = new AxisSpecification();
            AxisY = new AxisSpecification();
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

        public async Task<Stream> DownloadChartAsync()
        {

            #region "Create Http Request"

            List<KeyValuePair<string, string>> KVPs = new List<KeyValuePair<string, string>>();

            #region "Chart Title"
            //Chart title
            if (Title != null)
            {
                if (Title != "")
                {
                    KeyValuePair<string, string> k1 = new KeyValuePair<string, string>("chtt", Title);
                    KVPs.Add(k1);
                }
            }

            #endregion

            #region "Chart Type"
            //Chart type
            KVPs.Add(new KeyValuePair<string, string>("cht", "lc"));

            #endregion

            #region "Chart Size"

            //Chart size
            KVPs.Add(new KeyValuePair<string, string>("chs", Width.ToString() + "x" + Height.ToString()));
            
            #endregion
            
            #region "Axis Data"


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
            
            #region "Data"
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


            //Prepare the Request message
            FormUrlEncodedContent fuec = new FormUrlEncodedContent(KVPs.ToArray());
            HttpRequestMessage req = new HttpRequestMessage();
            req.RequestUri = new Uri("https://chart.googleapis.com/chart?");
            req.Method = HttpMethod.Post;
            req.Content = fuec;

            #endregion


            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.SendAsync(req);
            if (hrm.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Chart download failed.");
            }
            Stream s = await hrm.Content.ReadAsStreamAsync();
            s.Seek(0, SeekOrigin.Begin);
            return s;
        }

    }
}