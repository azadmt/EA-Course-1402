using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Persistence
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
