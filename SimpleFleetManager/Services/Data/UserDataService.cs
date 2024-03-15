using Microsoft.EntityFrameworkCore;
using SimpleFleetManager.Models.EntityFramework;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Interfaces;

namespace SimpleFleetManager.Services.Data
{
    public class UserDataService(SimpleDbContext context) : IDataService<User>
    {
        private readonly SimpleDbContext _context = context;

        public async Task<User> Create(User entity)
        {
            var addedEntity = _context.Users.Add(entity).Entity;
            await _context.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity != null) 
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User> Get(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Update(int id, User entity)
        {
            var existingEntity = await _context.Users.FindAsync(id);
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
