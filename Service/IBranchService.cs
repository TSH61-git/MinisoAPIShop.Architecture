using DTOs;

namespace Service
{
    public interface IBranchService
    {
        Task<List<BranchDTO>> GetBranches(string? query);
    }
}