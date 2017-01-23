using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
        private readonly IPhotoService _photoService;

        public UserController(IUserService userService, IProfileService profileService, IPhotoService photoSevice)
        {
            _userService = userService;
            _profileService = profileService;
            _photoService = photoSevice;
        }

        public ActionResult UserPage(string login)
        {
            var user = _userService.GetOneByPredicate(u => u.Login == login).ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("Error", "Error");
            }

            var profile = _profileService.GetById(user.Id).ToMvcProfile();

            var photos =
                _photoService.GetAllByPredicate(p => p.ProfileId == profile.Id)
                    .Select(photo => photo.ToMvcPhoto())
                    .ToList();

            var records = new PagedList<PhotoModel>
            {
                Content = photos.Take(ImageTool.PageSize).ToList(),
                CurrentPage = 1,
                PageName = "Profile" + user.Login,
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

            var model = new UserPageViewModel
            {
                User = user,
                Profile = profile,
                Photos = records
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ProfileEdit()
        {
            var user = _userService.GetOneByPredicate(u => u.Login == User.Identity.Name).ToMvcUser();
            var profile = _profileService.GetById(user.Id).ToMvcProfile();

            var model = new ProfileEditViewModel
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Avatar = profile.Avatar
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ProfileEdit(ProfileEditViewModel viewModel, HttpPostedFileBase file)
        {
            var user = _userService.GetOneByPredicate(u => u.Login == User.Identity.Name).ToMvcUser();

            var profile = _profileService.GetById(user.Id).ToMvcProfile();
            if (file != null && file.ContentLength > 0)
            {
                var target = new MemoryStream();
                file.InputStream.CopyTo(target);
                var byteArrayIn = target.ToArray();
                Image image = null;
                using (var ms = new MemoryStream(byteArrayIn))
                {
                    image = Image.FromStream(ms);
                }
                profile.Avatar = ImageTool.ImageToByteArray(ImageTool.CutImage(image, 200, 200));
            }

            profile.FirstName = viewModel.FirstName ?? profile.FirstName;
            profile.LastName = viewModel.LastName ?? profile.LastName;

            _profileService.Update(profile.ToBllProfile());
            return RedirectToAction("UserPage", new { login = user.Login });
        }

        [HttpGet]
        public ActionResult DeleteAvatar()
        {
            var user = _userService.GetOneByPredicate(u => u.Login == User.Identity.Name).ToMvcUser();

            var profile = _profileService.GetById(user.Id).ToMvcProfile();

            if (profile.Avatar != null)
            {
                profile.Avatar = null;
            }

            _profileService.Update(profile.ToBllProfile());
            return RedirectToAction("UserPage", new { login = user.Login });
        }

    }
}
