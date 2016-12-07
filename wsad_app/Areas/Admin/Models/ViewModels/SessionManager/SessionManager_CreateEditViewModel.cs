using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Areas.Admin.Models.ViewModels.SessionManager
{
    public class SessionManager_CreateEditViewModel
    {

        public SessionManager_CreateEditViewModel()
        {
        }
        public SessionManager_CreateEditViewModel(Session sessionDTO)
        {
            Id = sessionDTO.Id;
            Title = sessionDTO.Title;
            Description = sessionDTO.Description;
            Building = sessionDTO.Building;
            Room = sessionDTO.Room;
            TotalSeats = sessionDTO.TotalSeats;
            AvailableSeats = sessionDTO.AvailableSeats;
            DateAndTime = sessionDTO.DateAndTime;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime DateAndTime { get; set; }

    }

}