using System;
using System.Threading;
using System.Threading.Tasks;
using TauCode.Cqrs.Queries;

namespace TauCode.Lab.Mq.NHibernate.Tests.App.Core.Features.Notes.GetUserNotes
{
    public class GetUserNotesQueryHandler : IQueryHandler<GetUserNotesQuery>
    {
        public void Execute(GetUserNotesQuery query)
        {
            throw new NotSupportedException("Use async overload");
        }

        public Task ExecuteAsync(GetUserNotesQuery query, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
