using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Api.Persistence.EntityConfigurations;


public class PhysicalPersonConfiguration :
    IEntityTypeConfiguration<PhysicalPerson>,
    IEntityTypeConfiguration<PhysicalPersonRelation>
{
    public void Configure(EntityTypeBuilder<PhysicalPerson> builder)
    {
        builder.ToTable("PhysicalPersons");

        builder.HasKey(x => x.Id);

        //builder
        //    .HasMany(pp => pp.PhysicalPersonRelations)
        //    .WithOne(pp => pp.Master)
        //    .HasForeignKey(pp => pp.MasterId)
        //    .OnDelete(DeleteBehavior.ClientCascade);

        //builder
        //    .HasMany(pp => pp.PhysicalPersonRelationsOf)
        //    .WithOne(pp => pp.Related)
        //    .HasForeignKey(pp => pp.RelatedId)
        //    .OnDelete(DeleteBehavior.ClientCascade);


        builder.HasData(
            new PhysicalPerson
            { 
                Id = 1,
                Name = "Temo",
                LastName = "Botchorishvili",
                Gender = Gender.Male,
                PersonalNumber = "01011086652",
                DateOfBirth = new DateTime(1996, 7, 25),
                CityId = 1,
                PictureRelativePath = "C://",
            },
            new PhysicalPerson
            { 
                Id = 2,
                Name = "Kote",
                LastName = "Zibzibadze",
                Gender = Gender.Male,
                PersonalNumber = "01211086652",
                DateOfBirth = new DateTime(1996, 1, 25),
                CityId = 2,
                PictureRelativePath = "C://",
            },
            new PhysicalPerson
            {
                Id = 3,
                Name = "Soso",
                LastName = "Egadze",
                Gender = Gender.Male,
                PersonalNumber = "01211086652",
                DateOfBirth = new DateTime(1996, 1, 25),
                CityId = 1,
                PictureRelativePath = "C://",
            }
        );
    }

    public void Configure(EntityTypeBuilder<PhysicalPersonRelation> builder)
    {
        builder.ToTable("PhysicalPersonRelations");

        builder.HasKey(ppr => new { ppr.MasterId, ppr.RelatedId });

        builder
            .HasOne(ppr => ppr.Master)
            .WithMany(pp => pp.PhysicalPersonRelations)
            .HasForeignKey(ppr => ppr.MasterId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(ppr => ppr.Related)
            .WithMany(pp => pp.PhysicalPersonRelationsOf)
            .HasForeignKey(ppr => ppr.RelatedId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasData(
            new PhysicalPersonRelation { MasterId = 1, RelatedId = 2, Relation = Relation.Relative },
            new PhysicalPersonRelation { MasterId = 1, RelatedId = 3, Relation = Relation.Colleague },
            new PhysicalPersonRelation { MasterId = 2, RelatedId = 3, Relation = Relation.Other }
        );
    }
}