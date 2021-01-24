using AutoMapper;
using Dumka.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Services
{
    public class ProposalService
    {
        private readonly DumkaDbContext _dbContext;
        private readonly IMapper _mapper;


        public ProposalService(DumkaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Tuple<Proposal, string>> CreateProposal(ProposalDto proposalDto)
        {
            var proposal = _mapper.Map<Proposal>(proposalDto);

            var stageTuple = await GetStage(proposalDto.StageId, proposalDto.Stage);
            if (stageTuple?.Item2 != null)
            {
                return new Tuple<Proposal, string>(null, stageTuple.Item2);

            }
            else if (stageTuple?.Item1 != null)
            {
                proposal.Stage = stageTuple.Item1;
            }

            var deadlineTuple = await GetDeadline(proposalDto.DeadlineId, proposalDto.Deadline);
            if (deadlineTuple?.Item2 != null)
            {
                return new Tuple<Proposal, string>(null, deadlineTuple.Item2);

            }
            else if (deadlineTuple?.Item1 != null)
            {
                proposal.Deadline = deadlineTuple.Item1;
            }

            _dbContext.Proposals.Add(proposal);
            await _dbContext.SaveChangesAsync();
            return new Tuple<Proposal, string>(proposal, null);
        }

        public async Task<Tuple<Proposal, string>> UpdateProposal(ProposalDto proposalDto)
        {
            var proposal = await _dbContext.Proposals.FirstOrDefaultAsync(_ => _.Id == proposalDto.Id);
            if (proposal != null)
            {
                _mapper.Map<ProposalDto, Proposal>(proposalDto, proposal);

                var stageTuple = await GetStage(proposalDto.StageId, proposalDto.Stage);
                if (stageTuple?.Item2 != null )
                {
                    return new Tuple<Proposal, string>(null, stageTuple.Item2);

                }
                else if (stageTuple?.Item1 != null)
                {
                    proposal.Stage = stageTuple.Item1;
                }

                var deadlineTuple = await GetDeadline(proposalDto.DeadlineId, proposalDto.Deadline);
                if (deadlineTuple?.Item2 != null)
                {
                    return new Tuple<Proposal, string>(null, deadlineTuple.Item2);

                }
                else if (deadlineTuple?.Item1 != null)
                {
                    proposal.Deadline = deadlineTuple.Item1;
                }

                _dbContext.Entry(proposal).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();
            }
            return new Tuple<Proposal, string>(proposal, null);
        }

        public async Task<Tuple<ProposalStageTypes, string>> GetStage(int? stageId = null, string stageName = null)
        {
            if (stageId == null && stageName == null)
            {
                return new Tuple<ProposalStageTypes, string>(null, null);
            }

            List<ProposalStageTypes> stages = await _dbContext.ProposalStageTypes
                                                              .Where(_ => _.Id == stageId || _.Name == stageName)
                                                              .ToListAsync();
            if (stages.Count() > 1)
            {
                return new Tuple<ProposalStageTypes, string>(null, "Inconsistent stage id and name");
            }
            else if (stages.Count() == 1)
            {
                return new Tuple<ProposalStageTypes, string>(stages.First(), null);
            }
            else
            {
                return new Tuple<ProposalStageTypes, string>(null, "Stage's does not exist");
            }
        }

        public async Task<Tuple<ProposalDeadlineType, string>> GetDeadline(int? deadlineId = null, string deadlineName = null)
        {
            if (deadlineId == null && deadlineName == null)
            {
                return new Tuple<ProposalDeadlineType, string>(null, null);
            }

            List<ProposalDeadlineType> deadlines = await _dbContext.ProposalDeadlineTypes
                                                              .Where(_ => _.Id == deadlineId || _.Name == deadlineName)
                                                              .ToListAsync();
            if (deadlines.Count() > 1)
            {
                return new Tuple<ProposalDeadlineType, string>(null, "Inconsistent dealine's id and name");
            }
            else if (deadlines.Count() == 1)
            {
                return new Tuple<ProposalDeadlineType, string>(deadlines.First(), null);
            }
            else
            {
                return new Tuple<ProposalDeadlineType, string>(null, "Deadline does not exist");
            }
        }
    }
}
