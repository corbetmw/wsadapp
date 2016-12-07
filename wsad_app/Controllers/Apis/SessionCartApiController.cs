using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wsad_app.Models.DataAccess;

namespace wsad_app.Controllers.Apis
{
    public class SessionCartApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<SessionCart> Get(string username)
        {
            SessionCartManager sessCrtMgr = new SessionCartManager();

            //Search username
            var results = sessCrtMgr.GetAllSessionsByUser(username,null);

            return results;
        }

        // GET api/<controller>/5
        public IEnumerable<SessionCart> Get(int id)
        {
            SessionCartManager sessCrtMgr = new SessionCartManager();

            //Search username
            var results = sessCrtMgr.GetAllUsersBySession(id);

            return results;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            SessionCartManager sessCrtMgr = new SessionCartManager();

            sessCrtMgr.DeleteRegistration(id);
        }
    }
}