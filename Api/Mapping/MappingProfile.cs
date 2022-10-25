using Api.Controllers.Resources;
using Api.Core.Models;
using AutoMapper;

namespace Api.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// Domain to API Resource
		CreateMap<City, CityResource>();
        CreateMap<PhoneNumber, PhoneNumberResource>();
        CreateMap<PhysicalPerson, PhysicalPersonResource>()
			.ForMember(ppr => ppr.City, opt => opt.MapFrom(pp => new CityResource { Id = pp.City.Id, Name = pp.City.Name }))
            .ForMember(
				ppr => ppr.PhoneNumbers,
				opt => opt.MapFrom(pp => pp.PhoneNumbers
					.Select(pn => new PhoneNumberResource { Id = pn.Id, Number = pn.Number, Type = pn.Type }))
			)
            .ForMember(
				ppr => ppr.PhysicalPersonRelations,
				opt => opt.MapFrom(pp => pp.PhysicalPersonRelations
					.Select(ppr => new PhysicalPersonRelationResource
					{
						Id = ppr.RelatedId,
						Name = ppr.Related.Name,
						LastName = ppr.Related.LastName,
						Relation = ppr.Relation
					}))
			);

		// API resource to Domain
		CreateMap<SavePhysicalPersonResource, PhysicalPerson>()
			.ForMember(pp => pp.Id, opt => opt.Ignore())
			.ForMember(
				pp => pp.PhoneNumbers,
				opt => opt.MapFrom(sppr => sppr.PhoneNumbers.Select(pn => new PhoneNumber { Number = pn.Number, Type = pn.Type })));

    }
}
