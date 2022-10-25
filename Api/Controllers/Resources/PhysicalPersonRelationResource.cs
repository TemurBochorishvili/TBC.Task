using Api.Core.Models;

namespace Api.Controllers.Resources;

public class PhysicalPersonRelationResource
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public Relation Relation { get; set; }
}
