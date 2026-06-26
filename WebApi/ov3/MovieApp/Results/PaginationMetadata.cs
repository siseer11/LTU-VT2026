namespace MovieApp.Results;

public class PaginationMetadata
{
	public int CurrentPage { get; set; }
	public int ItemsPerPage { get; set; }
	public int TotalItemsCount { get; set; }
	public int TotalPages { get; set; }

	public PaginationMetadata(int currentPage, int itemsPerPage, int totalItemsCount)
	{
		CurrentPage = currentPage;
		ItemsPerPage = itemsPerPage;
		TotalItemsCount = totalItemsCount;
		TotalPages = (int)Math.Ceiling(TotalItemsCount / (double)ItemsPerPage);
	}
}