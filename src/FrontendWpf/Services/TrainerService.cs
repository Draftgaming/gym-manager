using DataAccess.Models;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontendWpf.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly HttpClient _http = new() { BaseAddress = new("http://localhost:5106/") };

        public Task<List<Trainer>> GetAllAsync() => _http.GetFromJsonAsync<List<Trainer>>("api/trainers")!;

        public async Task<Trainer> CreateAsync(Trainer t) => await _http
            .PostAsJsonAsync("api/trainers", t)
            .Result
            .Content
            .ReadFromJsonAsync<Trainer>()!;

        public Task UpdateAsync(Trainer t) => _http.PutAsJsonAsync($"api/trainers/{t.TrainerId}", t);

        public Task DeleteAsync(int id) => _http.DeleteAsync($"api/trainers/{id}");
    }
}
