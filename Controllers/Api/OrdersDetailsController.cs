using OilTeamProject.Dtos;
using OilTeamProject.Models;
using OilTeamProject.Persistence;
using System.Linq;
using System.Web.Http;

namespace OilTeamProject.Controllers.api
{
    public class OrdersDetailsController : ApiController
    {
        public ApplicationDbContext _context;

        public OrdersDetailsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public OrderDto GetOrderDetails(int id)
        {

            var order = _context.Orders
                .Single(o => o.OrderID == id);

            var dto = new OrderDto()
            {
                OrderNumber = order.OrderNumber,
                CreationDate = order.CreationDate,
                PaidOff = order.PaidOff,
                Type = order.Type,
                Status = order.Status
            };

            return dto;
        }
    }
}
