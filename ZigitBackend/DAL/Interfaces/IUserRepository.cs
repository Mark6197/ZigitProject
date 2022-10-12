using Domain.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        long TryLogin(string email, string password);
        Task<PersonalDetails> GetUserPersonalDetails(long userId);

    }
}
