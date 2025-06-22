using DataAccess.Models;

using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.ProgressRecords
{
    public class IndexModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        public List<ProgressRecord> ProgressRecords { get; private set; } = [];

        public async Task OnGetAsync()
        {
            var currentUser = User.Identity? .Name;
            var isAdmin = User.IsInRole("Administrator");

            // Fetch all records if Admin
            var allRecords = await _http.GetFromJsonAsync<List<ProgressRecord>>("api/progressrecords") ?? new();

            var filteredRecords = new List<ProgressRecord>();

            foreach (var record in allRecords)
            {
                var trainee = await _http.GetFromJsonAsync<Trainee>($"api/trainees/{record.TraineeId}");
                if (trainee == null)
                    continue;

                record.Trainee = trainee;

                if (isAdmin || trainee.Username == currentUser)
                {
                    filteredRecords.Add(record);
                }
            }

            ProgressRecords = filteredRecords;
        }
    }
}
