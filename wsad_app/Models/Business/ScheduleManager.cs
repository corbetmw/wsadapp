using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Models.Business
{
    public class ScheduleManager
    {
        internal IQueryable<Session> GetAllSessions(bool asNoTracking = false)
        {
            wsadDbContext context = new wsadDbContext();

            IQueryable<Session> results = context.Sessions;

            if (asNoTracking == false)
            {
                results = results.AsNoTracking();
            }

            return results;
        }
    }
}