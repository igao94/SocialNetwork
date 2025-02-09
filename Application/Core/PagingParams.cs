namespace Application.Core;

public class PagingParams
{
    public const int MinPageNumber = 1;
    public const int MaxPageNumber = 10;
    public const int MinPageSize = 1;
    public const int MaxPageSize = 10;

    private int _pageNumber = MinPageNumber;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = ValidatePageNumber(value);
    }

    private int _pageSize = 5;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = ValidatePageSize(value);
    }

    private static int ValidatePageNumber(int value)
    {
        if (value < MinPageNumber) return MinPageNumber;

        if (value > MaxPageNumber) return MaxPageNumber;

        return value;
    }

    private static int ValidatePageSize(int value)
    {
        if (value < MinPageSize) return MinPageSize;

        if (value > MaxPageSize) return MaxPageSize;

        return value;
    }
}
