using System;
using System.Collections.Generic;
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
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;

        public TagController(ITagService tagService, IPhotoService photoService, IUserService userService)
        {
            _tagService = tagService;
            _photoService = photoService;
            _userService = userService;
        }

        public ActionResult Search(string tagSearched)
        {
            if (tagSearched == null)
            {
                return View();
            }

            tagSearched = tagSearched.Replace(" ", string.Empty).ToLower();
            var tag = _tagService.GetOneByPredicate(t => t.Name == tagSearched).ToMvcTag();

            if (tag == null)
            {
                return View();
            }

            var records = new PagedList<PhotoModel>
            {
                Content = tag.Photos.Take(ImageTool.PageSize).ToList(),
                CurrentPage = 1,
                PageName = "Tag" + tag.Name,
                Count = tag.Photos.Count
            };

            var model = new TagViewModel()
            {
                Tag = tag,
                Photos = records
            };

            foreach (var like in records.Content.SelectMany(photo => photo.Likes))
            {
                like.UserLogin = _userService.GetOneByPredicate(u => u.Id == like.ProfileId).Login;
            }

            foreach (var photo in records.Content)
            {
                photo.UserLogin = _userService.GetOneByPredicate(u => u.Id == photo.ProfileId).Login;
            }
            return View(model);
        }

    }
}
