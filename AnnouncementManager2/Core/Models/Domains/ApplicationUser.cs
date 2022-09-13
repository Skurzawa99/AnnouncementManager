using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace AnnouncementManager2.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Announcements = new Collection<Announcement>();
        }

        public ICollection<Announcement> Announcements { get; set; }
    }
}
