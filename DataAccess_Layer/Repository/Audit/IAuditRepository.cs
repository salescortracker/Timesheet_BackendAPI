using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Repository.Audit
{
    public interface IAuditRepository
    {
        Task<IEnumerable<Auditlog>> GetAllAsync();
        Task<Auditlog?> GetAsync(int id);
        Task AddAsync(Auditlog audit);
        Task UpdateAsync(Auditlog audit);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
