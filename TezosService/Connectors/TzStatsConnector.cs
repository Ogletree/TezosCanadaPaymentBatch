using System;
using System.Net.Http;
using System.Reflection;
using log4net;
using TezosService.Model.Mine;
using TezosService.Model.TzScan;

namespace TezosService.Connectors
{
    public class TzStatsConnector
    {
        private readonly BakeryConfig _baker;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string Url = "https://api6.tzscan.io/v3/";

        public TzStatsConnector(BakeryConfig baker)
        {
            _baker = baker;
        }

        public static Head GetHead()
        {
            var client = new HttpClient {BaseAddress = new Uri(Url)};
            var response = client.GetAsync("head").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var head = Head.FromJson(result);
                return head;
            }

            Log.Info($"{(int) response.StatusCode} ({response.ReasonPhrase})");
            client.Dispose();
            return null;
        }
    }
}