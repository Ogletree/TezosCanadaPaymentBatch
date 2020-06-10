using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TezosService.Model
{
    public class DelegatePayments
    {
        [Key, Column(Order = 0)]
        public string Account { get; set; }
        [Key, Column(Order = 1)]
        public long Cycle { get; set; }
        public decimal Reward { get; set; }
        public string Paid { get; set; }
    }
}