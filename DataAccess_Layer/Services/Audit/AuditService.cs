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

        public async Task<Auditlog> CreateAsync(Auditlog audit)
        {
            audit.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(audit);

            // this line must exist for the mock to see SaveAsync
            await _repository.SaveAsync();

            return audit;
        }


        public async Task<bool> UpdateAsync(Auditlog dto)
        {
            var entity = await _repository.GetAsync(dto.AuditId);
            if (entity == null) return false;

            entity.UserId = dto.UserId;
            entity.ActionType = dto.ActionType;
            entity.ActionDetails = dto.ActionDetails;

            await _repository.UpdateAsync(entity);   // must exist if the test expects it
            await _repository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var audit = await _repository.GetAsync(id);
            if (audit == null) return false;

            await _repository.DeleteAsync(id);

            // Without this line, Moq can never see SaveAsync
            await _repository.SaveAsync();

            return true;
        }

        public Task SaveAsync() => _repository.SaveAsync();
    }
}
