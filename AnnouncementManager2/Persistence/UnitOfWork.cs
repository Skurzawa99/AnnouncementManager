using AnnouncementManager2.Core;
using AnnouncementManager2.Core.Repositories;
using AnnouncementManager2.Persistence.Repositories;

namespace AnnouncementManager2.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;

        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            Announcement = new AnnouncementRepository(context);
        }

        public IAnnouncementRepository Announcement { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
