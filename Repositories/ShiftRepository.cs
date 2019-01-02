using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OilTeamProject.Repositories
{
    public class ShiftRepository
    {
        private ApplicationDbContext _context;

        public ShiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Shift GetFirstShift(List<Shift> shifts)
        {
            return shifts.First();
        }

        public int GetShiftWithDepartmentYearMonth(int newDateTime2, int newDateTime1, int newDateTime3, int shiftTypeId, int shiftDepartmentId)
        {
            return _context.Shifts
                .Where(s => s.DateTime.Day == newDateTime2 &&
                s.DateTime.Month == newDateTime1 &&
                s.DateTime.Year == newDateTime3 &&
                s.ShiftType.Id == shiftTypeId &&
                s.Department.Id == shiftDepartmentId)
                .Single().Id;
        }

        public List<Shift> GetShiftsWithWorkSelectedEmployeeWithDateTimeAndDepartment(DateTime dateTime, int departmentId)
        {
            return _context.Shifts
                                .Include(s => s.Department)
                                .Include(s => s.ShiftType)
                                .Include(s => s.Works)
                                .Include(s => s.Works.Select(w => w.Employee))
                                .Where(s => s.DateTime == dateTime && s.DepartmentId == departmentId)
                                .ToList();
        }

        public List<byte> GetShiftTypesFromShifts(DateTime? dateTime, int? departmentId)
        {
            return _context.Shifts
                .Where(s => s.DateTime == dateTime && s.DepartmentId == departmentId)
                .Select(s => s.ShiftTypeId).ToList();
        }

        public bool GetShiftExist(WorkDayViewModel viewModel)
        {
            return _context.Shifts
                .Include(s => s.Department)
                .Include(s => s.ShiftType)
                .Any(s => s.DateTime == viewModel.WorkDate && s.DepartmentId == viewModel.DepartmentId);
        }

        public Shift GetShift()
        {
            return _context.Shifts
                    .Include(s => s.Department)
                    .Include(s => s.ShiftType)
                    .Single();
        }

        public Shift GetShift(int? id)
        {
            return _context.Shifts
                .Include(s => s.Department)
                .Include(s => s.ShiftType)
                .SingleOrDefault(s => s.Id == id);
        }

        public int GetShiftIdByDayTimeAndDepartmentId(DateTime datetime, int departmentId, int shiftTypeId)
        {
            return _context.Shifts
                .Where(s => s.DateTime == datetime &&
                s.DepartmentId == departmentId &&
                s.ShiftTypeId == shiftTypeId)
                .Single().Id;
        }
    }
}