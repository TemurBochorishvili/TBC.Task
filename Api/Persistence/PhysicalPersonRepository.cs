using Api.Core;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence;

public class PhysicalPersonRepository : IPhysicalPersonRepository
{
    private readonly TaskDbContext context;

    public PhysicalPersonRepository(TaskDbContext context)
    {
        this.context = context;
    }

    public void AddPhysicalPersonRelation()
    {
        throw new NotImplementedException();
    }

    public void CountRelatedPhysicalPersonsByRelationType()
    {
        throw new NotImplementedException();
    }

    public async Task CreatePhysicalPerson(PhysicalPerson physicalPerson)
    {
        await context.PhysicalPersons.AddAsync(physicalPerson);
    }

    public async Task<PhysicalPerson> GetPhysicalPerson(int id)
    {
        return await context.PhysicalPersons
            .Include(pp => pp.PhysicalPersonRelations)
                .ThenInclude(ppr => ppr.Related)
            .Include(pp => pp.PhoneNumbers)
            .Include(pp => pp.City)
            .SingleOrDefaultAsync(pp => pp.Id == id);
    }

    public void GetPhysicalPersons()
    {
        throw new NotImplementedException();
    }

    public void ModifyPhysicalPerson()
    {
        throw new NotImplementedException();
    }

    public void RemovePhysicalPerson(PhysicalPerson physicalPerson)
    {
        context.Remove(physicalPerson);
    }

    public void RemovePhysicalPersonRelation()
    {
        throw new NotImplementedException();
    }

    public void UploadPhysicalPersonPhoto()
    {
        throw new NotImplementedException();
    }
}
