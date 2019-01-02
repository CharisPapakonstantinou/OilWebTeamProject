using OilTeamProject.Models.Customers;
using OilTeamProject.Models.Products;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OilTeamProject.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }



        public ActionResult PayOff(int? id)
        {
            var UnpaidOrder = _context.Orders
                .Where(o => o.OrderID == id)
                .Include(o => o.OrderProducts)
                .Include(o => o.Customer)
                .Single();

            var viewModel = UnpaidOrder.FillOrderDetailsViewModel(UnpaidOrder, _context);
            return View("OrderReceiptUpdated", viewModel);
        }

        [HttpPost]
        public ActionResult PayOff(OrderDetailsFormViewModel viewModel)
        {
            var Order = _context.Orders.Find(viewModel.OrderId);
            if (viewModel.PaymentType != PaymentType.none)
            {
                Order.PaymentType = viewModel.PaymentType;
                Order.PaidOff = Order.CheckpaidOffProperty(Order.PaymentType);
                Order.PaymentDate = DateTime.Now;
                _context.SaveChanges();
            }


            return RedirectToAction("PendingOrders", new { id = Order.CustomerId });
        }
        public ActionResult PendingOrders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var CustomerPendingOrders = _context.Orders
                .Where(o => o.CustomerId == id && !o.PaidOff)
                .ToList();


            ViewBag.ListCount = Order.CountPendingOrders(CustomerPendingOrders);
            return View(CustomerPendingOrders);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Order = _context.Orders.Find(id);

            return View(Order);
        }

        public ActionResult EditDetails(int? id)
        {
            var newOrder = _context.Orders
                .Include(o => o.OrderProducts)
                .Where(o => o.OrderID == id)
                .Single();

            OrderFormViewModel viemodel = Order.PopulateUpdateOrderDetails(newOrder, _context);
            return View(viemodel);
        }

        [HttpPost]
        public ActionResult EditDetails(OrderFormViewModel viewModel)
        {


            //if (!ModelState.IsValid)
            //{
            //    return View("EditDetails", viewModel);
            //}

            var OrderToUpdate = _context.Orders
                .Include(o => o.OrderProducts)
                .Where(O => O.OrderID == viewModel.OrderId)
                .Single();





            Order.UpdateOrderProducts(OrderToUpdate, viewModel, _context);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = viewModel.OrderId });




            //PopulateUpdateOrderDetails(OrderToUpdate);

            //return View(OrderToUpdate);





        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = _context.Orders
               .Include(o => o.OrderProducts)
               .Where(o => o.OrderID == id)
               .Single();





            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }


        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _context.Orders.Find(id);
            //order.TotalCost = Order.GetTotalCost(order.OrderID, db);


            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            try
            {
                Order.DeleteOrder(Id, _context);

            }
            catch (Exception)
            {
                ModelState.AddModelError("", "unable to save changes");
            }
            //Order order = db.Orders.Find(id);
            //db.Orders.Remove(order);
            //db.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }


        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _context.Customers.Find(id);
            var ViewModel = Order.FillOrderFormViewModel(_context);

            ViewModel.CustomerId = customer.CustomerID;
            return View("ShoppingCart", ViewModel);
        }


        [HttpPost]
        public ActionResult Create(OrderFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }



            var customer = _context.Customers.Find(viewModel.CustomerId);

            OrderDetailsFormViewModel detailsViewModel = Order.FillOrderDetailsViewModel(viewModel, customer, _context);

            return View("OrderReceiptUpdated", detailsViewModel);
        }


        [HttpPost]
        public ActionResult ConfirmOrder(OrderDetailsFormViewModel viewModel)
        {

            if (viewModel.TotalCost == 0)
            {
                viewModel.CartIsEmpty = true;
                viewModel.OrderDate = DateTime.Now;
                return View("OrderReceipt", viewModel);
            }

            var customer = _context.Customers.Find(viewModel.CustomerId);

            Order order = new Order(Order.GetOrderNumber(viewModel.CustomerId, _context)
               , Order.CheckpaidOffProperty(viewModel.PaymentType)
               , Order.RedeemOrNot(viewModel, customer, _context),
               viewModel.PaymentType, customer,
               Order.SetOrderStatus(Order.CheckpaidOffProperty(viewModel.PaymentType)),
               Order.SetPayDate(Order.CheckpaidOffProperty(viewModel.PaymentType)));

            OrderProducts orderProducts = new OrderProducts();




            _context.Orders.Add(order);
            _context.SaveChanges();


            for (int i = 0; i < viewModel.Count; i++)
            {
                if (viewModel.AddToCarts[i] && viewModel.Quantities[i] != 0)
                {

                    orderProducts.OrderID = order.OrderID;
                    orderProducts.ProductStockID = viewModel.ProductStockIds[i];
                    orderProducts.Quantity = viewModel.Quantities[i];

                    _context.OrderProducts.Add(orderProducts);

                    var ProductStockToUpdate = _context.ProductStocks.Single(p => p.ProductStockID == orderProducts.ProductStockID);
                    ProductStockToUpdate = ProductStockToUpdate.SendProductStock(viewModel.Quantities[i], orderProducts.ProductStockID);


                    _context.Set<ProductStock>().AddOrUpdate(ProductStockToUpdate);
                    _context.SaveChanges();
                }
            }


            if (customer.MemberCard != null)
            {
                if (viewModel.RedeemBonus)
                {
                    MemberCard.ZeroMemberPoints(customer, _context);
                }
                else
                {
                    MemberCard.UpdateCredits(customer, _context, MemberCard.CalculateCredits(order.TotalCost, customer, _context));
                }

                MemberCard.CheckMembershipType(customer.MemberCard, customer.MemberCard.Credits, _context);
            }

            return RedirectToAction("Index", "Customers");




        }

    }
}