using AnnouncementManager2.Core;
using AnnouncementManager2.Core.Models.Domains;
using AnnouncementManager2.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementManager2.Persistence.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private IApplicationDbContext _context;
        public AnnouncementRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Announcement> GetAnnouncements(string userId = null, int categoryId = 0, string title = null)
        {
            var userAnnouncements = _context.Announcements
                .Include(x => x.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(userId))
                userAnnouncements = userAnnouncements.Where(x => x.UserId == userId);

            if (categoryId != 0)
                userAnnouncements = userAnnouncements.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                userAnnouncements = userAnnouncements.Where(x => x.Subject.Contains(title));

            return userAnnouncements.OrderBy(x => x.StartAnnouncement).ToList();
        }

        public Announcement GetAnnouncement(int id, string userId = null)
        {
            var announcement = _context.Announcements.Single(x => x.Id == id);

            if (!string.IsNullOrWhiteSpace(userId))
                announcement = _context.Announcements.Single(x => x.Id == id && x.UserId == userId);

            return announcement;
        }

        public void Add(Announcement announcement)
        {
            announcement.StartAnnouncement = DateTime.Now;
            _context.Announcements.Add(announcement);
        }

        public void Update(Announcement announcement)
        {
            var announcementToUpdate = _context.Announcements.Single(x => x.Id == announcement.Id);

            announcementToUpdate.ImageName = announcement.ImageName;
            announcementToUpdate.Subject = announcement.Subject;
            announcementToUpdate.Body = announcement.Body;
            announcementToUpdate.Price = announcement.Price;
            announcementToUpdate.CategoryId = announcement.CategoryId;
            announcementToUpdate.StartAnnouncement = DateTime.Now;

            _context.Announcements.Update(announcementToUpdate);
        }

        public void Delete(int id, string userId)
        {
            var announcementToDelete = _context.Announcements.Where(x => x.Id == id && x.UserId == userId).Single();

            _context.Announcements.Remove(announcementToDelete);
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _context.Categories
                .ToList();

            return categories;
        }
    }
}
