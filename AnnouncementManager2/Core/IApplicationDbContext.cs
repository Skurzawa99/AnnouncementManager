using AnnouncementManager2.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementManager2.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Announcement> Announcements { get; set; }

        DbSet<Category> Categories { get; set; }

        int SaveChanges();
    }
}
