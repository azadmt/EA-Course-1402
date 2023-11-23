using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence.EF
{
    public abstract class ApplicationDbContext : DbContext, IUnitOfWork
    {
        protected ApplicationDbContext(DbContextOptions options):base(options)
        {

        }
        public void Commit()
        {
            SaveChanges();
        }
    }
}