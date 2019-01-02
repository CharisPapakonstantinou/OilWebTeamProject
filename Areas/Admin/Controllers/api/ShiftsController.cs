using OilTeamProject.Persistence;
using System.Linq;
using System.Web.Http;

namespace OilTeamProject.Areas.Admin.Controllers.api
{
    public class ShiftsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ShiftsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(string id)
        {
            string myId = id;
            string[] stringId = myId.Split();

            var employeeId = int.Parse(stringId[1]);
            var shiftId = int.Parse(stringId[0]);

            var work = _context.Works
                .Where(w => w.EmployeeID == employeeId && w.ShiftId == shiftId)
                .Single();

            if (work.IsCanceled)
                return NotFound();

            _context.Works.Remove(work);
            _context.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IHttpActionResult AddWork(string id)
        {
            string myId = id;
            string[] stringId = myId.Split();

            var employeeId = int.Parse(stringId[0]);
            var shiftTypeId = int.Parse(stringId[1]);
            var shiftDepartmentId = int.Parse(stringId[2]);

            var newDateTime1 = int.Parse(stringId[3]);
            var newDateTime2 = int.Parse(stringId[4]);
            var newDateTime3 = int.Parse(stringId[5]);



            var shift = _context.Shifts
                .Where(s => s.DateTime.Day == newDateTime2 && s.DateTime.Month == newDateTime1 && s.DateTime.Year == newDateTime3 && s.ShiftType.Id == shiftTypeId && s.Department.Id == shiftDepartmentId)
                .Single().Id;

            var EmployeeIsInTheShift = _context.Works
                .Any(e => e.ShiftId == shift && e.EmployeeID == employeeId);

            if (EmployeeIsInTheShift)
            {
                return NotFound();
            }
            else
            {

                var work = new Models.Employees.Work(employeeId, shift);
                _context.Works.Add(work);
                _context.SaveChanges();
                return Ok();
            }


        }
    }
}