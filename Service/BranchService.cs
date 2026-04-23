using AutoMapper;
using DTOs;
using Entities;
using Repository;
using StackExchange.Redis; // חובה להוסיף
using System.Text.Json;    // לצורך סריאליזציה
using Microsoft.Extensions.Configuration; // לצורך משיכת ה-TTL

namespace Service
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IDatabase _cache;
        private readonly IConfiguration _configuration;

        public BranchService(IBranchRepository branchRepository, IMapper mapper, IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _cache = redis.GetDatabase();
            _configuration = configuration;
        }

        public async Task<List<BranchDTO>> GetBranches(string? query)
        {
            string cacheKey = $"branches_{query ?? "all"}";

            var cachedBranches = await _cache.StringGetAsync(cacheKey);
            if (!cachedBranches.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<List<BranchDTO>>(cachedBranches);
            }

            var branches = await _branchRepository.GetBranches(query);
            var branchesDto = _mapper.Map<List<BranchDTO>>(branches);

            var ttlMinutes = _configuration.GetValue<int>("CacheSettings:DefaultTTLInMinutes");

            string jsonBranches = JsonSerializer.Serialize(branchesDto);
            await _cache.StringSetAsync(cacheKey, jsonBranches, TimeSpan.FromMinutes(ttlMinutes));

            return branchesDto;
        }
    }
}