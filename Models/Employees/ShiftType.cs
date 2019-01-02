using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.Models.Employees
{
    public class ShiftType
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}