using _310NutritionAPI.Data;
using _310NutritionAPI.Models;
using _310NutritionAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace _310NutritionAPI.Repository
{
    public class CollectionRepository:Repository<Collection>,ICollectionRepository
    {
        private readonly ApplicationDbContext _db;
        public CollectionRepository(ApplicationDbContext db):base(db) {
            _db = db;
        }
        public async Task<Collection> UpdateAsync(Collection collection) {
            _db.Collections.Update(collection);
            collection.DateUpdated = DateTime.Now;
            await _db.SaveChangesAsync();
            return collection;
        }

        public async Task<IEnumerable<Collection>> GetAllAsync()
        {
            return await _db.Collections.Include(c => c.CollectionProducts).ToListAsync();
        }
    }
}
