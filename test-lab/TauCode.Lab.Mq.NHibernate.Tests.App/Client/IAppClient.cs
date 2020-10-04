using System.Collections.Generic;
using System.Threading.Tasks;
using TauCode.Lab.Mq.NHibernate.Tests.App.Client.Dto.Notes;

namespace TauCode.Lab.Mq.NHibernate.Tests.App.Client
{
    public interface IAppClient
    {
        Task<IList<NoteDto>> GetUserNotes(string userId);
    }
}
