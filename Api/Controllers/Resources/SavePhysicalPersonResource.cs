using Api.Core.Models;
using System.Collections.ObjectModel;

namespace Api.Controllers.Resources
{
    public class SavePhysicalPersonResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CityId { get; set; }

        public ICollection<PhoneNumberResource> PhoneNumbers { get; set; }

        public string PictureRelativePath { get; set; }


        public SavePhysicalPersonResource()
        {
            PhoneNumbers = new Collection<PhoneNumberResource>();
        }
    }
}
