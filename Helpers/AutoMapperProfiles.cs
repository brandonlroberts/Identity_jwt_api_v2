using AutoMapper;
using Identity_JWT_API.DTOs;
using Identity_JWT_API.Extensions.Entities;

namespace Identity_JWT_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<RegisterDto, AppUser>();
        }
    }
}