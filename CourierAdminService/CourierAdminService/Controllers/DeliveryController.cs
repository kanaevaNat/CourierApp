using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourierAdminService.Context;
using CourierAdminService.Models;
namespace CourierAdminService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<DeliveryController> _logger;
        private readonly DataBaseContext dbContext;

        public DeliveryController(ILogger<DeliveryController> logger, DataBaseContext dataBaseContext)
        {
            _logger = logger;
            dbContext = dataBaseContext;
        }

        [HttpGet("/statuses")]
 
        public List<Status> getAllStatuses()
        {
            return dbContext.Statuses.ToList();
        }

        [HttpGet("/orders")]
        public List<Order> getAllOrders()
        {
            return dbContext.Orders.Include(item => item.Client).Where(item => item.CourierId == null).ToList();
        }

        [HttpGet("/couriers")]
        public List<Courier> getAllCouriers()
        {
            return dbContext.Couriers.Include(item => item.Status).Where(item => item.Status.Title == "FREE").ToList();
        }

        [HttpPost("/auth")]
        public bool auth([FromBody] AuthDto authDto)
        {
            try
            {
                Operator o = dbContext.Operators.First(item => item.Login == authDto.Login);
                if(o == null)
                {
                    return false;
                }
                if(o.Password != authDto.Password)
                {
                    return false;
                }
                return true;
            } catch(Exception e)
            {
                return false;
            }
        }

        [HttpPost("/bind")]
        public Order bind([FromBody] BindOrderDto bindOrderDto)
        {
            Order order = dbContext.Orders.Find(bindOrderDto.OrderId);
            Courier courier = dbContext.Couriers.Include(item => item.Status).First(item => item.Id == bindOrderDto.CourierId);
            order.Courier = courier;
            courier.Status.Title = "ON_ORDER";
            dbContext.SaveChanges();
            return dbContext.Orders.Include(item => item.Client).Include(item => item.Courier).Include(item => item.Courier.Status).First(item => item.Id == bindOrderDto.OrderId);
        }
    }
}