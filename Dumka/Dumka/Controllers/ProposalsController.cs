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
using Dumka.Data;

namespace Dumka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsController : ControllerBase
    {
        private readonly DumkaDbContext _context;
        private readonly AuthService _authService;
        private readonly ProposalService _proposalService;
        private readonly IMapper _mapper;

        public ProposalsController(DumkaDbContext context, AuthService authService,
                                   ProposalService proposalService, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _proposalService = proposalService;
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
            int? userId = _authService.GetUserId(User.Claims);
            if (userId == null)
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
            IEnumerable<ProposalInfoDto> proposalDtos = proposals.Select(_ => _mapper.Map<ProposalInfoDto>(_, opt => {
                opt.Items[DumkaAutomapperConstants.IsForDisplayingInList] = true;
                opt.Items[DumkaAutomapperConstants.CurrentUserId] = userId;
            }));
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
            int? userId = _authService.GetUserId(User.Claims);
            if (userId == null)
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
            return new JsonResult(_mapper.Map<ProposalInfoDto>(proposal, opt => {
                opt.Items[DumkaAutomapperConstants.CurrentUserId] = userId;
            }));
        }

        // PUT: api/Proposals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProposals(int id, ProposalDto proposalDto)
        {
            if (proposalDto.Id == 0)
            {
                proposalDto.Id = id;
            }
            if (id != proposalDto.Id)
            {
                return BadRequest();
            }

            var proposalTuple = await _proposalService.UpdateProposal(proposalDto);
            if (proposalTuple.Item2 != null)
            {
                return new JsonResult(new { errors = proposalTuple.Item2 });
            }
            if (proposalTuple.Item1 == null)
            {
                return NotFound();
            }
            
            return RedirectToAction("GetProposals", new { id = proposalDto.Id });
        }

        // POST: api/Proposals
        [HttpPost]
        public async Task<IActionResult> PostProposals(ProposalDto proposalDto)
        {
            if (proposalDto.Id == 0)
            {
                var proposalTuple = await _proposalService.CreateProposal(proposalDto);
                if (proposalTuple.Item2 != null)
                {
                    return new JsonResult(new { errors = proposalTuple.Item2 });
                }
                if (proposalTuple.Item1 == null)
                {
                    return Forbid();
                }
                return CreatedAtAction("GetProposals", new { id = proposalTuple.Item1.Id }, _mapper.Map<ProposalInfoDto>(proposalTuple.Item1));
            }
            else
            {
                return await PutProposals(proposalDto.Id, proposalDto);
            }
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
