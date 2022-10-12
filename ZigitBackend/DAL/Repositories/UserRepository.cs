using DAL.Interfaces;
using Domain.Entities;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public long TryLogin(string email, string password)
        {
            User user = Context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            if (user == null)
                return 0;

            return user.Id;
        }

        public async Task<PersonalDetails> GetUserPersonalDetails(long userId)
        {
            return await Context.PersonalDetails.FindAsync(userId);
        }

        public AppDbContext Context
        {
            get { return _context as AppDbContext; }
        }
    }
}
