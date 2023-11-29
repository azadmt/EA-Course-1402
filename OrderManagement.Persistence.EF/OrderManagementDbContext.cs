using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Persistence.EF
{
    public class OrderManagementDbContext : ApplicationDbContext
    {
        public OrderManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public DbSet<OrderAggregate> Orders { get; set; }
    }
}