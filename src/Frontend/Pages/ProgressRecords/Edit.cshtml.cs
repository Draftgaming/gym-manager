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
    public class EditModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public ProgressRecord ProgressRecord { get; set; } = new();

        public SelectList TraineeOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            // 1) Load the existing record
            ProgressRecord = await _http.GetFromJsonAsync<ProgressRecord>($"api/progressrecords/{id.Value}") ?? default;

            if (ProgressRecord == default)
            {
                return NotFound();
            }

            // 2) Load the trainees for the dropdown
            var trainees = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? [];

            TraineeOptions = new SelectList(trainees, "TraineeId", "FullName", ProgressRecord.TraineeId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // re-populate dropdown before returning
                var traineesData = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? [];

                TraineeOptions = new SelectList(traineesData, "TraineeId", "FullName", ProgressRecord.TraineeId);

                return Page();
            }

            // PUT the updated record
            var resp = await _http.PutAsJsonAsync($"api/progressrecords/{ProgressRecord.ProgressRecordId}", ProgressRecord);

            if (resp.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }


            ModelState.AddModelError(string.Empty, "API error updating record.");
            
            // re-populate dropdown
            var trainees = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? [];
            
            TraineeOptions = new SelectList(trainees, "TraineeId", "FullName", ProgressRecord.TraineeId);
            
            return Page();
        }
    }
}
