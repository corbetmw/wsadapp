using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Controllers;
using wsad_app.Models.DataAccess;

namespace wsad_app.Models.Account
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {

        }
        public UserProfileViewModel(User userDTO)
        {
            EmailAddress = userDTO.EmailAddress;
            EmailOpt = userDTO.EmailOpt;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            UserName = userDTO.UserName;
            Id = userDTO.Id;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public bool EmailOpt { get; set; }
        public int Id { get; set; }
    }
}