using WiredBrainCoffee.DataProcessor.Processing;

namespace WiredBrainCoffe.DataProcessor.Tests.Processing;

public class CsvLineParserTests
{
	[Fact]
	public void LawgerDoesTheLog()
	{
		var stringWritter = new StringWriter();
		var lawger = new ConsoleLawger(stringWritter);

		lawger.Write("Hello there, test!");

		var result = stringWritter.ToString();
		Assert.Equal("Hello there, test!\n", result);
	}
}