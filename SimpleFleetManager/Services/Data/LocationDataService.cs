using Microsoft.EntityFrameworkCore;
using SimpleFleetManager.Models.EntityFramework;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Interfaces;

namespace SimpleFleetManager.Services.Data
{
    public class LocationDataService(SimpleDbContext context) : IDataService<Location>
    {
        private readonly SimpleDbContext _context = context;

        public async Task<Location> Create(Location entity)
        {
            var addedEntity = _context.Locations.Add(entity).Entity;
            await _context.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Locations.FindAsync(id);
            if (entity != null) 
            {
                _context.Locations.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Location> Get(int id)
        {
            var entity = await _context.Locations.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> Update(int id, Location entity)
        {
            var existingEntity = await _context.Locations.FindAsync(id);
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
