using Microsoft.EntityFrameworkCore;
using SimpleFleetManager.Models.Common.AMR.Misc;
using SimpleFleetManager.Models.EntityFramework;
using SimpleFleetManager.Services.Interfaces;

namespace SimpleFleetManager.Services.Data
{
    public class LogsDataService(SimpleDbContext context) : IDataService<ForkliftLog>
    {
        private bool _isLoadingList;
        private readonly SimpleDbContext _context = context;
        public async Task<ForkliftLog> Create(ForkliftLog entity)
        {
            if (!_isLoadingList)
            {
                var addedEntity = _context.Logs.Add(entity).Entity;
                await _context.SaveChangesAsync();
                return addedEntity;
            }
            return new();
            
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Logs.FindAsync(id);
            if (entity != null)
            {
                _context.Logs.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ForkliftLog> Get(int id)
        {
            var entity = await _context.Logs.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ForkliftLog>> GetAll()
        {
            _isLoadingList = true;
            List<ForkliftLog> forkliftLogs = await _context.Logs.ToListAsync();
            IEnumerable<ForkliftLog> Logs = forkliftLogs;
            _isLoadingList = false;
            return Logs;
        }


        public Task<ForkliftLog> Update(int id, ForkliftLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
