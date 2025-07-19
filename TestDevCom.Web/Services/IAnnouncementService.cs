using TestDevCom.Web.Models;

namespace TestDevCom.Web.Services
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<Announcement?> GetByIdAsync(int id);
        Task<bool> CreateAsync(Announcement model);
        Task<bool> UpdateAsync(Announcement model);
        Task<bool> DeleteAsync(int id);
    }

}
