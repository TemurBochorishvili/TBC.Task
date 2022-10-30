using Api.Core;

namespace Api.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TaskDbContext context;

    public UnitOfWork(TaskDbContext context)
    {
        this.context = context;
    }

    public async Task Complete()
    {
        await context.SaveChangesAsync();
    }
}
