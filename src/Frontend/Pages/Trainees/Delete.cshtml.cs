using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.Trainees
{
    public class DeleteModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public Trainee Trainee { get; set; } = default!;

        // GET: fetch the trainee to confirm deletion
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Trainee = await _http.GetFromJsonAsync<Trainee>($"api/trainees/{id.Value}") ?? throw new InvalidOperationException("Trainee not found");

            return Page();
        }

        // POST: call DELETE on the API
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var resp = await _http.DeleteAsync($"api/trainees/{id.Value}");

            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error deleting trainee.");

                Trainee = await _http.GetFromJsonAsync<Trainee>($"api/trainees/{id.Value}") ?? new Trainee();

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
