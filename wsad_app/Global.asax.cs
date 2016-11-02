using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using wsad_app.Models.Business;
using wsad_app.Models.DataAccess;
using System.Web.Http;
using System.Web.Routing;

namespace wsad_app
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //Is Authenticated?
            if (this.Context.Request.IsAuthenticated == false) { return; }

            //Get Current User
            string currentUsername = this.Context.User.Identity.Name;

            //GetUserManager
            UserManager userMgr = new UserManager();

            //Get USer from Manager
            User usr = userMgr.GetAllUsers().FirstOrDefault(row => row.UserName == currentUsername);

            //Get User_Roles from Manager
            IEnumerable<UserRole> allUsersRoles = userMgr.GetUserRoles(usr.Id);

            //Create Identity Object
            System.Security.Principal.GenericIdentity identity;
            identity = new System.Security.Principal.GenericIdentity(currentUsername);

            //Get Roles as an array of string
            string[] roles;
            roles = allUsersRoles.Select(ur => ur.Role.Name).ToArray();

            //Create Principal Object
            System.Security.Principal.GenericPrincipal principal;
            principal = new System.Security.Principal.GenericPrincipal(identity, roles);

            //Set Principal as new User
            this.Context.User = principal;
        }
    }
}
