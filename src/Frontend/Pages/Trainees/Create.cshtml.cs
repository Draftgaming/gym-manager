using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.Trainees
{
    public class CreateModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public Trainee Trainee { get; set; } = new Trainee();

        // these hold the dropdown options
        public SelectList TrainerOptions { get; set; } = default!;
        public SelectList TrainingPlanOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // fetch all trainers
            var trainers = await _http.GetFromJsonAsync<List<Trainer>>("api/trainers") ?? [];
            
            TrainerOptions = new SelectList(trainers, "TrainerId", "Name");

            // fetch all training plans
            var plans = await _http.GetFromJsonAsync<List<TrainingPlan>>("api/trainingplans") ?? [];
            
            TrainingPlanOptions = new SelectList(plans, "TrainingPlanId", "PlanName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // re-populate dropdowns if validation fails
                await OnGetAsync();
                return Page();
            }

            // post the new trainee to your API
            var resp = await _http.PostAsJsonAsync("api/trainees", Trainee);
            if (!resp.IsSuccessStatusCode)
            {
                // you might read error details here and surface them
                ModelState.AddModelError(string.Empty, "API error creating trainee.");
                await OnGetAsync();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
