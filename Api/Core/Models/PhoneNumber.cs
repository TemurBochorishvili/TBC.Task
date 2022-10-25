namespace Api.Core.Models;

public class PhoneNumber
{
    public int Id { get; set; }

    public string Number { get; set; }

    public int PhysicalPersonId { get; set; }

    public PhoneNumberType Type { get; set; }


    public virtual PhysicalPerson PhysicalPerson { get; set; }
}

public enum PhoneNumberType { Mobile, Office, Home }
