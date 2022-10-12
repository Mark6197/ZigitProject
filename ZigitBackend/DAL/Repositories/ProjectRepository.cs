using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository 
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Project>> GetAllProjectByUserAsync(long id)
        {
            return await Context.Projects.Where(p=>p.UserId==id).ToListAsync();
        }

        public AppDbContext Context
        {
            get { return _context as AppDbContext; }
        }
    }
}
