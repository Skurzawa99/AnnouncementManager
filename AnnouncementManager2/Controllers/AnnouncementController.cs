using AnnouncementManager2.Core.Models.Domains;
using AnnouncementManager2.Core.Models;
using AnnouncementManager2.Core.ViewModels;
using AnnouncementManager2.Persistence.Extensions;
using AnnouncementManager2.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AnnouncementManager2.Persistence.Repositories;
using AnnouncementManager2.Persistence.Services;
using AnnouncementManager2.Core.Services;

namespace AnnouncementManager2.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {

        private IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [AllowAnonymous]
        public IActionResult Announcements()
        {
            var vm = new AnnouncementsViewModel
            {
                FilterAnnouncements = new FilterAnnouncements(),
                Announcements = _announcementService.GetAnnouncements(),
                Categories = _announcementService.GetCategories()
            };

            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Announcements(AnnouncementsViewModel viewModel)
        {
            var announcements = _announcementService.GetAnnouncements(
                default,
                viewModel.FilterAnnouncements.CategoryId,
                viewModel.FilterAnnouncements.Title);

            return PartialView("_AnnouncementTable", announcements);
        }

        public IActionResult Announcement(int id = 0)
        {
            var userId = User.GetUserId();

            var announcement = id == 0 ? 
                new Announcement { Id = 0, UserId = userId, StartAnnouncement = DateTime.Now } 
                : _announcementService.GetAnnouncement(id, userId);

            var vm = new AnnouncementViewModel
            {
                Announcement = announcement,
                Heading = id == 0 ? "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia",
                Categories = _announcementService.GetCategories()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Announcement(AnnouncementViewModel viewModel)
        {
            viewModel.Announcement.UserId = User.GetUserId();
            viewModel.Announcement.UserName = User.Identity.Name;
            viewModel.Announcement.ImageName = _announcementService.UploadFile(viewModel.Image);

            if (!ModelState.IsValid)
            {
                viewModel.Heading = viewModel.Announcement.Id == 0 ?
                "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia";
                viewModel.Categories = _announcementService.GetCategories();

                return View("Announcement", viewModel);
            }

            if (viewModel.Announcement.Id == 0)
                _announcementService.Add(viewModel.Announcement);
            else
                _announcementService.Update(viewModel.Announcement);

            return RedirectToAction("Announcements");
        }

        public IActionResult UserAnnouncements()
        {
            var userId = User.GetUserId();

            var vm = new AnnouncementsViewModel
            {
                FilterAnnouncements = new FilterAnnouncements(),
                Announcements = _announcementService.GetAnnouncements(userId),
                Categories = _announcementService.GetCategories()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult UserAnnouncements(AnnouncementsViewModel viewModel)
        {
            var userId = User.GetUserId();

            var announcements = _announcementService.GetAnnouncements(
                userId,
                viewModel.FilterAnnouncements.CategoryId,
                viewModel.FilterAnnouncements.Title);

            return PartialView("_UserAnnouncementTable", announcements);
        }

        [HttpPost]
        public IActionResult DeleteAnnouncement(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _announcementService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SelectCategory(int id)
        {
            var announcements = _announcementService.GetAnnouncements(null, id);

            return PartialView("_AnnouncementTable", announcements);
        }

        [AllowAnonymous]
        public IActionResult AnnouncementWindow(int id)
        {
            var announcement = _announcementService.GetAnnouncement(id);

            return View("AnnouncementWindow", announcement);
        }
    }
}
