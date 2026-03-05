using Entities;

namespace Repository
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetBranches(string? query);
    }
}