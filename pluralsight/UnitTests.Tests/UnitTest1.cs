namespace UnitTests.Tests;

public class BallTests
{
	[Fact]
	public void HitBall_Adds_NumberOfHits()
	{
		// Arrange
		Ball redBall = new("red", true);

		int numberOfPlayers = 3;

		// Act
		redBall.HitBall(numberOfPlayers);

		// Assert
		Assert.Equal(numberOfPlayers, redBall.HitNthTimes);
	}

	[Fact]
	public void HitBall_Updates_InGoodShape()
	{
		Ball blueBall = new("blue", true);

		// Hit ball 10 times to make it in bad shape
		blueBall.HitBall(10);

		Assert.False(blueBall.InGoodShape);
	}
}
