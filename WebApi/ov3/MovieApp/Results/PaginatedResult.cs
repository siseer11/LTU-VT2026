namespace MovieApp.Results;

public class PaginatedResult<DataType>
{
	public IEnumerable<DataType> Data { get; set; } = [];
	public PaginationMetadata Pagination { get; set; } = null!;
}