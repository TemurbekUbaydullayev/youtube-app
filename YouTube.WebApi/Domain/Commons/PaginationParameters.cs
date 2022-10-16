namespace YouTube.WebApi.Domain.Commons;

public class PaginationParameters
{
    private const int maxPageSize = 20;
    private int pageSize;

    public int PageSize
    {
        get => pageSize;
        set => pageSize = value > maxPageSize ? maxPageSize : value;
    }

    public int PageIndex { get; set; }
}
