using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TauCode.Lab.Mq.NHibernate.Tests.App.Client.Dto.Notes;

namespace TauCode.Lab.Mq.NHibernate.Tests.App.Client
{
    public class AppClient : IAppClient
    {
        private readonly HttpClient _httpClient;

        public AppClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IList<NoteDto>> GetUserNotes(string userId)
        {
            var responseMessage = await _httpClient.GetAsync($"api/notes/by-user-id/{userId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"No success: {responseMessage.StatusCode}");
            }

            var json = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IList<NoteDto>>(json);
            return result;
        }
    }
}
