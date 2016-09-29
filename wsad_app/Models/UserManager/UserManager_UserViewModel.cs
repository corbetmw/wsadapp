using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Models.UserManager
{
    public class UserManager_UserViewModel
    {

        public UserManager_UserViewModel(User userDTO)
        {
            Id = userDTO.Id;
            UserName = userDTO.UserName;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            EmailAddress = userDTO.EmailAddress;
            EmailOpt = userDTO.EmailOpt;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool EmailOpt { get; set; }
    }
}