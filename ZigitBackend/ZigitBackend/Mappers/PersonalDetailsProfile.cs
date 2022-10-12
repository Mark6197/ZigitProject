using AutoMapper;
using Domain.Entities;
using ZigitBackend.DTOs;

namespace ZigitBackend.Mappers
{
    public class PersonalDetailsProfile:Profile
    {
        public PersonalDetailsProfile()
        {
            CreateMap<PersonalDetails, PersonalDetailsDTO>();
        }
    }
}
