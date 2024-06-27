using Framework.Core;
using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence.EF
{
    public abstract class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public string TraceId { get; protected set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new TransactionalOutboxInterceptor());

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}