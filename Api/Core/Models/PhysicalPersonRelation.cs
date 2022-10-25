namespace Api.Core.Models;

public class PhysicalPersonRelation
{
    public int MasterId { get; set; }

    public int RelatedId { get; set; }

    public Relation Relation { get; set; }


    public virtual PhysicalPerson Master { get; set; }

    public virtual PhysicalPerson Related { get; set; }
}

public enum Relation { Colleague, Acquaintance, Relative, Other }
