using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net;

namespace TimHanewich.Google.Charts
{
    public class BarChartRequest : StandardChartRequest, IChartRequestComponent, IDownloadableChart
    {
        public new KeyValuePair<string, string>[] GenerateFormContent()
        {
            //The paramaters i have to worry about in this class
            //cht - chart type

            List<KeyValuePair<string, string>> KVPs = new List<KeyValuePair<string, string>>();

            //Add the chart type
            KVPs.Add(new KeyValuePair<string, string>("cht", "bvs"));

            //Add the paramaters from my base class
            KVPs.AddRange(base.GenerateFormContent());


            return KVPs.ToArray();

        }
    
        public async Task<Stream> DownloadChartAsync()
        {
            //Get content to request
            KeyValuePair<string, string>[] KVPs = GenerateFormContent();

            FormUrlEncodedContent content = new FormUrlEncodedContent(KVPs);
            HttpRequestMessage req = new HttpRequestMessage();
            req.Method = HttpMethod.Post;
            req.Content = content;
            req.RequestUri = new Uri(ChartRequest.RequestEndpoint);

            //Make the request
            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.SendAsync(req);

            //Return the stream
            Stream s = await hrm.Content.ReadAsStreamAsync();
            return s;
             
        }
    }
}