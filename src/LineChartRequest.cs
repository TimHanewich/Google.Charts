using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace TimHanewich.Google.Charts
{
    public class LineChartRequest : StandardChartRequest, IChartRequestComponent, IDownloadableChart
    {

        public new KeyValuePair<string, string>[] GenerateFormContent()
        {
            //The paramaters that this class is responsible is
            //cht - the chart type (for this class it will be lc)

            List<KeyValuePair<string, string>> KVPs = new List<KeyValuePair<string, string>>();

            KVPs.Add(new KeyValuePair<string, string>("cht", "lc"));

            return KVPs.ToArray();
        }

        public async Task<Stream> DownloadChartAsync()
        {

            #region "Create Http Request"

            List<KeyValuePair<string, string>> KVPs = new List<KeyValuePair<string, string>>();

            

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