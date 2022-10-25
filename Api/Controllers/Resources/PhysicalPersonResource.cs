using Api.Core.Models;
using System.Collections.ObjectModel;

namespace Api.Controllers.Resources
{
    public class PhysicalPersonResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public CityResource City { get; set; }

        public ICollection<PhoneNumberResource> PhoneNumbers { get; set; }

        public ICollection<PhysicalPersonRelationResource> PhysicalPersonRelations { get; set; }

        public string PictureRelativePath { get; set; }


        public PhysicalPersonResource()
        {
            PhysicalPersonRelations = new Collection<PhysicalPersonRelationResource>();
            PhoneNumbers = new Collection<PhoneNumberResource>();
        }
    }
}
