using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using wsad_app.Models.Business;
using wsad_app.Models.DataAccess;

namespace wsad_app
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    //Is Authenticated?
        //    if (this.Context.Request.IsAuthenticated == false) { return ; }

        //    //Get Current user
        //    string currentUsername = this.Context.User.Identity.Name;

        //    //Get USer manger
        //    UserManager userMgr = new UserManager();

        //    //Get user form manager
        //    User usr = userMgr.GetAllUsers().FirstOrDefault(row => row.UserName == currentUsername);

        //    //Get user_roles from manager
        //    IQueryable<User_Role> allUserRoles = userMgr.GetUserRoles(usr.Id);

        //    //create identitiy obeckt
        //    System.Security.Principal.GenericIdentity identity;
        //    identity = new System.Security.Principal.GenericIdentity(currentUsername);

        //    //add reoles to principle
        //    string[] roles;
        //    roles = allUserRoles.Select(ur => ur.Role.Name).ToArray();

        //    //Create principle object
        //    System.Security.Principal.GenericPrincipal principal;
        //    principal = new System.Security.Principal.GenericPrincipal(identity, roles);

        //    //set principal as new user
        //    this.Context.User = principal;
        //}
    }
}
