using DataAccess;
using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController(GymContext context) : ControllerBase
    {
        private readonly GymContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetTrainers()
        {
            var trainers = await _context.Trainers.ToListAsync();

            return Ok(trainers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);

            return trainer == null ? NotFound() : Ok(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> PostTrainer(Trainer trainer)
        {
            _context.Trainers.Add(trainer);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrainer), new { id = trainer.TrainerId }, trainer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, Trainer trainer)
        {
            if (id != trainer.TrainerId)
            {
                return BadRequest();
            }

            _context.Entry(trainer).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);

            if (trainer == null)
            {
                return NotFound();
            }

            _context.Trainers.Remove(trainer);
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
