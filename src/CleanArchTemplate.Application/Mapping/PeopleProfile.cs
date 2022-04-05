using AutoMapper;
using CleanArchTemplate.Application.Endpoints.People;
using CleanArchTemplate.Application.Endpoints.People.Commands;
using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.Mapping;

public class PeopleProfile : Profile
{
    public PeopleProfile()
    {
        CreateMap<Person, PersonViewModel>()
            .ReverseMap();
        CreateMap<AddPersonCommand, Person>()
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());
    }
}
