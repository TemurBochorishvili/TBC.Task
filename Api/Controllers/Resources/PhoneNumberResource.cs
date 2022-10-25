using Api.Core.Models;

namespace Api.Controllers.Resources;

public class PhoneNumberResource
{
    public int Id { get; set; }

    public string Number { get; set; }

    public PhoneNumberType Type { get; set; }
}
