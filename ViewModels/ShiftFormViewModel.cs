using OilTeamProject.Models.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.ViewModels
{
    public class ShiftFormViewModel
    {
        public int ShiftId { get; set; }

        [Required]
        [Display(Name = "Type Of Shift")]
        public byte ShiftTypeId { get; set; }

        public IEnumerable<ShiftType> ShiftTypes { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public IEnumerable<Department> Departments { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Day")]
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0}", Date));
        }

        public bool IsValidDayComparedToDate()
        {
            DateTime dateTime = GetDateTime();
            if (dateTime.Date.DayOfWeek == DayOfWeek)
                return true;
            else
                return false;
        }
    }
}