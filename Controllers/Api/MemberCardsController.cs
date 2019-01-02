using OilTeamProject.Models.Customers;
using OilTeamProject.Persistence;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace OilTeamProject.Controllers.api
{
    public class MemberCardsController : ApiController
    {
        public ApplicationDbContext _context;

        public MemberCardsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Create(int? id)
        {
            if (_context.MemberCards
                .Any(m => m.CustomerID == id))
                return BadRequest("The membercard already exists");

            var memberCard = new MemberCard
            {
                CustomerID = id.Value,
                MemberCardCode = MemberCard.GetMemberCardCode(),
                Type = MembershipType.Basic,
                CreationDate = MemberCard.GetCreationDate(),
                Credits = 0
            };

            _context.MemberCards.Add(memberCard);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Delete(int? id)
        {
            var memberCard = _context.MemberCards.Find(id);
            _context.MemberCards.Remove(memberCard);

            try
            {
                _context.SaveChanges();
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }

            return Ok();
        }
    }
}