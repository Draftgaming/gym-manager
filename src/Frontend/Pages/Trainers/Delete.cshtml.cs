using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.Trainers
{
    public class DeleteModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;

        // GET: fetch the trainer to confirm deletion
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Trainer = await _http.GetFromJsonAsync<Trainer>($"api/trainers/{id.Value}") ?? default;

            return Page();
        }

        // POST: call DELETE on the API
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var resp = await _http.DeleteAsync($"api/trainers/{id.Value}");
            
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error deleting trainer.");
                Trainer = await _http.GetFromJsonAsync<Trainer>($"api/trainers/{id.Value}") ?? new Trainer();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
