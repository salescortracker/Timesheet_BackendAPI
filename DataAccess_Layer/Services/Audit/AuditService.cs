using DataAccess_Layer.Models;
using DataAccess_Layer.Repository.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Services.Audit
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _repository;
        public AuditService(IAuditRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Auditlog>> GetAllAsync() =>
           await _repository.GetAllAsync();

        public async Task<Auditlog?> GetAsync(int id) =>
            await _repository.GetAsync(id);

        public async Task AddAsync(Auditlog audit) =>
            await _repository.AddAsync(audit);

        public Task UpdateAsync(Auditlog audit)
        {
            _repository.UpdateAsync(audit);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);

        }

        public Task SaveAsync() => _repository.SaveAsync();
    }
}
