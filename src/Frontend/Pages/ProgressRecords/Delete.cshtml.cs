using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.ProgressRecords
{
    public class DeleteModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty]
        public ProgressRecord ProgressRecord { get; set; } = default!;

        // GET: load the record to confirm deletion
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            ProgressRecord = await _http.GetFromJsonAsync<ProgressRecord>($"api/progressrecords/{id.Value}") ?? default;

            if (ProgressRecord == default)
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

            var resp = await _http.DeleteAsync($"api/progressrecords/{id.Value}");

            if (resp.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, "Error deleting record.");
            ProgressRecord = await _http.GetFromJsonAsync<ProgressRecord>($"api/progressrecords/{id.Value}") ?? new ProgressRecord();
            return Page();
        }
    }
}
