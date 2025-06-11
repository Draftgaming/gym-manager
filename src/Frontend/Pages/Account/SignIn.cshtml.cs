using DataAccess.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Frontend.Pages.Account
{
    public class SignInModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        [BindProperty] public string Username { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";
        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var login = new { Username, Password };
            var resp = await _http.PostAsJsonAsync("api/trainees/authenticate", login);

            if (!resp.IsSuccessStatusCode)
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }

            var trainee = await resp.Content.ReadFromJsonAsync<Trainee>();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, trainee!.Username),
                new Claim(ClaimTypes.Role, trainee.Role),
                new Claim("TraineeId", trainee.TraineeId.ToString())
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = System.DateTimeOffset.UtcNow.AddHours(2)
                });

            return RedirectToPage("/Index");
        }
    }
}
