using DataAccess.Models;

using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.Trainers
{
    public class IndexModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        public List<Trainer> Trainers { get; private set; } = [];

        public async Task OnGetAsync()
        {
            var trainers = await _http.GetFromJsonAsync<List<Trainer>>("api/trainers") ?? [];

            foreach (var trainer in trainers)
            {
                var trainees = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? [];
                trainer.Trainees = trainees.FindAll(t => t.TrainerId == trainer.TrainerId);
            }

            Trainers = trainers;
        }
    }
}
