using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace Google.Charts
{
    public interface IDownloadableChart
    {
        HttpRequestMessage CreateHttpRequestMessage();
        Task<Stream> DownloadChartAsync();
    }
}