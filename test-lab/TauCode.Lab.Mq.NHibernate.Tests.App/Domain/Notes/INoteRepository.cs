using System.Collections.Generic;

namespace TauCode.Lab.Mq.NHibernate.Tests.App.Domain.Notes
{
    public interface INoteRepository
    {
        IList<Note> GetUserNotes(string userId);
        void Save(Note note);
    }
}
