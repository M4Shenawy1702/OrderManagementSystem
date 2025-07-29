namespace OrderManagementSystem.Application.DOTs
{
    public record PaginatedResult<TData>(int PageIndex, int PageSize, int Count, IEnumerable<TData> Data);
}
