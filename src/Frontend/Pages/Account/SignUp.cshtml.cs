// Frontend/Pages/Account/SignUp.cshtml.cs
using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Pages.Account
{
    public class SignUpModel(IHttpClientFactory httpFactory) : PageModel
    {
        private readonly HttpClient _http = httpFactory.CreateClient("BackendAPI");

        // Bindable properties match Trainee fields
        [BindProperty, Required]
        public string FullName { get; set; } = "";

        [BindProperty, Required]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        [BindProperty, Required]
        public int TrainerId { get; set; }

        public SelectList TrainerOptions { get; private set; } = default!;

        [BindProperty, Required]
        public int TrainingPlanId { get; set; }

        public SelectList PlanOptions { get; private set; } = default!;

        [BindProperty, Required, MaxLength(50)]
        public string Username { get; set; } = "";

        [BindProperty, Required, MaxLength(100)]
        public string Password { get; set; } = "";

        [BindProperty, Required, MaxLength(20)]
        public string Role { get; set; } = "User";

        public async Task OnGetAsync()
        {
            var trainers = await _http.GetFromJsonAsync<List<Trainer>>("api/trainers") ?? [];
            TrainerOptions = new SelectList(trainers, "TrainerId", "Name");

            var plans = await _http.GetFromJsonAsync<List<TrainingPlan>>("api/trainingplans") ?? [];
            PlanOptions = new SelectList(plans, "TrainingPlanId", "PlanName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            // Build the Trainee DTO
            var newTrainee = new Trainee
            {
                FullName = FullName,
                DateJoined = DateJoined,
                TrainerId = TrainerId,
                TrainingPlanId = TrainingPlanId,
                Username = Username,
                Password = Password,
                Role = Role
            };

            // Send to backend
            var resp = await _http.PostAsJsonAsync("api/trainees", newTrainee);

            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error creating account.");
                await OnGetAsync();
                return Page();
            }

            return RedirectToPage("/Account/Login");
        }
    }
}
