using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wsad_app.Models.Business;
using wsad_app.Models.DataAccess;

namespace wsad_app.Controllers.Apis
{
    public class UserApiController : ApiController
    {
        // GET: api/UserApi
        public IEnumerable<User> Get()
        {
            //Get User Manager
            UserManager userMgr = new UserManager();

            //Get All Users
            var allUsers = userMgr.GetAllUsers();

            //Return all Users
            return allUsers;
        }

        // GET: api/UserApi/5
        public User Get(int id)
        {
            //User Manager
            UserManager userMgr = new UserManager();
            //Get User By Id
            var matchingUser = userMgr.GetUser(id);

            //Return User Match
            if (matchingUser != null)
            {
                return matchingUser;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<User> Get(string query)
        {
            UserManager userMgr = new UserManager();

            //Search on Firstname, Lastname, EmailAddress, Username
            var results = userMgr.GetAllUsers().Where(row =>
                row.FirstName.Contains(query) ||
                row.LastName.Contains(query) ||
                row.EmailAddress.Contains(query) ||
                row.UserName.Contains(query)
            );

            //return results
            return results;
        }

        // POST: api/UserApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserApi/5
        public void Put(int id, [FromBody]string value)
        {
            
        }

        // DELETE: api/UserApi/5
        public void Delete(int id)
        {
        }
    }
}
