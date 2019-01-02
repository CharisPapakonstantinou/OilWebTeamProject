using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.Models.Employees
{
    public class Shift
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public byte ShiftTypeId { get; set; }

        public ShiftType ShiftType { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<Work> Works { get; set; }

        //Constractors

        public Shift()
        {
            Works = new Collection<Work>();
        }

        public Shift(DateTime dateTime, byte shiftTypeId, int departmentId)
        {
            DateTime = dateTime;
            ShiftTypeId = shiftTypeId;
            DepartmentId = departmentId;

            Works = new Collection<Work>();
        }
    }
}