using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wsad_app.Models.DataAccess
{
    [Table("Session")]
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}