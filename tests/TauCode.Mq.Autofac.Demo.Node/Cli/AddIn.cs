using System.Collections.Generic;
using TauCode.Cli;

namespace TauCode.Mq.Autofac.Demo.Node.Cli
{
    public class AddIn : CliAddInBase
    {
        protected override IReadOnlyList<ICliWorker> CreateWorkers()
        {
            return new List<ICliWorker>
            {
                new Worker(),
            };
        }
    }
}
