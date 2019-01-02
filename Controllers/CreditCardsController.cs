using OilTeamProject.Models.Customers;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System.Data;
using System.Net;
using System.Web.Mvc;

namespace OilTeamProject.Controllers
{
    public class CreditCardsController : Controller
    {
        private ApplicationDbContext _context;

        public CreditCardsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create(int id)
        {
            var creditCard = new CreditCardFormViewModel()
            {
                CustomerID = id
            };

            return View(creditCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreditCardFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            //if (id == null)
            //{
            //    return HttpNotFound();
            //}

            var newCreditCard = new CreditCard(viewModel.Type, viewModel.CreditCardNumber, viewModel.GetDateTime(), viewModel.CustomerID);

            //{
            //    CustomerID = id.Value,
            //    CreditCardNumber = viewModel.CreditCardNumber,
            //    Type = viewModel.Type,
            //    ExpireDate = viewModel.GetDateTime()
            //};

            try
            {
                _context.CreditCards.Add(newCreditCard);
                _context.SaveChanges();
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }

            return RedirectToAction("CustomerProfile", "Customers", new { id = newCreditCard.CustomerID });
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CreditCard creditCard = _context.CreditCards.Find(id);
            if (creditCard == null)
                return HttpNotFound();

            return View(creditCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            CreditCard creditCard = _context.CreditCards.Find(id);

            _context.CreditCards.Remove(creditCard);
            _context.SaveChanges();
            return RedirectToAction("CustomerProfile", "Customers", new { id = creditCard.CustomerID });
        }
    }
}