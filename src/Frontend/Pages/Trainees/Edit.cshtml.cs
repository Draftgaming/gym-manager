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
    public class EditModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public Trainee Trainee { get; set; } = new Trainee();

        public SelectList TrainerOptions { get; set; } = default!;
        public SelectList TrainingPlanOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 1) Load the trainee
            Trainee = await _http.GetFromJsonAsync<Trainee>($"api/trainees/{id.Value}") ?? default;

            if (Trainee == default)
            {
                return NotFound("Trainee not found.");
            }

            // 2) Load dropdowns
            var trainers = await _http.GetFromJsonAsync<List<Trainer>>("api/trainers") ?? [];

            TrainerOptions = new SelectList(trainers, "TrainerId", "Name", Trainee.TrainerId);

            var plans = await _http.GetFromJsonAsync<List<TrainingPlan>>("api/trainingplans") ?? [];

            TrainingPlanOptions = new SelectList(plans, "TrainingPlanId", "PlanName", Trainee.TrainingPlanId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Re-populate dropdowns and return
                await OnGetAsync(Trainee.TraineeId);
                return Page();
            }

            // Send a PUT to update
            var resp = await _http.PutAsJsonAsync($"api/trainees/{Trainee.TraineeId}", Trainee);
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "API error updating trainee.");
                await OnGetAsync(Trainee.TraineeId);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
