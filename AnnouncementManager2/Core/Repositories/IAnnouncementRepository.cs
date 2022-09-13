using AnnouncementManager2.Core.Models.Domains;

namespace AnnouncementManager2.Core.Repositories
{
    public interface IAnnouncementRepository
    {
        IEnumerable<Announcement> GetAnnouncements(string userId = null, int categoryId = 0, string title = null);

        Announcement GetAnnouncement(int id, string userId = null);

        IEnumerable<Category> GetCategories();

        void Add(Announcement announcement);

        void Update(Announcement announcement);

        void Delete(int id, string userId);
    }
}
