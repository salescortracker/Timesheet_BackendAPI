using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Services.Audit
{
    public interface IAuditService
    {
        Task<IEnumerable<Auditlog>> GetAllAsync();
        Task<Auditlog?> GetAsync(int id);
        Task<Auditlog> CreateAsync(Auditlog audit);
        Task<bool> UpdateAsync(Auditlog dto);
        Task<bool> DeleteAsync(int id);
        Task SaveAsync();
    }
}
