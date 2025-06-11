using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.Trainers
{
    public class EditModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public Trainer Trainer { get; set; } = new Trainer();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // GET the existing trainer
            Trainer = await _http.GetFromJsonAsync<Trainer>($"api/trainers/{id.Value}") ?? default;
            
            if (Trainer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // PUT the updated trainer
            var resp = await _http.PutAsJsonAsync($"api/trainers/{Trainer.TrainerId}", Trainer);
            
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "API error updating trainer.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
