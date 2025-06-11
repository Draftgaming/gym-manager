using DataAccess;
using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressRecordsController(GymContext context) : ControllerBase
    {
        private readonly GymContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetProgressRecords()
        {
            var records = await _context.ProgressRecords.Include(p => p.Trainee).ToListAsync();

            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgressRecord(int id)
        {
            var record = await _context.ProgressRecords
                .Include(p => p.Trainee)
                .FirstOrDefaultAsync(p => p.ProgressRecordId == id);

            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> PostProgressRecord(ProgressRecord record)
        {
            _context.ProgressRecords.Add(record);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetProgressRecord), new { id = record.ProgressRecordId }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgressRecord(int id, ProgressRecord record)
        {
            if (id != record.ProgressRecordId)
            {
                return BadRequest();
            }

            _context.Entry(record).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgressRecord(int id)
        {
            var record = await _context.ProgressRecords.FindAsync(id);
            
            if (record == null)
            {
                return NotFound();
            }

            _context.ProgressRecords.Remove(record);
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
