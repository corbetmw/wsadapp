using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wsad_app.Models.DataAccess
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key]
        [Column(Order = 0)]
        public int User_Id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int Role_Id { get; set; }

        [ForeignKey("Role_Id")]
        public virtual Role Role { get; set; }
        [ForeignKey("User_Id")]
        public virtual User User { get; set; }
        
    }
}