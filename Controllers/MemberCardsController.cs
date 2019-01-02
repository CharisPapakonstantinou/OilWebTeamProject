using OilTeamProject.Models.Customers;
using OilTeamProject.Persistence;
using System.Net;
using System.Web.Mvc;

namespace OilTeamProject.Controllers
{
    public class MemberCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberCardsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(MemberCard member, int? id)
        {

            if (!ModelState.IsValid)
            {
                return View("Create", member);
            }

            // Create a memberCard

            var memberCard = new MemberCard()
            {
                CustomerID = id.Value,
                MemberCardCode = MemberCard.GetMemberCardCode(),
                Type = MembershipType.Basic,
                CreationDate = MemberCard.GetCreationDate(),
                Credits = 0,
                NewsLetter = member.NewsLetter
            };

            // Send memberCard to DB
            _context.MemberCards.Add(memberCard);
            _context.SaveChanges();

            // where i will send the user
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MemberCard memberCard = _context.MemberCards.Find(id);

            if (memberCard == null)
            {
                return HttpNotFound();
            }

            return View(memberCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            MemberCard memberCard = _context.MemberCards.Find(id);

            _context.MemberCards.Remove(memberCard);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

    }
}