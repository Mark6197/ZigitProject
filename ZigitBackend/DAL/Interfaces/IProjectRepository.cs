using Domain.Entities;

namespace DAL.Interfaces
{
    public interface IProjectRepository:IRepository<Project>
    {
        Task<IList<Project>> GetAllProjectByUserAsync(long id);
    }
}
