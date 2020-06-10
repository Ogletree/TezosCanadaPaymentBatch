using System;
using System.ServiceProcess;
using System.Threading;

namespace TezosService.Misc
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new TezosService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}