using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using wsad_app.Controllers;
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

        public Session GetSession(int sessionId)
        {
            //SELECT TOP 1 * FROM USERS WHERE ID = @userId
            return GetAllSessions().FirstOrDefault(row => row.Id == sessionId);
        }

        public Session AddSession(Session template)
        {
            using (wsadDbContext context = new wsadDbContext())
            {
                Session newSessionObj = context.Sessions.Add(template);

                context.SaveChanges();

                return newSessionObj;
            }
        }

        public Session UpdateSession(Session sessionToUpdate)
        {
            using (wsadDbContext context = new wsadDbContext())
            {
                //Get session From Database
                Session currentSessionDTO = context.Sessions.Find(sessionToUpdate.Id);

                //Copy Values
                currentSessionDTO.Title = sessionToUpdate.Title;
                currentSessionDTO.Description = sessionToUpdate.Description;
                currentSessionDTO.Building = sessionToUpdate.Building;
                currentSessionDTO.Room = sessionToUpdate.Room;
                currentSessionDTO.DateAndTime = sessionToUpdate.DateAndTime;
                currentSessionDTO.TotalSeats = sessionToUpdate.TotalSeats;

                //Save Changes
                context.SaveChanges();

                return currentSessionDTO;
            }
        }

        public bool DeleteSession(int sessionId)
        {
            //Check for registrations
            SessionCartManager sessMgr = new SessionCartManager();

            var registrations = sessMgr.GetAllUsersBySession(sessionId);

            if (registrations == null)
            {
               using (wsadDbContext context = new wsadDbContext())
                {
                    Session sessionDTO = context.Sessions.Find(sessionId);

                    context.Sessions.Remove(sessionDTO);

                    context.SaveChanges();
                }

                return true;
            }
            else
            {
                return false;
            }

        }


    }
}