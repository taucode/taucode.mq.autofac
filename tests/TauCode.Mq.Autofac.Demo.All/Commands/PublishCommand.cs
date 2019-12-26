namespace TauCode.Mq.Autofac.Demo.All.Commands
{
    // todo: can be contained in Node app itself.
    public class PublishCommand
    {
        public string To { get; set; }
        public string MessageText { get; set; }
    }
}
