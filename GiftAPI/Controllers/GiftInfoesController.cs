using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiftInfoLibrary.Models;

namespace GiftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftInfoesController : ControllerBase
    {
        private readonly GiftInfoDbContext _context;

        public GiftInfoesController(GiftInfoDbContext context)
        {
            _context = context;
        }

        // GET: api/GiftInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftInfo>>> GetGiftInfos()
        {
          if (_context.GiftInfos == null)
          {
              return NotFound();
          }
            return await _context.GiftInfos.ToListAsync();
        }

        // GET: api/GiftInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftInfo>> GetGiftInfo(int id)
        {
          if (_context.GiftInfos == null)
          {
              return NotFound();
          }
            var giftInfo = await _context.GiftInfos.FindAsync(id);

            if (giftInfo == null)
            {
                return NotFound();
            }

            return giftInfo;
        }

        // PUT: api/GiftInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiftInfo(int id, GiftInfo giftInfo)
        {
            if (id != giftInfo.GiftId)
            {
                return BadRequest();
            }

            _context.Entry(giftInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiftInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GiftInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GiftInfo>> PostGiftInfo(GiftInfo giftInfo)
        {
          if (_context.GiftInfos == null)
          {
              return Problem("Entity set 'GiftInfoDbContext.GiftInfos'  is null.");
          }
            _context.GiftInfos.Add(giftInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGiftInfo", new { id = giftInfo.GiftId }, giftInfo);
        }

        // DELETE: api/GiftInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiftInfo(int id)
        {
            if (_context.GiftInfos == null)
            {
                return NotFound();
            }
            var giftInfo = await _context.GiftInfos.FindAsync(id);
            if (giftInfo == null)
            {
                return NotFound();
            }

            _context.GiftInfos.Remove(giftInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GiftInfoExists(int id)
        {
            return (_context.GiftInfos?.Any(e => e.GiftId == id)).GetValueOrDefault();
        }
    }
}
