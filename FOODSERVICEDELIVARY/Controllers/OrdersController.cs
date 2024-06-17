using FOODSERVICEDELIVARY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FOODSERVICEDELIVARY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly FoodDeliveryContext _context;

        public OrdersController(FoodDeliveryContext context)
        {
            _context = context;
        }
        [HttpGet("recent")]
        public ActionResult<IEnumerable<OrderDto>> GetRecentOrders()
        {
            try
            {
                var orders = _context.Orders
                        .Include(o => o.Restaurant)
                        .Include(o => o.Customer)
                        .OrderByDescending(o => o.OrderTime)
                        .Take(50)
                        .Select(o => new OrderDto
                        {
                            Id = o.Id,
                            OrderTime = o.OrderTime,
                            RestaurantName = o.Restaurant.Name,
                            CustomerName = o.Customer.Name,
                            IsTipped = o.IsTipped,
                            TipAmount = o.TipAmount,
                            PickupLocation = o.PickupLocation,
                            DeliveryLocation = o.DeliveryLocation
                        })
                        .ToList();

                 if (orders == null)
                {
                    return NotFound("Food item not available");
                }
                return Ok(orders);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("customer/{customerId}")]
        public ActionResult<CustomerDto> GetCustomerDetails(int customerId)
        {
            try
            {
                var customer = _context.Customers
                        .Include(c => c.Orders)
                        .ThenInclude(o => o.Restaurant)
                        .Include(c => c.Tips)
                        .FirstOrDefault(c => c.Id == customerId);

                if (customer == null)
                {
                    return NotFound("Customer id not available");
                }

                var customerDto = new CustomerDto
                {
                    Name = customer.Name,
                    Address = customer.Address,
                    OrderHistory = customer.Orders.Select(o => new OrderDto
                    {
                        Id = o.Id,
                        OrderTime = o.OrderTime,
                        RestaurantName = o.Restaurant.Name,
                        CustomerName = customer.Name,
                        IsTipped = o.IsTipped,
                        TipAmount = o.TipAmount,
                        PickupLocation = o.PickupLocation,
                        DeliveryLocation = o.DeliveryLocation
                    }).ToList(),
                    TotalTips = customer.Tips.Sum(t => t.Amount)
                };

                return Ok(customerDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetAllOrdersHistory")]
        public ActionResult<IEnumerable<OrderDto>> GetAllOrdersHistory()
        {
            try
            {
                var orders = _context.Orders
                        .Include(o => o.Restaurant)
                        .Include(o => o.Customer)
                        .OrderByDescending(o => o.OrderTime)
                        .Select(o => new OrderDto
                        {
                            Id = o.Id,
                            OrderTime = o.OrderTime,
                            RestaurantName = o.Restaurant.Name,
                            CustomerName = o.Customer.Name,
                            IsTipped = o.IsTipped,
                            TipAmount = o.TipAmount,
                            PickupLocation = o.PickupLocation,
                            DeliveryLocation = o.DeliveryLocation
                        })
                        .ToList();

                if (orders == null)
                {
                    return NotFound("Food item not available");
                }
                return Ok(orders);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string RestaurantName { get; set; }
        public string CustomerName { get; set; }
        public bool IsTipped { get; set; }
        public decimal TipAmount { get; set; }
        public string PickupLocation { get; set; }
        public string DeliveryLocation { get; set; }
    }

    public class CustomerDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<OrderDto> OrderHistory { get; set; }
        public decimal TotalTips { get; set; }
    }
}
