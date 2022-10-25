namespace Api.Extensions;

public interface IQueryObject
{
    string SortBy { get; set; }

    SortType SortType { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }
}

public enum SortType { Ascending, Descending };
