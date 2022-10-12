using AutoMapper;
using Domain.Entities;
using ZigitBackend.DTOs;

namespace ZigitBackend.Mappers
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>();
        }
    }
}
