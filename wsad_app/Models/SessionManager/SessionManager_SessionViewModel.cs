using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Models.SessionManager
{
    public class SessionManager_SessionViewModel
    {
        public SessionManager_SessionViewModel()
        {

        }
        public SessionManager_SessionViewModel(Session sessionDTO)
        {
            Title = sessionDTO.Title;
            Description = sessionDTO.Description;
            Building = sessionDTO.Building;
            Room = sessionDTO.Room;
            TotalSeats = sessionDTO.TotalSeats;
            AvailableSeats = sessionDTO.AvailableSeats;
            DateAndTime = sessionDTO.DateAndTime;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime DateAndTime { get; set; }

        public bool IsSelected { get; set; }
    }
}