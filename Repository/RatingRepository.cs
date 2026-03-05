using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MyWebApiShopContext _context;
        public RatingRepository(MyWebApiShopContext context)
        {
            _context = context;
        }
        public async Task<Rating> AddRating(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return rating;
        }
    }
}
