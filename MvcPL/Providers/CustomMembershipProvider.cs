using System;
using System.Web.Helpers;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Providers
{
    //провайдер членства помогает системе идентифицировать пользователя
    public class CustomMembershipProvider : MembershipProvider
    {
        public IUserService UserService
        {
            get { return (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService)); }
        }

        public IRoleService RoleService
        {
            get { return (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService)); }
        }

        public IProfileService ProfileService
        {
            get
            {
                return (IProfileService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IProfileService));
            }
        }

        public MembershipUser CreateUser(string email, string login, string password)
        {
            MembershipUser membershipUser = GetUser(login, false);

            if (membershipUser != null)
            {
                return null;
            }

            var sameEmailUser = UserService.GetOneByPredicate(u => u.Email == email);

            if (sameEmailUser != null)
            {
                return null;
            }

            var user = new UserModel()
            {
                Email = email,
                Login = login,
                Password = Crypto.HashPassword(password),
                RoleId = RoleService.GetOneByPredicate(role => role.Name == "User").Id
            };


            UserService.Create(user.ToBllUser());

            user = UserService.GetOneByPredicate(u => u.Login == login).ToMvcUser();
            ProfileService.Create(new ProfileEntity()
            {
                Id = user.Id
            });

            membershipUser = GetUser(login, false);
            return membershipUser;
        }

        public override bool ValidateUser(string login, string password)
        {
            var user = UserService.GetOneByPredicate(u => u.Login == login).ToMvcUser();

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            //Определяет, соответствуют ли заданный хэш RFC 2898 и пароль друг другу
            {
                return true;
            }
            return false;
        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            var user = UserService.GetOneByPredicate(excitingUser => excitingUser.Login == login).ToMvcUser();

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Login,
                null, null, null, null,
                false, false, default(DateTime),
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }


        public override string GetUserNameByEmail(string email)
        {
            var user = UserService.GetOneByPredicate(excitingUser => excitingUser.Email == email).ToMvcUser();

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Login,
                null, null, null, null,
                false, false, default(DateTime),
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser.UserName;
        }

        #region Stubs

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}