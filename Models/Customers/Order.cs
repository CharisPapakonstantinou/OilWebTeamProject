using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OilTeamProject.ViewModels;
using OilTeamProject.Persistence;

namespace OilTeamProject.Models.Customers
{
    public enum OrderType
    {
        FirstComeFirstServed,
        TopPriority
    }
    public enum OrderStatus
    {
        Fulfilled,
        Deleted,
        InProgress

    }
    public class Order
    {
        public int OrderID { get; set; }

        [Display(Name = "Order Date")]
        public string OrderNumber { get; set; }

        public DateTime CreationDate { get; set; }

        [Display(Name = "Order Type")]
        public OrderType Type { get; set; }

        [Display(Name = "Order Status")]
        public OrderStatus Status { get; set; }

        [Display(Name = "Paid Off")]
        public bool PaidOff { get; set; }

        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        public DateTime? PaymentDate { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, 1000000)]
        public double TotalCost { get; set; }

        public PaymentType PaymentType { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }


        public Order()
        {

        }

        public Order(string orderNumber, bool paidOff, double totalCost, PaymentType paymentType, Customer customer, OrderStatus orderStatus, DateTime? paymentDate)
        {

            OrderNumber = orderNumber;
            PaidOff = paidOff;
            TotalCost = totalCost;
            PaymentType = paymentType;
            CreationDate = DateTime.Now;
            PaymentDate = paymentDate;
            Status = orderStatus;

            if (customer == null)
            {
                throw new ArgumentNullException("user");
            }

            Customer = customer;
        }

        public static double RedeemOrNot(OrderDetailsFormViewModel viewModel, Customer customer, ApplicationDbContext _context)
        {
            if (viewModel.RedeemBonus)
            {
                return MemberCard.GetNewTotalCost(GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts), MemberCard.BonusAmount(customer, _context));
            }
            else
            {
                return GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts);
            }
        }

        public static DateTime? SetPayDate(bool paidOff)
        {
            if (paidOff)
                return DateTime.Now;
            return null;


        }

        public static List<Order> GetUnpaidOrders(List<Customer> customers, ApplicationDbContext _context)
        {
            var customerPendingOrders = new List<Order>();
            foreach (var customer in customers)
            {
                var customerPendingOrder = _context.Orders
                .Where(o => o.CustomerId == customer.CustomerID && !o.PaidOff)
                 .Single();

                customerPendingOrders.Add(customerPendingOrder);
                //NumberOfUnpaidOrder.Add(CustomerPendingOrders.Count);
            }

            return customerPendingOrders;
        }


        public static List<Order> GetUnpaidOrders(int? id, ApplicationDbContext _context)
        {
            var CustomerPendingOrders = _context.Orders
              .Where(o => o.CustomerId == id && !o.PaidOff)
              .ToList();

            return CustomerPendingOrders;
        }

        public static OrderDetailsFormViewModel FillOrderDetailsViewModel(OrderFormViewModel viewModel, Customer customer, ApplicationDbContext _context)
        {
            OrderDetailsFormViewModel detailsViewmodel = new OrderDetailsFormViewModel();

            for (int i = 0; i < viewModel.Count; i++)
            {

                detailsViewmodel.ProductStockIds.Add(viewModel.ProductStockIds[i]);
                detailsViewmodel.Quantities.Add(viewModel.Quantities[i]);
                detailsViewmodel.AddToCarts.Add(viewModel.AddToCarts[i]);
                detailsViewmodel.Prices.Add(viewModel.Prices[i]);
                detailsViewmodel.ProductNames.Add(viewModel.ProductNames[i]);
                detailsViewmodel.SubTotal.Add(GetSubTotal(viewModel.Prices[i], viewModel.Quantities[i]));


            }


            detailsViewmodel.OrderNumber = GetOrderNumber(customer.CustomerID, _context);
            detailsViewmodel.TotalCost = GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts);
            detailsViewmodel.OrderDate = customer.GetCreationDate();
            detailsViewmodel.Count = viewModel.Count;
            detailsViewmodel.OrderId = viewModel.OrderId;
            detailsViewmodel.FirstName = customer.FirstName;
            detailsViewmodel.LastName = customer.LastName;
            detailsViewmodel.City = customer.City;
            detailsViewmodel.Country = customer.Country;
            detailsViewmodel.Address = customer.Address;
            detailsViewmodel.CustomerId = customer.CustomerID;
            detailsViewmodel.PhoneNumber = customer.PhoneNumber;
            detailsViewmodel.PostalCode = customer.PostalCode;
            detailsViewmodel.Email = customer.Email;
            detailsViewmodel.PaymentType = PaymentType.none;
            // TODO
            detailsViewmodel.IfBonusIsApplicable = MemberCard.CheckBonus(detailsViewmodel.TotalCost, customer, _context);
            detailsViewmodel.CartIsEmpty = false;

            return detailsViewmodel;
        }

        public OrderDetailsFormViewModel FillOrderDetailsViewModel(Order order, ApplicationDbContext _context)
        {

            OrderDetailsFormViewModel detailsViewmodel = new OrderDetailsFormViewModel();
            List<OrderProducts> orderproducts = OrderProducts
                .Where(o => o.OrderID == order.OrderID)
                .ToList();

            List<string> productNames = new List<string>();
            List<double> prices = new List<double>();
            List<int> quantities = new List<int>();
            List<string> ProductStockIds = new List<string>();

            foreach (var item in orderproducts)
            {
                productNames.Add(item.ProductStock.Product.Name);
                prices.Add(item.ProductStock.Product.Price);
                quantities.Add(item.Quantity);
                ProductStockIds.Add(item.ProductStockID);
            }
            for (int i = 0; i < orderproducts.Count; i++)
            {

                detailsViewmodel.ProductStockIds.Add(ProductStockIds[i]);
                detailsViewmodel.Quantities.Add(quantities[i]);
                detailsViewmodel.AddToCarts.Add(true);
                detailsViewmodel.Prices.Add(prices[i]);
                detailsViewmodel.ProductNames.Add(productNames[i]);
                detailsViewmodel.SubTotal.Add(GetSubTotal(prices[i], quantities[i]));


            }

            detailsViewmodel.OrderNumber = GetOrderNumber(Customer.CustomerID, _context);
            detailsViewmodel.TotalCost = TotalCost;
            detailsViewmodel.OrderDate = Customer.GetCreationDate();
            detailsViewmodel.Count = orderproducts.Count;
            detailsViewmodel.OrderId = order.OrderID;
            detailsViewmodel.FirstName = Customer.FirstName;
            detailsViewmodel.LastName = Customer.LastName;
            detailsViewmodel.City = Customer.City;
            detailsViewmodel.Country = Customer.Country;
            detailsViewmodel.Address = Customer.Address;
            detailsViewmodel.CustomerId = Customer.CustomerID;
            detailsViewmodel.PhoneNumber = Customer.PhoneNumber;
            detailsViewmodel.PostalCode = Customer.PostalCode;
            detailsViewmodel.Email = Customer.Email;
            detailsViewmodel.PaymentType = order.PaymentType;
            return detailsViewmodel;
        }

        public static OrderFormViewModel PopulateUpdateOrderDetails(Order order, ApplicationDbContext _context)
        {

            int i = 0;
            var AllProductStocks = _context.ProductStocks
                .Include(p => p.Product)
                .Include(p => p.Bottling);


            var OrderProducts = new HashSet<string>(order.OrderProducts.Select(op => op.ProductStockID));
            var ProductQuantites = new HashSet<int>(order.OrderProducts.Select(op => op.Quantity));
            var alreadyContainedProducts = new List<int>();
            var viewModel = new OrderFormViewModel();
            //foreach (var Product in AllProductStocks)
            //{
            //    viewModel.Count++;
            //}
            List<int> QuantityInOrderProducts = new List<int>();

            foreach (var item in order.OrderProducts)
            {
                QuantityInOrderProducts.Add(item.Quantity);


            }

            viewModel.OrderId = order.OrderID;
            foreach (var productStock in AllProductStocks)
            {
                var productStock1 = productStock.SendToCustomerView(productStock.ProductID);

                if (!alreadyContainedProducts.Contains(productStock1.ProductID))
                {
                    viewModel.ProductStockIds.Add(productStock.ProductStockID);
                    viewModel.ProductNames.Add(productStock.Product.Name);
                    viewModel.Prices.Add(productStock.Product.Price);
                    viewModel.AddToCarts.Add(OrderProducts.Contains(productStock.ProductStockID));
                    if (OrderProducts.Contains(productStock.ProductStockID))
                    {

                        viewModel.Quantities.Add(QuantityInOrderProducts[i]);
                        i++;
                    }
                    else
                    {
                        viewModel.Quantities.Add(0);
                    }
                    viewModel.Count++;
                    alreadyContainedProducts.Add(productStock1.ProductID);

                }

            }

            foreach (var item in order.OrderProducts)
            {
                QuantityInOrderProducts.Add(item.Quantity);


            }



            return viewModel;

        }

        public static OrderFormViewModel FillOrderFormViewModel(ApplicationDbContext _context)
        {
            var viewModel = new OrderFormViewModel();
            var productStocks = _context.ProductStocks
                .Include(p => p.Product)
                .Include(p => p.Bottling)
                .ToList();

            var alreadyContainedProducts = new List<int>();

            foreach (var productStock in productStocks)
            {
                var productStock1 = productStock.SendToCustomerView(productStock.ProductID);
                if (!alreadyContainedProducts.Contains(productStock1.ProductID))
                {
                    viewModel.ProductStockIds.Add(productStock1.ProductStockID);
                    viewModel.ProductNames.Add(productStock1.Product.Name);
                    viewModel.Prices.Add(productStock1.Product.Price);
                    viewModel.Descriptions.Add(productStock1.Product.Description);
                    viewModel.Quantities.Add(0);
                    viewModel.AddToCarts.Add(false);
                    alreadyContainedProducts.Add(productStock1.ProductID);

                    viewModel.Count++;
                }

                //viewModel.Count++;
            }


            return viewModel;
        }

        public static void UpdateOrderProducts(Order orderToUpdate, OrderFormViewModel viewModel, ApplicationDbContext _context)
        {
            int i = 0;
            int countItemsInSelectedQuantityFromView = 0;
            OrderProducts orderProduct = new OrderProducts();
            //if (viewModel.ProductID == null) // ?????? 
            //{
            //    orderToUpdate.OrderProducts = new List<OrderProducts>();

            //}
            var selectedProductStockFromView = new List<string>();
            var selectedQuantityFromView = new List<int>();


            for (int j = 0; j < viewModel.Count; j++)
            {
                if (viewModel.AddToCarts[j])
                {
                    selectedProductStockFromView.Add(viewModel.ProductStockIds[j]);
                    selectedQuantityFromView.Add(viewModel.Quantities[j]);
                    countItemsInSelectedQuantityFromView++;
                }
            }
            var orderToUpdateTotalCost = _context.Orders.Find(viewModel.OrderId);

            var productStocks = _context.ProductStocks
                .Include(p => p.Product)
                .Include(p => p.Bottling);

            var ProductStockID = new List<string>();

            foreach (var productStock in productStocks)
            {

                ProductStockID.Add(productStock.ProductStockID);
            }



            var OrderProducts = new HashSet<string>(orderToUpdate.OrderProducts.Select(o => o.ProductStockID));
            var SelectedQuantityFromDb = new List<int>(orderToUpdate.OrderProducts.Select(o => o.Quantity));

            foreach (var productStockid in ProductStockID)
            {
                if (i < viewModel.Count)
                {
                    if (selectedProductStockFromView.Contains(productStockid))
                    {
                        if (!(viewModel.Quantities[i] == 0))
                        {
                            if (!OrderProducts.Contains(productStockid))
                            {

                                orderProduct.OrderID = viewModel.OrderId;
                                orderProduct.ProductStockID = viewModel.ProductStockIds[i];
                                orderProduct.Quantity = viewModel.Quantities[i];

                                orderToUpdateTotalCost.TotalCost = GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts);
                                _context.Entry(orderToUpdateTotalCost).State = EntityState.Modified;
                                orderToUpdate.OrderProducts.Add(orderProduct);
                            }
                            else if (i < countItemsInSelectedQuantityFromView) // h lista me ta epilegmena adikeimena view tha ne sunithos mikroterh....
                            {
                                if (!(SelectedQuantityFromDb[i] == selectedQuantityFromView[i])) // Μεταβάλλω την ποσοτητα  κραττώντας επιλεγμένο το προιον
                                {
                                    int quantityForFindingOrderProduct = SelectedQuantityFromDb[i]; //  prepei na to vgalo ekso po th LINQ  gt de  katalavenei c# vlepe paradeigma peri me gig
                                    var OrderProductFromDbToUpdate = orderToUpdate.OrderProducts
                                        .Single(op => op.OrderID == viewModel.OrderId && op.ProductStockID == productStockid
                                    && op.Quantity == quantityForFindingOrderProduct); // vrisko thn eggrafh  pou thelo na kano update 

                                    OrderProductFromDbToUpdate.OrderID = viewModel.OrderId;
                                    OrderProductFromDbToUpdate.ProductStockID = viewModel.ProductStockIds[i];
                                    OrderProductFromDbToUpdate.Quantity = viewModel.Quantities[i];

                                    orderToUpdateTotalCost.TotalCost = GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts);
                                    _context.Entry(orderToUpdateTotalCost).State = EntityState.Modified;

                                    orderToUpdate.OrderProducts.Add(OrderProductFromDbToUpdate);
                                }
                            }
                        }



                    }
                    else
                    {
                        if (OrderProducts.Contains(productStockid))
                        {
                            int quantityForFindingOrderProduct = SelectedQuantityFromDb[i];
                            orderProduct.OrderID = viewModel.OrderId;
                            orderProduct.ProductStockID = viewModel.ProductStockIds[i];
                            orderProduct.Quantity = viewModel.Quantities[i];

                            orderToUpdateTotalCost.TotalCost = GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts);
                            _context.Entry(orderToUpdateTotalCost).State = EntityState.Modified;

                            DeleteOrderProducts(productStockid, orderToUpdate.OrderID, quantityForFindingOrderProduct, _context);

                            // βρισκει την εγγραφη στον ενδιαμεσο πινακα για να την αφαιρεσει διοτι πρεπει να ειναι ακριβως ομοια


                        }
                    }
                }
                //if (selectedProductStockFromView.Contains(productStockid))
                //{
                //    if (!(viewModel.Quantities[i] == 0))
                //    {
                //        if (!OrderProducts.Contains(productStockid))
                //        {

                //            orderProduct.OrderID = viewModel.OrderId;
                //            orderProduct.ProductStockID = viewModel.ProductStockIds[i];
                //            orderProduct.Quantity = viewModel.Quantities[i];

                //            orderToUpdateTotalCost.TotalCost = GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts);
                //            _context.Entry(orderToUpdateTotalCost).State = EntityState.Modified;
                //            orderToUpdate.OrderProducts.Add(orderProduct);
                //        }
                //        else if (i < countItemsInSelectedQuantityFromView) // h lista me ta epilegmena adikeimena view tha ne sunithos mikroterh....
                //        {
                //            if (!(SelectedQuantityFromDb[i] == selectedQuantityFromView[i])) // Μεταβάλλω την ποσοτητα  κραττώντας επιλεγμένο το προιον
                //            {
                //                int quantityForFindingOrderProduct = SelectedQuantityFromDb[i]; //  prepei na to vgalo ekso po th LINQ  gt de  katalavenei c# vlepe paradeigma peri me gig
                //                var OrderProductFromDbToUpdate = orderToUpdate.OrderProducts
                //                    .Single(op => op.OrderID == viewModel.OrderId && op.ProductStockID == productStockid
                //                && op.Quantity == quantityForFindingOrderProduct); // vrisko thn eggrafh  pou thelo na kano update 

                //                OrderProductFromDbToUpdate.OrderID = viewModel.OrderId;
                //                OrderProductFromDbToUpdate.ProductStockID = viewModel.ProductStockIds[i];
                //                OrderProductFromDbToUpdate.Quantity = viewModel.Quantities[i];

                //                orderToUpdateTotalCost.TotalCost = GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts);
                //                _context.Entry(orderToUpdateTotalCost).State = EntityState.Modified;

                //                orderToUpdate.OrderProducts.Add(OrderProductFromDbToUpdate);
                //            }
                //        }
                //    }



                //}
                //else
                //{
                //    if (OrderProducts.Contains(productStockid))
                //    {
                //        int quantityForFindingOrderProduct = SelectedQuantityFromDb[i];
                //        orderProduct.OrderID = viewModel.OrderId;
                //        orderProduct.ProductStockID = viewModel.ProductStockIds[i];
                //        orderProduct.Quantity = viewModel.Quantities[i];

                //        orderToUpdateTotalCost.TotalCost = GetTotalCost(viewModel.Prices, viewModel.Quantities, viewModel.Count, viewModel.AddToCarts);
                //        _context.Entry(orderToUpdateTotalCost).State = EntityState.Modified;

                //        DeleteOrderProducts(productStockid, orderToUpdate.OrderID, quantityForFindingOrderProduct, _context);

                //        // βρισκει την εγγραφη στον ενδιαμεσο πινακα για να την αφαιρεσει διοτι πρεπει να ειναι ακριβως ομοια


                //    }
                //}
                i++;
            }
        }
        public static void DeleteOrderProducts(string productStockID, int OrderID, int Quantity, ApplicationDbContext _context)
        {

            var orderFromRemoval = _context.OrderProducts.Single(op => op.ProductStockID == productStockID
                                        && op.Quantity == Quantity
                                        && op.OrderID == OrderID);
            if (orderFromRemoval != null)
            {
                _context.OrderProducts.Remove(orderFromRemoval);
            }

        }


        public static double GetTotalCost(List<double> Price, List<int> Quantity, int Count, List<bool> Bought)
        {
            double TotalCost = 0;

            for (int i = 0; i < Count; i++)
            {
                if (Bought[i])
                {
                    TotalCost = TotalCost + (double?)(Price[i] * Quantity[i]) ?? 0;
                }

            }

            return TotalCost;

        }

        public static double GetTotalCostAfterMemberShipBonus(List<double> Price, List<int> Quantity, int Count, List<bool> Bought, MemberCard memberCard)
        {
            double TotalCost = 0;

            switch (memberCard.Type)
            {
                case MembershipType.Silver:
                    TotalCost = GetTotalCost(Price, Quantity, Count, Bought) - 20;
                    break;
                case MembershipType.Gold:
                    TotalCost = GetTotalCost(Price, Quantity, Count, Bought) - 30;
                    break;
                case MembershipType.Basic:
                    TotalCost = GetTotalCost(Price, Quantity, Count, Bought) - 10;
                    break;

            }

            return TotalCost;
        }

        public double GetTotalCost(int id)
        {
            double TotalCost = OrderProducts.Where(op => op.OrderID == id).Sum(op => (double?)(op.ProductStock.Product.Price * op.Quantity)) ?? 0;

            return TotalCost;
        }

        public static double GetSubTotal(double Price, int Quantity)
        {

            return (Price * Quantity);
        }

        public static OrderStatus SetOrderStatus(bool PaidOff)
        {
            if (PaidOff)
                return OrderStatus.Fulfilled;
            return OrderStatus.InProgress;
        }

        public static string GetOrderNumber(int CustomerId, ApplicationDbContext _context)
        {
            int count = 0;

            var Orders = _context.Orders
                .Where(O => O.CustomerId == CustomerId)
                .ToList();

            foreach (var order in Orders)
            {
                count++;
            }

            string OrderNumber = "OD" + CustomerId + "-000" + count;

            return OrderNumber;


        }

        public static bool CheckpaidOffProperty(PaymentType paymentType)
        {
            if (PaymentType.none != paymentType)
                return true;
            return false;
        }

        public static int CountPendingOrders(List<Order> Orders)
        {

            return Orders.Count;
        }

        public static void DeleteOrder(int id, ApplicationDbContext _context)
        {
            Order order = _context.Orders
                .Include(o => o.OrderProducts)
                .Where(o => o.OrderID == id)
                .Single();

            _context.Orders.Remove(order);






            var OrderProduct = _context.OrderProducts
                .Where(o => o.OrderID == id)
                .FirstOrDefault();

            if (OrderProduct != null)
            {
                _context.OrderProducts.Remove(OrderProduct);
            }



            _context.SaveChanges();

        }
    }
}