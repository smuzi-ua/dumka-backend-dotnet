using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dumka;
using AutoMapper;
using Dumka.Services;
using Dumka.Models.DTO;

namespace Dumka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsController : ControllerBase
    {
        private readonly DumkaDbContext _context;
        private readonly AuthService _authService;
        private readonly IMapper _mapper;

        public ProposalsController(DumkaDbContext context, AuthService authService,
                                   IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _mapper = mapper;
        }

        // GET: api/Proposals
        [HttpGet]
        public async Task<IActionResult> GetProposals()
        {
            int? schoolId = _authService.GetSchoolId(User.Claims);
            if (schoolId == null)
            {
                return BadRequest();
            }
            var proposals = await _context.Proposals
                .Include(_ => _.User)
                .Include(_ => _.Stage)
                .Include(_ => _.Deadline)
                .Include(_ => _.ProposalLikes)
                .Include(_ => _.Comments)
                .Where(_ => _.User.SchoolId == schoolId)
                .ToListAsync();
            IEnumerable<ProposalDto> proposalDtos = proposals.Select(_ => _mapper.Map<ProposalDto>(_));
            return new JsonResult(proposalDtos);
        }

        // GET: api/Proposals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProposals(int id)
        {
            int? schoolId = _authService.GetSchoolId(User.Claims);
            if (schoolId == null)
            {
                return BadRequest();
            }
            var proposal = await _context.Proposals
                .Include(_ => _.User)
                .Include(_ => _.Stage)
                .Include(_ => _.Deadline)
                .Include(_ => _.ProposalLikes)
                .Include(_ => _.Comments)
                .FirstOrDefaultAsync(_ => _.Id == id);
            if (proposal == null)
            {
                return NotFound();
            }
            if (proposal.User.SchoolId != schoolId)
            {
                return Forbid();
            }
            return new JsonResult(_mapper.Map<ProposalDto>(proposal));
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
