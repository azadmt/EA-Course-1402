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
        //DbSet<OutboxSupportApplicationDbContext> Outbox { get; set; }
        public OutboxSupportApplicationDbContext(DbContextOptions options) : base(options)

        {
        }
    }
}