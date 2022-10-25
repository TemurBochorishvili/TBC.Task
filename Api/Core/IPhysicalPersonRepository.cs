using Api.Core.Models;

namespace Api.Core;

public interface IPhysicalPersonRepository
{
    Task CreatePhysicalPerson(PhysicalPerson physicalPerson);

    void ModifyPhysicalPerson();

    void UploadPhysicalPersonPhoto();

    void AddPhysicalPersonRelation();

    void RemovePhysicalPersonRelation();

    void RemovePhysicalPerson(PhysicalPerson physicalPerson);

    Task<PhysicalPerson> GetPhysicalPerson(int id);

    void GetPhysicalPersons();

    void CountRelatedPhysicalPersonsByRelationType();
}
