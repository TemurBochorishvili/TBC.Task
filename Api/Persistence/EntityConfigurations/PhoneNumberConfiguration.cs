using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Persistence.EntityConfigurations;


public class PhoneNumberConfiguration :
    IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.ToTable("PhoneNumbers");

        builder.HasKey(x => x.Id);

        builder.HasData(
            new PhoneNumber { Id = 1, Number = "551 75 67 76", PhysicalPersonId = 1, Type = PhoneNumberType.Mobile },
            new PhoneNumber { Id = 2, Number = "032 2 75 67 76", PhysicalPersonId = 1, Type = PhoneNumberType.Home },
            new PhoneNumber { Id = 3, Number = "551 12 13 76", PhysicalPersonId = 2, Type = PhoneNumberType.Mobile },
            new PhoneNumber { Id = 4, Number = "032 2 33 22 76", PhysicalPersonId = 2, Type = PhoneNumberType.Home },
            new PhoneNumber { Id = 5, Number = "551 12 13 57", PhysicalPersonId = 3, Type = PhoneNumberType.Mobile },
            new PhoneNumber { Id = 6, Number = "032 2 64 55 76", PhysicalPersonId = 3, Type = PhoneNumberType.Home }
        );
    }
}