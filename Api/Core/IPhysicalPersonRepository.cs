using Api.Core.Models;

namespace Api.Core;

public interface IPhysicalPersonRepository
{
    Task CreatePhysicalPerson(PhysicalPerson physicalPerson);

    void RemovePhysicalPerson(PhysicalPerson physicalPerson);

    Task<PhysicalPerson> GetPhysicalPerson(int id, bool includeRelated = true);

    Task<IEnumerable<PhysicalPerson>> GetPhysicalPersons(PhysicalPersonQuery queryObj);
}
