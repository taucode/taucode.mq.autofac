using System.Collections.Generic;
using TauCode.Cli;

namespace TauCode.Mq.Autofac.Demo.Node.Cli
{
    public class AddIn : CliAddInBase
    {
        protected override IReadOnlyList<ICliExecutor> CreateExecutors()
        {
            return new ICliExecutor[]
            {
                new Executor(),
            };
        }
    }
}
