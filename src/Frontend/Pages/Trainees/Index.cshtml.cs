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

        public List<Trainee> Trainees { get; private set; }

        public async Task OnGetAsync()
        {
            // Call the API and deserialize JSON into your model list
            Trainees = await _http.GetFromJsonAsync<List<Trainee>>("api/trainees") ?? [];
        }
    }
}
