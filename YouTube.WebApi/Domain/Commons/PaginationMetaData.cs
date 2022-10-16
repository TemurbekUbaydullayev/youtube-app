namespace YouTube.WebApi.Domain.Commons;

public class PaginationMetaData
{
    public PaginationMetaData(int totalCount, int pageSize, int pageIndex)
    {
        CurrentPage = pageIndex;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public int CurrentPage { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}
