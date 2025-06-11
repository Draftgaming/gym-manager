using DataAccess;
using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingPlansController(GymContext context) : ControllerBase
    {
        private readonly GymContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetTrainingPlans()
        {
            var plans = await _context.TrainingPlans
                .Include(p => p.Trainees)
                .ToListAsync();

            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingPlan(int id)
        {
            var plan = await _context.TrainingPlans.FindAsync(id);

            return plan == null ? NotFound() : Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> PostTrainingPlan(TrainingPlan plan)
        {
            _context.TrainingPlans.Add(plan);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrainingPlan), new { id = plan.TrainingPlanId }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingPlan(int id, TrainingPlan plan)
        {
            if (id != plan.TrainingPlanId)
            {
                return BadRequest();
            }

            _context.Entry(plan).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingPlan(int id)
        {
            var plan = await _context.TrainingPlans.FindAsync(id);

            if (plan == null)
            {
                return NotFound();
            }

            _context.TrainingPlans.Remove(plan);
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
