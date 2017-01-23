using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcPL.Models.ViewModels;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Helpers;
using MvcPL.Providers;
using WebMatrix.WebData;


namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _service;

        public AccountController(IUserService service)
        {
            this._service = service;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Login, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "The user login or password provided is incorrect.");
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (_service.GetOneByPredicate(u => u.Email == viewModel.Email) != null)
            {
                ModelState.AddModelError("", "User with this e-mail is already registered.");
                return View(viewModel);
            }
            if (_service.GetOneByPredicate(u => u.Login == viewModel.Login) != null)
            {
                ModelState.AddModelError("", "User with this login is already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Email, viewModel.Login, viewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Error registration.");
            }
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult CheckLoginUniqueness(string login)
        {
            bool result = ((CustomMembershipProvider)Membership.Provider).GetUser(login, false) == null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult CheckEmailUniqueness(string email)
        {
            bool result = ((CustomMembershipProvider)Membership.Provider).GetUserNameByEmail(email) == null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
