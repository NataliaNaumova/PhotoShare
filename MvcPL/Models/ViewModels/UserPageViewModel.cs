using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models.Helpers;

namespace MvcPL.Models.ViewModels
{
    public class UserPageViewModel
    {
        public UserModel User { get; set; }
        public ProfileModel Profile { get; set; }

        public PagedList<PhotoModel> Photos { get; set; }
    }
}