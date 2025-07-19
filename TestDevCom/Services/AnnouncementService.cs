
using TestDevCom.Data;
using TestDevCom.Models;

namespace TestDevCom.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ISqlExecutor _executor;

        public AnnouncementService(ISqlExecutor executor)
        {
            _executor = executor;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync(string? category, string? subCategory)
        {
            return await _executor.QueryAsync("storproc_GetAnnouncements", AnnouncementMapper.Map, new()
        {
            { "@Category", category },
            { "@SubCategory", subCategory }
        });
        }

        public async Task AddAsync(Announcement ann)
        {
            await _executor.ExecuteAsync("storproc_InsertAnnouncement", new()
        {
            { "@Title", ann.Title },
            { "@Description", ann.Description },
            { "@Status", ann.Status },
            { "@Category", ann.Category.ToString() },
            { "@SubCategory", ann.SubCategory.ToString() }
        });
        }

        public async Task UpdateAsync(Announcement ann)
        {
            await _executor.ExecuteAsync("storproc_UpdateAnnouncement", new()
        {
            { "@Id", ann.Id },
            { "@Title", ann.Title },
            { "@Description", ann.Description },
            { "@Status", ann.Status },
            { "@Category", ann.Category.ToString() },
            { "@SubCategory", ann.SubCategory.ToString() }
        });
        }

        public async Task DeleteAsync(int id)
        {
            await _executor.ExecuteAsync("storproc_DeleteAnnouncement", new()
        {
            { "@Id", id }
        });
        }

    }
}
