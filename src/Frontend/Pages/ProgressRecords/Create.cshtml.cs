using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.ProgressRecords
{
    public class CreateModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        // for the Trainee dropdown
        public SelectList TraineeOptions { get; set; } = default!;

        [BindProperty]
        public ProgressRecord ProgressRecord { get; set; } = new ProgressRecord();

        public async Task<IActionResult> OnGetAsync()
        {
            var trainees = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees")?? [];

            TraineeOptions = new SelectList(trainees, "TraineeId", "FullName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // re-populate dropdown if validation fails
                var traineesData = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? [];
                TraineeOptions = new SelectList(traineesData, "TraineeId", "FullName");
                return Page();
            }

            var resp = await _http.PostAsJsonAsync("api/progressrecords", ProgressRecord);
            
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, "API error creating progress record.");
            var trainees = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? [];
            TraineeOptions = new SelectList(trainees, "TraineeId", "FullName");
            return Page();
        }
    }
}
