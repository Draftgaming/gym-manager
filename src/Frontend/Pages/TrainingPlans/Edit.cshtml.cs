using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.TrainingPlans
{
    public class EditModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public TrainingPlan TrainingPlan { get; set; } = new TrainingPlan();

        // GET: load the existing plan
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            TrainingPlan = await _http.GetFromJsonAsync<TrainingPlan>($"api/trainingplans/{id.Value}") ?? default;

            if(TrainingPlan == default)
            {
                return NotFound();
            }

            return Page();
        }

        // POST: send the updated plan
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var resp = await _http.PutAsJsonAsync($"api/trainingplans/{TrainingPlan.TrainingPlanId}", TrainingPlan);
            
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "API error updating training plan.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
