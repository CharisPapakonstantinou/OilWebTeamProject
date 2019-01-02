using OilTeamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.Models.Employees
{
    public class Employee
    {
        //comment
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public double Salary { get; set; }

        public ICollection<Performance> Performances { get; set; }

        public virtual PersonalDetails PersonalDetails { get; set; }

        public virtual ContactDetails ContactDetails { get; set; }

        public ICollection<Work> Works { get; set; }

        public ICollection<Request> Requests { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

        public int RemaingDaysOfLeave { get; set; } = 23;

        public Employee()
        {
            Works = new Collection<Work>();
            Requests = new Collection<Request>();
        }

        public Employee(EmployeeFormViewModel viewModel)
        {
            FirstName = viewModel.FirstName;
            LastName = viewModel.LastName;
            RoleId = viewModel.RoleId;
            DepartmentId = viewModel.DepartmentId;
            PersonalDetails = viewModel.PersonalDetails;
            ContactDetails = viewModel.ContactDetails;
            Salary = viewModel.Salary;

            Works = new Collection<Work>();
        }

        public void EmployeeModify(EmployeeFormViewModel viewModel)
        {
            FirstName = viewModel.FirstName;
            LastName = viewModel.LastName;
            RoleId = viewModel.RoleId;
            DepartmentId = viewModel.DepartmentId;
            PersonalDetails = viewModel.PersonalDetails;
            ContactDetails = viewModel.ContactDetails;
            Salary = viewModel.Salary;
            RemaingDaysOfLeave = viewModel.RemainingDaysOfLeave;
        }

        //methods
        //Employee Add Shift to Employee

        public void AddEmployeeToShift(Shift shift)
        {
            var work = new Work(this.Id, shift.Id);
            Works.Add(work);
        }

        //Make A Leave Request
        public Request MakeARequestForLeave(Leave leave)
        {
            var request = new Request()
            {
                Employee = this,
                Leave = leave,
                DateRequestedLeave = DateTime.Now.Date,
                IsAccepted = true
            };
            if (RemaingDaysOfLeave < leave.HowManyDays || leave.HowManyDays <= 0)
                request.IsAccepted = false;

            if (request.IsAccepted)
                RemaingDaysOfLeave -= leave.HowManyDays;

            return request;
        }
    }
}