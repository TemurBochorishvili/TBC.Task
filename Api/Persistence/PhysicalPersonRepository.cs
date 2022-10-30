using Api.Core;
using Api.Core.Models;
using Api.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Persistence;

public class PhysicalPersonRepository : IPhysicalPersonRepository
{
    private readonly TaskDbContext context;

    public PhysicalPersonRepository(TaskDbContext context)
    {
        this.context = context;
    }

    public async Task CreatePhysicalPerson(PhysicalPerson physicalPerson)
    {
        await context.PhysicalPersons.AddAsync(physicalPerson);
    }

    public async Task<PhysicalPerson?> GetPhysicalPerson(int id, bool includeRelated = true)
    {
        if (!includeRelated)
            return await context.PhysicalPersons.FindAsync(id);

        return await context.PhysicalPersons
            .Include(pp => pp.PhysicalPersonRelations)
                .ThenInclude(ppr => ppr.Related)
            .Include(pp => pp.PhoneNumbers)
            .Include(pp => pp.City)
            .SingleOrDefaultAsync(pp => pp.Id == id);
    }

    public async Task<IEnumerable<PhysicalPerson>> GetPhysicalPersons(PhysicalPersonQuery queryObj)
    {
        Expression<Func<PhysicalPerson, bool>> predicate = pp =>
            pp.Name.Contains(queryObj.Name ?? "") &&
            pp.LastName.Contains(queryObj.LastName ?? "") &&
            pp.PersonalNumber.Contains(queryObj.PersonalNumber ?? "");

        var query = await context.PhysicalPersons
            .Include(pp => pp.PhysicalPersonRelations)
                .ThenInclude(ppr => ppr.Related)
            .Include(pp => pp.PhoneNumbers)
            .Include(pp => pp.City)
            .Where(predicate)
            .ApplyPaging(queryObj)
            .ToListAsync();

        return query;
    }

    public void RemovePhysicalPerson(PhysicalPerson physicalPerson)
    {
        context.Remove(physicalPerson);
    }

}
