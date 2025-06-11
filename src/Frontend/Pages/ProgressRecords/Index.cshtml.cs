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
            var progressRecords = await _http.GetFromJsonAsync<List<ProgressRecord>>("api/progressrecords") ?? [];

            foreach (var record in progressRecords)
            {
                // Fetch Trainee details for each ProgressRecord
                var trainee = await _http.GetFromJsonAsync<Trainee>($"api/trainees/{record.TraineeId}");
                if (trainee != null)
                {
                    record.Trainee = trainee;
                }
            }

            ProgressRecords = progressRecords;
        }
    }
}
