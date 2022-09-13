using AnnouncementManager2.Core.Repositories;
using AnnouncementManager2.Persistence.Repositories;

namespace AnnouncementManager2.Core
{
    public interface IUnitOfWork
    {
        IAnnouncementRepository Announcement { get; }
        void Complete();
    }
}
