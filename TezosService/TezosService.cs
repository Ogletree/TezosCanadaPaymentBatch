using System;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace TezosService
{
    public partial class TezosService : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public TezosService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Singleton.Instance.Bot.OnMessage += MessageRouter.OnMessage;
                Singleton.Instance.Bot.StartReceiving();
                System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                Singleton.Instance.SendMessage("👍 Tezos Service is starting.");
                var thread = new Thread(Execute);
                thread.Start();
            }
            catch (Exception e)
            {
                Log.Error("Eror in TezosService OnStart");
                Log.Error(e);
                throw;
            }
        }

        private static void Execute()
        {
            try
            {
                Parallel.ForEach(Singleton.Instance.Bakers, baker =>
                {
                    var thingDoer = new ThingDoer(baker);
                    thingDoer.DoThing();
                });
            }
            catch (Exception e)
            {
                Log.Error("Eror in TezosService Execute");
                Log.Error(e);
                throw;
            }
        }

        protected override void OnStop()
        {
            try
            {
                Singleton.Instance.SendMessage("💀 Tezos Service is stopping.");
                Singleton.Instance.Bot.StopReceiving();
            }
            catch (Exception e)
            {
                Log.Error("Eror in TezosService Execute");
                Log.Error(e);
                throw;
            }
        }
    }
}