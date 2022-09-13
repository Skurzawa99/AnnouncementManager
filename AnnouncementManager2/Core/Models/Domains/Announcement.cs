using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AnnouncementManager2.Core.Models.Domains
{
    public class Announcement
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [Display(Name = "Tytuł")]
        public string Subject { get; set; }

        [MinLength(50)]
        [Required(ErrorMessage = "Opis musi zawierać minimum 50 znaków.")]
        [Display(Name = "Opis")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Kategoria jest wymagana.")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public DateTime StartAnnouncement { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
    }
}
