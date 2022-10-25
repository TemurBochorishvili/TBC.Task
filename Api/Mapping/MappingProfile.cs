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
		CreateMap<PhysicalPerson, SavePhysicalPersonResource>()
            .ForMember(
				ppr => ppr.PhoneNumbers,
				opt => opt.MapFrom(pp => pp.PhoneNumbers
					.Select(pn => new PhoneNumberResource { Id = pn.Id, Number = pn.Number, Type = pn.Type }))
			);

		// API resource to Domain
		CreateMap<SavePhysicalPersonResource, PhysicalPerson>()
			.ForMember(pp => pp.Id, opt => opt.Ignore())
			.ForMember(pp => pp.PhoneNumbers, opt => opt.Ignore())
            .AfterMap((sppr, v) =>
            {
                //Remove unselected features
                v.PhoneNumbers
                    .Where(f => !sppr.PhoneNumbers.Any(i => (i.Number == f.Number) && (i.Type == f.Type)))
                    .ToList()
                    .ForEach(f => v.PhoneNumbers.Remove(f));

				sppr.PhoneNumbers
					.Where(pnr => !v.PhoneNumbers.Any(pn => (pn.Number == pnr.Number) && (pn.Type == pnr.Type)))
					.Select(pnr => new PhoneNumber { Id = pnr.Id, Number = pnr.Number, Type = pnr.Type })
                    .ToList()
                    .ForEach(pn => v.PhoneNumbers.Add(pn));
            });

    }
}
