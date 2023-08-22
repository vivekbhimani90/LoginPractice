using LoginPractice.Data;
using LoginPractice.Models;
using LoginPractice.Repository.IRepository;

namespace LoginPractice.Repository
{
    public class PersonalDetailsRepository : Repository<PersonalDetails>, IPersonalDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonalDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<PersonalDetails> UpdateAsync(PersonalDetails entity)
        {
            entity.Updated = DateTime.Now;
            _db.UserDetails.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
