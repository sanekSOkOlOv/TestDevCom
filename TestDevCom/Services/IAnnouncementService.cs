using TestDevCom.Models;

namespace TestDevCom.Services
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAllAsync(string? category = null, string? subCategory = null);
        Task AddAsync(Announcement ann);
        Task UpdateAsync(Announcement ann);
        Task DeleteAsync(int id);
    }
}
