using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilTeamProject.Models.Employees
{
    public class Request
    {
        [Key]
        [Column(Order = 1)]
        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }

        [Key]
        [Column(Order = 2)]
        public int LeaveID { get; set; }

        public virtual Leave Leave { get; set; }

        public DateTime DateRequestedLeave { get; set; }
        public bool IsAccepted { get; set; }
    }
}