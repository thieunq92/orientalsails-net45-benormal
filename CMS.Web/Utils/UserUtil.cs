using CMS.Core.Domain;
using CMS.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Utils
{
    public class UserUtil
    {
        public UserRepository UserRepository { get; set; }

        public UserUtil()
        {
            UserRepository = new UserRepository();
        }

        public void Dispose()
        {
            if (UserRepository != null)
            {
                UserRepository.Dispose();
                UserRepository = null;
            }
        }

        public User UserGetCurrent()
        {
            return UserRepository.UserGetById(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
        }


        public string UserCurrentGetName()
        {
            return UserGetCurrent().FullName;
        }

        public object UserGetByRole(int roleId)
        {
            return UserRepository.UserGetByRole(roleId);
        }
    }
}