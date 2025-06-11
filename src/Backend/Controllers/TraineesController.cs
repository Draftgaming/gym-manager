using DataAccess;
using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineesController(GymContext context) : ControllerBase
    {
        private readonly GymContext _context = context;

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel login)
        {
            var trainee = await _context
                .Trainees
                .FirstOrDefaultAsync(t =>t.Username == login.Username && t.Password == login.Password);

            return trainee == null ? Unauthorized() : Ok(trainee);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainees()
        {
            var trainees = await _context
                .Trainees
                .Include(t => t.Trainer)
                .Include(t => t.TrainingPlan)
                .ToListAsync();

            return Ok(trainees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainee(int id)
        {
            var trainee = await _context.Trainees
                .Include(t => t.Trainer)
                .Include(t => t.TrainingPlan)
                .FirstOrDefaultAsync(t => t.TraineeId == id);

            return trainee == null ? NotFound() : Ok(trainee);
        }

        [HttpPost]
        public async Task<IActionResult> PostTrainee(Trainee trainee)
        {
            _context.Trainees.Add(trainee);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrainee), new { id = trainee.TraineeId }, trainee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainee(int id, Trainee trainee)
        {
            if (id != trainee.TraineeId)
            {
                return BadRequest();
            }

            _context.Entry(trainee).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainee(int id)
        {
            var trainee = await _context.Trainees.FindAsync(id);

            if (trainee == null)
            {
                return NotFound();
            }

            _context.Trainees.Remove(trainee);
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
