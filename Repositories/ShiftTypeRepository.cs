using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace OilTeamProject.Repositories
{
    public class ShiftTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ShiftTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ShiftType> GetShiftTypes()
        {
            return _context.ShiftTypes.ToList();
        }

        public List<ShiftType> GetListFromShiftTypes(List<byte> shiftIds)
        {
            return _context.ShiftTypes.Where(t => shiftIds.Contains(t.Id)).ToList();
        }
    }
}