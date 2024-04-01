using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Services
{
    public interface IReviewRepository
    {
        Task<Reviews> GetReviewAsync(string reviewId);
        Task<IEnumerable<Reviews>> GetReviewsAsync();
        Task AddAsync(Reviews review);
        Task DeleteAsync(string reviewId);
        Task UpdateAsync(string reviewId, Reviews review);
        Task PatchAsync(string reviewId, Reviews reviewUpdateDTO);
    }
}
