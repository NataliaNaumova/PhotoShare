using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models.ViewModels
{
    public class UserListViewModel
    {
        public ICollection<UserModel> Users { get; set; }

        public ICollection<ProfileModel> Profiles { get; set; }

        public ICollection<RoleModel> Roles { get; set; }
    }
}