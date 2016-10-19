using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.Schedule;

namespace wsad_app.Models.SessionCart
{
    public class SessionCartViewModel
    {
        public SessionCartViewModel(DataAccess.SessionCart item)
        {
            Id = item.Id;
            UserId = item.UserId;
            SessionId = item.SessionId;
            DateAdded = item.DateAdded;
            IsActive = item.IsActive;
            Session = new SessionViewModel(item.Session);
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsActive { get; set; }

        public SessionViewModel Session { get; set; }
    }
}