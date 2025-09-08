using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Repository.Audit
{
    public class AuditRepository : IAuditRepository
    {
        private readonly TimeSheetContext _ctx;
        public AuditRepository(TimeSheetContext ctx) => _ctx = ctx;
        #region added by durga
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Auditlog>> GetAllAsync() =>
            await _ctx.Auditlogs.AsNoTracking().ToListAsync();
        #endregion

        public async Task<Auditlog?> GetAsync(int id) =>
            await _ctx.Auditlogs.FindAsync(id);

        public async Task AddAsync(Auditlog audit) =>
            await _ctx.Auditlogs.AddAsync(audit);

        public Task UpdateAsync(Auditlog audit)
        {
            _ctx.Auditlogs.Update(audit);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Auditlogs.FindAsync(id);
            if (entity != null) _ctx.Auditlogs.Remove(entity);
        }

        public Task SaveAsync() => _ctx.SaveChangesAsync();
    }
}
