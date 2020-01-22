using System.Linq;
using AutoMapper;
using DatingApp.Api.Dtos;
using DatingApp.Api.Models;

namespace DatingApp.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
            //For PhotUrl
             .ForMember(dest => dest.PhotoUrl, opt =>
             opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
             //For Age
             .ForMember(dest => dest.Age, opt =>
             opt.MapFrom(src => src.DateOfBirth.CalculateAge()));


            CreateMap<User, UserForDetailedDto>()
            //For PhotUrl
             .ForMember(dest => dest.PhotoUrl, opt =>
             opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
             //For Age
             .ForMember(dest => dest.Age, opt =>
             opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto,User>();
        }
    }
}