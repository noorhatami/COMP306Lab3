using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Services
{
    public interface IBookRepository
    {
        Task<Books> GetBookAsync(string bookId);
        Task<IEnumerable<Books>> GetBooksAsync();
        Task AddAsync(Books book);
        Task DeleteAsync(string bookId);
        Task UpdateAsync(string bookId, Books book);
        Task PatchAsync(string bookId, Books bookUpdateDTO);
    }
}
