using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace Google.ImageCharts
{
    public interface IDownloadableChart
    {
        Task<Stream> DownloadChartAsync();
    }
}