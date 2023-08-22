using AutoMapper;
using LoginPractice.Models;
using LoginPractice.Models.Dto;

namespace LoginPractice
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PersonalDetails, PersonalDetailsDTO>().ReverseMap();
            CreateMap<PersonalDetails,PersonalDetailsCreateDTO>().ReverseMap();
            CreateMap<PersonalDetails,PersonalDetailsUpdateDTO>().ReverseMap();
        }
    }
}
