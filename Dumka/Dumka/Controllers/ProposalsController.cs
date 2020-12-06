using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dumka;

namespace Dumka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsController : ControllerBase
    {
        private readonly DumkaDbContext _context;

        public ProposalsController(DumkaDbContext context)
        {
            _context = context;
        }

        // GET: api/Proposals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proposal>>> GetProposals()
        {
            return await _context.Proposals.ToListAsync();
        }

        // GET: api/Proposals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proposal>> GetProposals(int id)
        {
            var proposals = await _context.Proposals.FindAsync(id);

            if (proposals == null)
            {
                return NotFound();
            }

            return proposals;
        }

        // PUT: api/Proposals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProposals(int id, Proposal proposals)
        {
            if (id != proposals.Id)
            {
                return BadRequest();
            }

            _context.Entry(proposals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposalsExists(id))
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

        // POST: api/Proposals
        [HttpPost]
        public async Task<ActionResult<Proposal>> PostProposals(Proposal proposals)
        {
            _context.Proposals.Add(proposals);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProposals", new { id = proposals.Id }, proposals);
        }

        // DELETE: api/Proposals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Proposal>> DeleteProposals(int id)
        {
            var proposals = await _context.Proposals.FindAsync(id);
            if (proposals == null)
            {
                return NotFound();
            }

            _context.Proposals.Remove(proposals);
            await _context.SaveChangesAsync();

            return proposals;
        }

        private bool ProposalsExists(int id)
        {
            return _context.Proposals.Any(e => e.Id == id);
        }
    }
}
