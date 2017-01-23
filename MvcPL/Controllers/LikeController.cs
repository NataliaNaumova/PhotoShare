using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
        private readonly IPhotoService _photoService;


        public LikeController(IUserService userService, IProfileService profileService, IPhotoService photoSevice)
        {
            _userService = userService;
            _profileService = profileService;
            _photoService = photoSevice;
        }

        public ActionResult Like(int photoId)
        {
            var user = _userService.GetOneByPredicate(u => u.Login == User.Identity.Name).ToMvcUser();
            var profile = _profileService.GetOneByPredicate(p => p.Id == user.Id).ToMvcProfile();

            var photo = _photoService.GetById(photoId).ToMvcPhoto();

            var like = new LikeModel()
            {
                PhotoId = photoId,
                ProfileId = profile.Id
            };
            if (photo.Likes.FirstOrDefault(l => l.ProfileId == profile.Id) != null)
            {
                _photoService.RemoveLike(like.ToBllLike());
            }
            else
            {
                _photoService.AddLike(like.ToBllLike());
            }

            photo = _photoService.GetById(photoId).ToMvcPhoto();

            foreach (var photoLike in photo.Likes)
            {
                photoLike.UserLogin = _userService.GetOneByPredicate(u => u.Id == like.ProfileId).Login;
            }

            return PartialView("_Like", photo);
        }

    }
}
