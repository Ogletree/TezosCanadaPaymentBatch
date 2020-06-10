using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using log4net;
using TezosService.Model.Mine;
using TezosService.Model.TzScan;

namespace TezosService.Connectors
{
    public class TzScanConnector
    {
        private readonly BakeryConfig _baker;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string Url = "https://api6.tzscan.io/v3/";

        public TzScanConnector(BakeryConfig baker)
        {
            _baker = baker;
        }

        public static Head GetHead()
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };
            var response = client.GetAsync("head").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var head = Head.FromJson(result);
                return head;
            }
            Log.Info($"{(int)response.StatusCode} ({response.ReasonPhrase})");
            client.Dispose();
            return null;
        }
        public static Level GetLevel(string blockHash)
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };
            var response = client.GetAsync($"level/{blockHash}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var level = Level.FromJson(result);
                return level;
            }
            Log.Info($"{(int)response.StatusCode} ({response.ReasonPhrase})");
            client.Dispose();
            return null;
        }
        public static List<Blocks> GetBlocks(int number)
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };
            var response = client.GetAsync($"blocks?number={number}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var blocks = Blocks.FromJson(result).ToList();
                return blocks;
            }
            Log.Info($"{(int)response.StatusCode} ({response.ReasonPhrase})");
            client.Dispose();
            return null;
        }
        public List<BakerRewards> GetBakerRewards()
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };
            var response = client.GetAsync($"rewards_split_cycles/{_baker.Account}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bakerRewards = BakerRewards.FromJson(result);
                return bakerRewards;
            }
            Log.Info($"{(int)response.StatusCode} ({response.ReasonPhrase})");
            client.Dispose();
            return null;
        }

        public List<DelegateRewards> GetDelegateRewards(string account)
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };
            var response = client.GetAsync($"delegator_rewards_with_details/{account}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bakerRewards = DelegateRewards.FromJson(result);
                return bakerRewards;
            }
            Log.Info($"{(int)response.StatusCode} ({response.ReasonPhrase})");
            client.Dispose();
            return null;
        }
        public RewardSplit GetRewardSplitByCycle(long cycle)
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };
            var response = client.GetAsync($"rewards_split/{_baker.Account}?cycle={cycle}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var rewardSplit = RewardSplit.FromJson(result);
                return rewardSplit;
            }
            Log.Info($"{(int)response.StatusCode} ({response.ReasonPhrase})");
            client.Dispose();
            return null;
        }

        public static Blocks GetBlock()
        {
            return GetBlocks(1).FirstOrDefault();
        }

        public static List<Gunk> GetTrans()
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };
            var response = client.GetAsync($"operations/redacted?type=Transaction&p=0&number=20").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var rewardSplit = Gunk.FromJson(result);
                return rewardSplit;
            }
            Log.Info($"{(int)response.StatusCode} ({response.ReasonPhrase})");
            client.Dispose();
            return null;
        }

        public long GetCycleZero()
        {
            var delegateRewards = GetBakerRewards().OrderByDescending(x => x.Cycle)
                .FirstOrDefault(x => x.Status.Status == StatusEnum.RewardsDelivered);
            return delegateRewards.Cycle;
        }
    }
}