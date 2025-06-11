using DataAccess.Models;

using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.TrainingPlans
{
    public class IndexModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        public List<TrainingPlan> TrainingPlans { get; private set; } = [];

        public async Task OnGetAsync()
        {
            var trainingPlans = await _http.GetFromJsonAsync<List<TrainingPlan>>("api/trainingplans") ?? [];

            foreach (var trainingPlan in trainingPlans)
            {
                var trainees = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? [];
                trainingPlan.Trainees = trainees.FindAll(t => t.TrainingPlanId == trainingPlan.TrainingPlanId);
            }

            TrainingPlans = trainingPlans;
        }
    }
}
