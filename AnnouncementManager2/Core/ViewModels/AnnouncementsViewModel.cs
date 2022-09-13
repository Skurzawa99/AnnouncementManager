using AnnouncementManager2.Core.Models.Domains;
using AnnouncementManager2.Core.Models;

namespace AnnouncementManager2.Core.ViewModels
{
    public class AnnouncementsViewModel
    {
        public IEnumerable<Announcement> Announcements { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public FilterAnnouncements FilterAnnouncements { get; set; }
    }
}
