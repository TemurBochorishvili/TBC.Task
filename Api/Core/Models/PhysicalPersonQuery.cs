using Api.Extensions;

namespace Api.Core.Models;

public class PhysicalPersonQuery : IQueryObject
{
    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? PersonalNumber { get; set; }

    public string SortBy { get; set; }

    public SortType SortType { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }
}
