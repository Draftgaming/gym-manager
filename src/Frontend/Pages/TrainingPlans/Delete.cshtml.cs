using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.TrainingPlans
{
    public class DeleteModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public TrainingPlan TrainingPlan { get; set; } = default!;

        // GET: load the plan to confirm deletion
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            TrainingPlan = await _http.GetFromJsonAsync<TrainingPlan>($"api/trainingplans/{id.Value}") ?? default;

            if (TrainingPlan == default)
            {
                return NotFound();
            }

            return Page();
        }

        // POST: call DELETE on the API
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var resp = await _http.DeleteAsync($"api/trainingplans/{id.Value}");

            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error deleting training plan.");
                
                // re-fetch for display
                TrainingPlan = await _http.GetFromJsonAsync<TrainingPlan>($"api/trainingplans/{id.Value}") ?? new TrainingPlan();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
