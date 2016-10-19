using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Models.Schedule
{
    public class SessionViewModel
    {
        public SessionViewModel()
        {

        }
        public SessionViewModel(Session s)
        {
            Id = s.Id;
            Title = s.Title;
            TotalSeats = s.TotalSeats;
            Building = s.Building;
            Room = s.Room;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int TotalSeats { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public bool IsSelected { get; set; }
    }
}