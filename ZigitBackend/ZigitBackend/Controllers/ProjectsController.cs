using AutoMapper;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZigitBackend.DTOs;

namespace ZigitBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Project>> GetAll()
        {
            if(!long.TryParse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value, out long userId))
                return Unauthorized();

            IList<Project> projects=await _projectRepository.GetAllProjectByUserAsync(userId);

            if (projects.Count == 0)
                return NoContent();

            IList<ProjectDTO> projectDTOs = new List<ProjectDTO>();
            foreach (var project in projects)
                projectDTOs.Add(_mapper.Map<ProjectDTO>(project));

            return Ok(projectDTOs);
        }
    }
}
