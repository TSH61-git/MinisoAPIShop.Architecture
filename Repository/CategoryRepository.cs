using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyWebApiShopContext _context;

        public CategoryRepository(MyWebApiShopContext context)
        {
            _context = context;
        }

        async public Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
