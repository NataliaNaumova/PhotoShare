using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Models.ViewModels;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
        private readonly IRoleService _roleService;

        public AdminController(IUserService userService, IProfileService profileService, IRoleService roleService)
        {
            _userService = userService;
            _profileService = profileService;
            _roleService = roleService;
        }
        
        [HttpGet]
        public ActionResult UserListPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(int userId)
        {
            var user = _userService.GetById(userId).ToMvcUser();

            if (user == null)
            {
                RedirectToAction("Error", "Error");
            }

            _userService.Delete(user.ToBllUser());

            return UserList();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult UserList()
        {
            var users = _userService.GetAllEntities().Select(u => u.ToMvcUser()).ToList();

            var profiles = _profileService.GetAllEntities().Select(p => p.ToMvcProfile()).ToList();

            var roles = _roleService.GetAllEntities().Select(r => r.ToDalRole()).ToList();

            var model = new UserListViewModel()
            {
                Users = users,
                Profiles = profiles,
                Roles = roles
            };
            return PartialView("_UserList",model);
        }

    }
}
