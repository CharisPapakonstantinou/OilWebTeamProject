using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace OilTeamProject.Repositories
{
    public class RoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}