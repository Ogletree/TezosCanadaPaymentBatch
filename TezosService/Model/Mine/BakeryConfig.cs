namespace TezosService.Model.Mine
{
    public class BakeryConfig
    {
        public string Account { get; set; }
        public string BurnerKt1 { get; set; }
        public string Node { get; set; }
        public int GasLimit { get; set; }
        public int Storage { get; set; }
        public int NetworkFee { get; set; }
    }
}