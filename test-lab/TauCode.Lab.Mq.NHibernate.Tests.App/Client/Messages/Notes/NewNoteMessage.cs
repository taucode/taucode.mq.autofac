using System;
using TauCode.Mq.Abstractions;

namespace TauCode.Lab.Mq.NHibernate.Tests.App.Client.Messages.Notes
{
    public class NewNoteMessage : IMessage
    {
        public string Topic { get; set; }
        public string CorrelationId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public string UserId { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public ImportanceDto Importance { get; set; }
    }
}
