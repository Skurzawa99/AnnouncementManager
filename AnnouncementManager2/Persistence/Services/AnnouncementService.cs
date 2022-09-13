using AnnouncementManager2.Core;
using AnnouncementManager2.Core.Models.Domains;
using AnnouncementManager2.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AnnouncementManager2.Persistence.Services
{
    public class AnnouncementService : IAnnouncementService                                     
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnnouncementService(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IEnumerable<Announcement> GetAnnouncements(string userId = null, int categoryId = 0, string title = null)
        {
            return _unitOfWork.Announcement.GetAnnouncements(userId, categoryId, title);
        }

        public Announcement GetAnnouncement(int id, string userId = null)
        {
            return _unitOfWork.Announcement.GetAnnouncement(id, userId);
        }

        public void Add(Announcement announcement)
        {
            _unitOfWork.Announcement.Add(announcement);
            _unitOfWork.Complete();
        }

        public void Update(Announcement announcement)
        {
            _unitOfWork.Announcement.Update(announcement);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Announcement.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Announcement.GetCategories();
        }

        public string UploadFile(IFormFile image)
        {
            string fileName = null;
            if (image != null)
            {
                string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return fileName;
        }

    }
}
