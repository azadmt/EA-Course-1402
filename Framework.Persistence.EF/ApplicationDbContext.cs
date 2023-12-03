using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Framework.Persistence.EF
{
    public abstract class ApplicationDbContext : DbContext, IUnitOfWork
    {
        protected ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new OutboxInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}