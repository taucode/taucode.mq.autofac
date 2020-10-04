using System.Threading;
using System.Threading.Tasks;
using TauCode.Lab.Mq.NHibernate.Tests.App.Client.Messages.Notes;
using TauCode.Mq.Abstractions;

namespace TauCode.Lab.Mq.NHibernate.Tests.App.Core.Handlers.Notes
{
    public class NewNoteHandler : AsyncMessageHandlerBase<NewNoteMessage>
    {
        public override Task HandleAsync(NewNoteMessage message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
