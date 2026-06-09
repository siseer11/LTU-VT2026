using WiredBrainCoffee.DataProcessor.Parsing;

namespace WiredBrainCoffe.DataProcessor.Tests.Parsing;

public class CsvLineParserTests
{
	[Fact]
	public void ShouldParseValidLine()
	{
		string[] csvLines = ["Cappuccino;10/27/2022 8:06:04 AM"];

		var machineDataItems = CsvLineParser.Parse(csvLines);

		Assert.NotNull(machineDataItems);
		Assert.Single(machineDataItems);
		Assert.Equal("Cappuccino", machineDataItems[0].CoffeeType);
		Assert.Equal(new DateTime(2022, 10, 27, 8, 6, 4), machineDataItems[0].CreatedAt);
	}

	[Fact]
	public void ShouldSkipEmptyLines()
	{
		string[] csvLines = ["", "  "];

		var machineDataItems = CsvLineParser.Parse(csvLines);

		Assert.NotNull(machineDataItems);
		Assert.Empty(machineDataItems);
	}


	[InlineData("Cappucino", "Invalid csv line:")]
	[InlineData("Cappuciano;InvalidDate", "Invalid date in csv line:")]
	[Theory]
	public void ShouldThrowExceptionWithMessage(string csvLine, string expectedMsgPrefix)
	{
		string[] csvLines = [csvLine];

		var excetion = Assert.Throws<Exception>(() => CsvLineParser.Parse(csvLines));

		Assert.Equal($"{expectedMsgPrefix} {csvLine}", excetion.Message);
	}
}
