using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using log4net;
using Newtonsoft.Json;
using TezosService.Connectors;
using TezosService.Model;
using TezosService.Model.Mine;
using TezosService.Model.TzScan;
using Tz.Net;
using Tz.Net.Extensions;

namespace TezosService
{
    public class ThingDoer
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly BakeryConfig _baker;
        private readonly TzScanConnector _tzScanConnector;

        public ThingDoer(BakeryConfig baker)
        {
            try
            {
                _baker = baker;
                _tzScanConnector = new TzScanConnector(baker);
            }
            catch (Exception e)
            {
                Log.Error("Constructor: " + e.Message);
                Log.Error("Constructor: " + e);
                Singleton.Instance.SendMessage("Constructor: " + e.Message);
                Singleton.Instance.SendMessage("Constructor: " + e);
            }
        }

        public void DoThing()
        {
            do
            {
                try
                {
                    var zero = 154;
                    var json = File.ReadAllText($@"C:\Deployment\DEV\Payments\Cycle{zero}.json");
                    var transactions = JsonConvert.DeserializeObject<List<MyTrans>>(json);

                    //var transactions = GetTransactions(new List<long> {zero}, zero);
                    //var rs = JsonConvert.SerializeObject(transactions, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    //File.WriteAllText($@"C:\Deployment\DEV\Payments\Cycle{zero}.json", rs);

                    //var head = TzScanConnector.GetHead();
                    //var level = TzScanConnector.GetLevel(head.Hash);
                    //var cycleZero = level.Cycle - 6;
                    //var cyclesList = GetCyclesWeNeedToPay(cycleZero);
                    //var transactions = GetTransactions(cyclesList, cycleZero);
                    SendTransactionsNow(transactions);
                    //WaitForBlock(level.CycleEndBlock() + 20);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    Log.Error(e.ToString());
                    Singleton.Instance.SendMessage(e.Message);
                    Singleton.Instance.SendMessage(e.ToString());
                    Thread.Sleep(TimeSpan.FromMinutes(20));
                }
            } while (true);
        }

        private List<MyTrans> GetTransactions(List<long> cyclesList, long cycleZero)
        {
            var transactions = new List<MyTrans>();
            foreach (var cycle in cyclesList)
            {
                var rewardSplit = _tzScanConnector.GetRewardSplitByCycle(cycle);
                transactions.AddRange(GetTransactions(rewardSplit, cycleZero, cycle).ToList());
            }

            return transactions;
        }

        private static List<long> GetCyclesWeNeedToPay(long cycleZero)
        {
            var myContext = new MyContext();
            var list = new List<long>();
            Log.Info("Cycles we need to pay...");
            foreach (var guy in myContext.DelegateConfig)
            {
                Log.Info($"Guy {guy.Name} offset {guy.RewardOffset}");
                var lastPayment = myContext.DelegatePayments.OrderByDescending(x => x.Cycle).FirstOrDefault();
                for (var i = lastPayment.Cycle + 1; i <= cycleZero + guy.RewardOffset; i++) list.Add(i);
            }

            return list.Distinct().ToList();
        }

        private void WaitForBlock(long blockHeight)
        {
            long estimatedBlock;
            do
            {
                Log.Info($"Waiting for {blockHeight}");
                var block = TzScanConnector.GetBlock();
                estimatedBlock = block.Level;
                Log.Info($"Last Block {block.Level}");
                var dateTime = block.Timestamp.ToLocalTime().DateTime;
                var timeSpan = DateTime.Now - dateTime;
                Log.Info($"{block.Level} {dateTime} {timeSpan.ToPrettyFormat()}");
                if (timeSpan.Minutes > 0)
                {
                    estimatedBlock += timeSpan.Minutes;
                    Log.Info($"That block is old. We must be at block {estimatedBlock}");
                }

                if (estimatedBlock >= blockHeight)
                {
                    Log.Info($"We are at {estimatedBlock} finished loop.");
                    var newspanz = TimeSpan.FromSeconds(20);
                    Log.Info($"Waiting for {newspanz} for good measure!");
                    Thread.Sleep(newspanz);
                    return;
                }

                var untilNextBlock = 60 - timeSpan.TotalSeconds % 60;
                var span = TimeSpan.FromSeconds(untilNextBlock);
                Log.Info($"Waiting for {span}");
                Thread.Sleep(span);
                estimatedBlock += 1;
                Log.Info($"We must be at block {estimatedBlock}");
                var blocksToWait = blockHeight - estimatedBlock;
                if (blocksToWait > 0)
                {
                    var spanToWait = TimeSpan.FromMinutes(blocksToWait);
                    Log.Info($"Waiting for {spanToWait}");
                    Thread.Sleep(spanToWait);
                }
            } while (estimatedBlock < blockHeight);

            var newspan = TimeSpan.FromSeconds(20);
            Log.Info($"Waiting for {newspan} for good measure!");
            Thread.Sleep(newspan);
        }

        private List<MyTrans> GetTransactions(RewardSplit rewardSplitByCycle, long cycleZero, long cycle)
        {
            var myContext = new MyContext();
            var transactions = new List<MyTrans>();
            foreach (var delegatorsBalance in rewardSplitByCycle.GetDelegatorsBalance())
            {
                var delegateConfig = myContext.DelegateConfig.Find(_baker.Account, delegatorsBalance.Account.Tz);
                if (cycleZero + delegateConfig.RewardOffset < cycle) continue;
                var delegatePercentage = rewardSplitByCycle.GetDelegatePercentage(delegatorsBalance);
                var totalReward = rewardSplitByCycle.GetTotalReward();
                var delegateReward = totalReward * delegatePercentage * delegatorsBalance.GetFee();
                var myTrans = new MyTrans
                {
                    Name = delegateConfig.Name, Account = delegatorsBalance.Account.Tz, Cycle = cycle,
                    Reward = delegateReward.ToTez(), Percentage = delegatePercentage
                };
                transactions.Add(myTrans);
                Log.Info($"{myTrans}");
            }
            return transactions;
        }

        private Wallet GetWallet()
        {
            var mnemonic = Singleton.Instance.GetMnemonic(_baker.BurnerKt1);
            var passphrase = Singleton.Instance.GetPassphrase(_baker.BurnerKt1);
            var provider = _baker.Node;
            var wallet = new Wallet(mnemonic, passphrase) {Provider = provider};
            return wallet;
        }

        private void SendTransactionsNow(List<MyTrans> transactions)
        {
            if (transactions == null || transactions.Count == 0) return;
            var lowFee = new BigFloat(_baker.NetworkFee);
            var address = _baker.BurnerKt1;
            var wallet = GetWallet();
            var opResult = wallet.Transfer(address, transactions, lowFee).Result;
            var instanceLastOperation = Tz.Net.Singleton.Instance.LastOperation;
            var myContext = new MyContext();
            foreach (var transaction in transactions)
                myContext.DelegatePayments.Add(new DelegatePayments
                {
                    Account = transaction.Account, Cycle = transaction.Cycle,
                    Reward = (decimal) transaction.Reward,
                    Paid = instanceLastOperation
                });
            myContext.SaveChanges();
            Singleton.Instance.SendMessage($"🔔 {Tz.Net.Singleton.Instance.LastOperation}");
        }
    }
}