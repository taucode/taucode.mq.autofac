﻿using System.Collections.Generic;
using System.Linq;
using TauCode.Cli;

namespace TauCode.Mq.Autofac.Demo.Node.Cli
{
    public class Host : CliHostBase
    {
        public Host()
            : base("demo", "1.0", true)
        {
        }

        public string UserName { get; set; }
        public IMessagePublisher Publisher { get; set; }

        protected override IReadOnlyList<ICliAddIn> CreateAddIns()
        {
            return new List<ICliAddIn>
            {
                new AddIn(),
            };
        }

        protected override string GetHelpImpl()
        {
            var worker = this
                .GetAddIns()
                .Single()
                .GetExecutors()
                .Single();
            var descriptor = worker.Descriptor;
            var help = descriptor.GetHelp();
            return help;
        }
    }
}
