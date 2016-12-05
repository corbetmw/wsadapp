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
    public class SessionApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Session> Get()
        {
            //Get Session Manager
            ScheduleManager sessMgr = new ScheduleManager();

            //Get All Sessions
            var allSessions = sessMgr.GetAllSessions();

            //Return all Sessions
            return allSessions;
        }

        // GET api/<controller>/5
        public Session Get(int id)
        {
            //User Manager
            ScheduleManager sessMgr = new ScheduleManager();
            //Get User By Id
            var matchingSession = sessMgr.GetSession(id);

            //Return User Match
            if (matchingSession != null)
            {
                return matchingSession;
            }
            else
            {
                return null;
            }
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
        }
    }
}