using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsad_app.Models.Account
{
    public class AccountCreateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool EmailOpt { get; set; }
    }
}