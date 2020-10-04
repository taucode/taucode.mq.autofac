using System;
using System.Threading;
using System.Threading.Tasks;
using TauCode.Cqrs.Commands;
using TauCode.Lab.Mq.NHibernate.Tests.App.Domain.Notes;

namespace TauCode.Lab.Mq.NHibernate.Tests.App.Core.Features.Notes.CreateNote
{
    public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand>
    {
        private readonly INoteRepository _noteRepository;

        public CreateNoteCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public void Execute(CreateNoteCommand command)
        {
            throw new NotSupportedException("Use async overload.");
        }

        public Task ExecuteAsync(CreateNoteCommand command, CancellationToken cancellationToken = default)
        {
            var note = new Note(command.UserId, command.Subject, command.Body);
            _noteRepository.Save(note);
        }
    }
}
