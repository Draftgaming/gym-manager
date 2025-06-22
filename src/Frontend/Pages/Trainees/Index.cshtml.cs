using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.Trainees
{
    public class IndexModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        public List<Trainee> Trainees { get; private set; } = new();

        public async Task OnGetAsync()
        {
            var currentUser = User.Identity?.Name;
            var isAdmin = User.IsInRole("Administrator");

            var allTrainees = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? new();

            if (isAdmin)
            {
                // Show all trainees
                Trainees = allTrainees;
            }
            else
            {
                // Show only the logged-in user
                Trainees = allTrainees.FindAll(t => t.Username == currentUser);
            }
        }
    }
}
