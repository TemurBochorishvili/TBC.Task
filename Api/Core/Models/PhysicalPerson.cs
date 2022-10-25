using System.Collections.ObjectModel;

namespace Api.Core.Models;


public class PhysicalPerson
{
    public PhysicalPerson()
    {
        PhysicalPersonRelations = new Collection<PhysicalPersonRelation>();
        PhysicalPersonRelationsOf = new Collection<PhysicalPersonRelation>();
        PhoneNumbers = new Collection<PhoneNumber>();
    }


    public int Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public Gender Gender { get; set; }

    public string PersonalNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int CityId { get; set; }

    public string PictureRelativePath { get; set; }

    public virtual ICollection<PhysicalPersonRelation> PhysicalPersonRelations { get; set; }

    public virtual ICollection<PhysicalPersonRelation> PhysicalPersonRelationsOf { get; set; }

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }


    public virtual City City { get; set; }
}

public enum Gender { Male, Female }
