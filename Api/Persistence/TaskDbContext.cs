using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Api.Persistence;


public class TaskDbContext : DbContext
{
    public DbSet<PhysicalPerson> PhysicalPersons { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<PhysicalPersonRelation> RelatedPersons { get; set; }


    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
