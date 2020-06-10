using System.Collections.Generic;
using Newtonsoft.Json;

namespace TezosService.Model.TzScan
{
    public class DelegatorsBalance
    {
        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("balance")]
        public long Balance { get; set; }

        public decimal GetFee()
        {
            //TODO: This does not belong here...
            //var discount = new List<string> { "KT1MhbE8s8VRrU3B7JEYmxnUD9oR3zJLDZdL", "KT1Tq3ZpWUzN4b3LxeqEpve7hSx6UzvFtkAC", "KT1F53SuT5ore7Zc7ANqQ2ABFBHuoE2GQqZC", "KT1TzSitJ71n8EU6iZzbZaMm3nzg4gjrfraV", "KT1PVKTkpmuUvscXHUc5UhzobBDq9L7QZMEj" };
            //return discount.Contains(Account.Tz) ? .95m : .90m;
            return .91m;
        }
    }
}