using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using System.Linq;

namespace OilTeamProject.Repositories
{
    public class WorkRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Work work)
        {
            _context.Works.Add(work);
        }

        public void Cancel(Work work)
        {
            _context.Works.Remove(work);
        }

        public Work Work(int employeeId, int shiftId)
        {
            return _context.Works
                .Where(w => w.EmployeeID == employeeId && w.ShiftId == shiftId)
                .Single();
        }

        public bool GetWork(int employee, int shift)
        {
            return _context.Works.Any(w => w.EmployeeID == employee && w.ShiftId == shift);
        }
    }
}