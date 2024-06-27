using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using OrderManagement.Domain.Order;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Persistence.EF
{
    public class OrderManagementDbContext : ApplicationDbContext
    {
        public OrderManagementDbContext(DbContextOptions options) : base(options)
        {
            TraceId = Tracer.CurrentSpan.Context.TraceId.ToString();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public DbSet<OrderAggregate> Orders { get; set; }
    }
}