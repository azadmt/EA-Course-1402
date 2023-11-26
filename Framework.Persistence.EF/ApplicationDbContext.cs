using Framework.Core;
using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence.EF
{
    public abstract class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public void Commit()
        {
            SaveChanges();
        }
    }

    public abstract class OutboxSupportApplicationDbContext : ApplicationDbContext
    {
        public DbSet<OutBoxMessage> Outbox { get; set; }

        public OutboxSupportApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<OutBoxMessage>()
                .ToTable("Outbox")
                .HasKey(e => e.Id);
            modelBuilder
                  .Entity<OutBoxMessage>()
                  .Property(e => e.EventType)
                  .HasMaxLength(500);
            modelBuilder
                  .Entity<OutBoxMessage>()
                  .Property(e => e.EventBody);
            modelBuilder
                 .Entity<OutBoxMessage>()
                 .Property(e => e.PublishedAt);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new TransactionalOutboxInterceptor());
            base.OnConfiguring(optionsBuilder);
        }
    }
}