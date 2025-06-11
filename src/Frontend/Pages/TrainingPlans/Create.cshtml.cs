using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.TrainingPlans
{
    public class CreateModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public TrainingPlan TrainingPlan { get; set; } = new TrainingPlan();

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // POST to your API
            var resp = await _http.PostAsJsonAsync("api/trainingplans", TrainingPlan);
            
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "API error creating training plan.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
