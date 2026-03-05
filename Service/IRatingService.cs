using Entities;

namespace Service
{
    public interface IRatingService
    {
        Task<Rating> AddRating(Rating rating);
    }
}