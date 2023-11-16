using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence.EF
{
    public abstract class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public void Commit()
        {
          SaveChanges();
        }
    }
}