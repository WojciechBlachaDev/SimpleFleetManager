using Microsoft.EntityFrameworkCore;
using SimpleFleetManager.Models.EntityFramework;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Interfaces;

namespace SimpleFleetManager.Services.Data
{
    public class JobDataService(SimpleDbContext context) : IDataService<Job>
    {
        private readonly SimpleDbContext _context = context;

        public async Task<Job> Create(Job entity)
        {
            var addedEntity = _context.Jobs.Add(entity).Entity;
            await _context.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Jobs.FindAsync(id);
            if (entity != null) 
            {
                _context.Jobs.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Job> Get(int id)
        {
            var entity = await _context.Jobs.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetAll()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<Job> Update(int id, Job entity)
        {
            var existingEntity = await _context.Jobs.FindAsync(id);
            if (existingEntity != null)
            {
                existingEntity = entity;
            }
            else
            {
                await Create(entity);
                existingEntity = entity;
            }
            await _context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
