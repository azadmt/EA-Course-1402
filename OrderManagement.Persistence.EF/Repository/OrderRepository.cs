using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Persistence.EF.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagementDbContext _uow;

        public OrderRepository(OrderManagementDbContext uow)
        {
            _uow = uow;
        }

        public OrderAggregate Get(Guid id)
        {
            return _uow.Orders.Include(x => x.OrderItems).Single(x => x.Id == id);
        }

        public void Save(OrderAggregate aggregate)
        {
            _uow.Orders.Add(aggregate);
            _uow.SaveChanges();
        }

        public void Update(OrderAggregate aggregate)
        {
            _uow.Orders.Update(aggregate);
            _uow.SaveChanges();
        }
    }
}