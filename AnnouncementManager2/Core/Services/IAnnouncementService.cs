using AnnouncementManager2.Core.Models.Domains;

namespace AnnouncementManager2.Core.Services
{
    public interface IAnnouncementService
    {
        IEnumerable<Announcement> GetAnnouncements(string userId = null, int categoryId = 0, string title = null);

        Announcement GetAnnouncement(int id, string userId = null);

        void Add(Announcement announcement);

        void Update(Announcement announcement);

        void Delete(int id, string userId);

        IEnumerable<Category> GetCategories();

        string UploadFile(IFormFile image);
    }
}
