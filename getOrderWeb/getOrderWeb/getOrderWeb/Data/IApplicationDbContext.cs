using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace getOrderWeb.Data
{
    interface IApplicationDbContext:IDisposable
    {


        void Dispose();
        ValueTask DisposeAsync();
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
