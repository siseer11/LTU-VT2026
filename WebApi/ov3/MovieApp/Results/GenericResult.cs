namespace MovieApp.Results;

public class GenericResult<DataType, ErrorType>
{
	public bool Success { get; set; }

	public ErrorType? ErrorCode { get; set; }

	public DataType? Data { get; set; }
}