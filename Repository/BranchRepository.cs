using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly MyWebApiShopContext _context;

        public BranchRepository(MyWebApiShopContext context)
        {
            _context = context;
        }

        public async Task<List<Branch>> GetBranches(string? query)
        {
            var branchesQuery = _context.Branches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                branchesQuery = branchesQuery.Where(b =>
                    b.BranchName.Contains(query) ||
                    b.Address.Contains(query));
            }

            return await branchesQuery
                .OrderBy(b => b.BranchName)
                .ToListAsync();
        }
    }
}
