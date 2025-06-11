using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.Trainers
{
    public class CreateModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public Trainer Trainer { get; set; } = new Trainer();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // POST to your API
            var resp = await _http.PostAsJsonAsync("api/trainers", Trainer);
            
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "API error creating trainer.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
