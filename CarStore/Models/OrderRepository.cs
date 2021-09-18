using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarStore.Models
{
    public class OrderRepository : IOrderRepository
    {
        private CarsDbContext dbContext;
        public OrderRepository(CarsDbContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Order> Orders => this.dbContext.Orders.Include(order => order.Lines).ThenInclude(line => line.Car);

        public void SaveOrder(Order order)
        {
            this.dbContext.AttachRange(order.Lines.Select(line => line.Car));
            if (order.OrderId == 0)
            {
                this.dbContext.Add(order);
            }

            this.dbContext.SaveChanges();
        }
    }
}
