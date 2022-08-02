using Microsoft.EntityFrameworkCore;
using Multitenant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitenant.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Branch> Branches { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
