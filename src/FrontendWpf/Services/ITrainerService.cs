using DataAccess.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontendWpf.Services
{
    public interface ITrainerService
    {
        Task<List<Trainer>> GetAllAsync();
        Task<Trainer> CreateAsync(Trainer t);
        Task UpdateAsync(Trainer t);
        Task DeleteAsync(int id);
    }
}
