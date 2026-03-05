using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public BranchService(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<List<BranchDTO>> GetBranches(string? query)
        {
            var branches = await _branchRepository.GetBranches(query);
            var branchesDto = _mapper.Map<List<BranchDTO>>(branches);
            return branchesDto;
        }
    }
}