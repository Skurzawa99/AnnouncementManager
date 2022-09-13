using AnnouncementManager2.Core.Models.Domains;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AnnouncementManager2.Core.ViewModels
{
    public class AnnouncementViewModel
    {
        public string Heading { get; set; }
        public Announcement Announcement { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        [Required(ErrorMessage = "Zdjęcie wymagane.")]
        [Display(Name = "Nazwa zdjęcia")]
        public IFormFile Image { get; set; }
    }
}
