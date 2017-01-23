using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Models.Helpers;
using MvcPL.Models.ViewModels;

namespace MvcPL.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
        private readonly IPhotoService _photoService;
        private readonly ITagService _tagService;


        public PhotoController(IUserService userService, IProfileService profileService, IPhotoService photoSevice,
            ITagService tagService)
        {
            _userService = userService;
            _profileService = profileService;
            _photoService = photoSevice;
            _tagService = tagService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            var photo = new AddPhotoViewModel();
            return View(photo);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(AddPhotoViewModel photo, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View(photo);

            if (file == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            if (photo.Tags != null)
                photo.Tags = photo.Tags.Trim();

            var model = new PhotoModel();
            var user = _userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
            var profile = _profileService.GetOneByPredicate(p => p.Id == user.Id);

                using (var img = Image.FromStream(file.InputStream))
                {
                    var cutImage = ImageTool.CutImage(img, 600, 600);
                    model.Image = ImageTool.ImageToByteArray(cutImage);
                }

                model.CreationTime = DateTime.Now.ToUniversalTime();
                model.Description = photo.Description;
                model.ProfileId = profile.Id;

                var tags = photo.Tags != null ? photo.Tags.ToLower().Split(' ') : new string[0];

                model.Tags = new List<TagModel>();
                foreach (var tag in tags)
                {
                    model.Tags.Add(new TagModel
                    {
                        Name = tag
                    });
                }

              
            _photoService.Create(model.ToBllPhoto());
            return RedirectToAction("UserPage", "User", new { login = user.Login });
        }

        [HttpGet]
        public ActionResult Gallery(int page, string pageName)
        {
            var photos = new PagedList<PhotoModel>
            {
                CurrentPage = page,
                PageName = pageName,
            };
            if (pageName.StartsWith("Profile"))
            {
                string username = pageName.Substring(7);
                var user = _userService.GetOneByPredicate(u => u.Login == username).ToMvcUser();

                if (user == null)
                {
                    return RedirectToAction("Error", "Error");
                }

                var profile = _profileService.GetById(user.Id).ToMvcProfile();

                photos.Content =
                    _photoService.GetAllByPredicate(p => p.ProfileId == profile.Id).Select(photo => photo.ToMvcPhoto()).ToList();
            }
            else if (pageName.StartsWith("Tag"))
            {
                string tag = pageName.Substring(3);
                photos.Content = _tagService.GetOneByPredicate(t => t.Name == tag).ToMvcTag().Photos.ToList();
            }
            else if (pageName.StartsWith("Main"))
            {
                photos.Content = _photoService.GetAllEntities()
                    .Select(photo => photo.ToMvcPhoto())
                    .ToList();
            }

            photos.Count = photos.Content.Count;

            photos.Content = photos.Content
                .Skip((photos.CurrentPage - 1) * ImageTool.PageSize)
                .Take(ImageTool.PageSize)
                .ToList();

            foreach (var like in photos.Content.SelectMany(photo => photo.Likes))
            {
                like.UserLogin = _userService.GetOneByPredicate(u => u.Id == like.ProfileId).Login;
            }

            foreach (var photo in photos.Content)
            {
                photo.UserLogin = _userService.GetOneByPredicate(u => u.Id == photo.ProfileId).Login;
            }


            return PartialView("_Gallery", photos);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult PhotosList()
        {
            var photos =
                _photoService.GetAllEntities()
                    .Select(photo => photo.ToMvcPhoto())
                    .ToList();

            var records = new PagedList<PhotoModel>
            {
                Content = photos.Take(ImageTool.PageSize).ToList(),
                CurrentPage = 1,
                PageName = "Main",
                Count = photos.Count
            };

            foreach (var like in records.Content.SelectMany(photo => photo.Likes))
            {
                like.UserLogin = _userService.GetOneByPredicate(u => u.Id == like.ProfileId).Login;
            }

            foreach (var photo in records.Content)
            {
                photo.UserLogin = _userService.GetOneByPredicate(u => u.Id == photo.ProfileId).Login;
            }

            return PartialView("_Gallery",records);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int photoId, int page, string pageName)
        {
            var photo = _photoService.GetById(photoId).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Error", "Error");

            }

            if (!User.IsInRole("Admin"))
            {
                if (photo.ProfileId == null)
                {
                    return RedirectToAction("Error", "Error");
                }

                var user = _userService.GetById(photo.ProfileId.GetValueOrDefault()).ToMvcUser();

                if (User.Identity.Name != user.Login)
                {
                    return RedirectToAction("Error", "Error");
                }
            }

            _photoService.Delete(photo.ToBllPhoto());
            return RedirectToAction("Gallery", new {Page = page, PageName = pageName});
        }
    }
}
