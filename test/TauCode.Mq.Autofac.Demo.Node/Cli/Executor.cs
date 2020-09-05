using System.Collections.Generic;
using System.Linq;
using TauCode.Cli;
using TauCode.Cli.CommandSummary;
using TauCode.Cli.Data;
using TauCode.Extensions;
using TauCode.Mq.Autofac.Demo.All.Messages;

namespace TauCode.Mq.Autofac.Demo.Node.Cli
{
    public class Executor : CliExecutorBase
    {
        public Executor()
            : base(
                typeof(Host).Assembly.GetResourceText(".Grammar.lisp", true),
                null,
                false)
        {
        }

        public override void Process(IList<CliCommandEntry> entries)
        {
            var summary = (new CliCommandSummaryBuilder()).Build(this.Descriptor, entries);
            var host = (Host)this.AddIn.Host;

            var to = summary.Keys["to"].Single();
            var text = summary.Arguments["message-text"].Single();

            host.Publisher.Publish(new Greeting(
                host.UserName,
                to,
                text),
                to);
        }
    }
}
