using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Persistence.EntityConfigurations;


public class CityConfiguration :
    IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(x => x.Id);

        builder.HasData(
            new City { Id = 1, Name = "Tbilisi" },
            new City { Id = 2, Name = "Sagarejo" },
            new City { Id = 3, Name = "Gurjaani" },
            new City { Id = 4, Name = "Tevali" }
        );
    }
}