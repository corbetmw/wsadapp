using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Areas.Admin.Models.ViewModels.SessionManager
{
    public class SessionManager_CreateEditViewModel
    {
    }

    public SessionManager_CreateEditViewModel(Session sessionDTO)
    {
        Id = sessionDTO.Id;
        UserName = sessionDTO.UserName;
        FirstName = sessionDTO.FirstName;
        LastName = sessionDTO.LastName;
        EmailAddress = sessionDTO.EmailAddress;
        EmailOpt = sessionDTO.EmailOpt;
    }
}