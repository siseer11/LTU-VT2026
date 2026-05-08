using System;

namespace UnitTests;

public class Ball(string color, bool inGoodShape)
{
	public string Color { get; set; } = color;
	public bool InGoodShape { get; set; } = inGoodShape;

	public int HitNthTimes { get; set; } = 0;

	public void HitBall()
	{
		HitBall(1);
	}

	public void HitBall(int numberOfPlayers)
	{
		HitNthTimes += numberOfPlayers;

		if (HitNthTimes >= 10)
		{
			InGoodShape = false;
			Console.WriteLine($"The {Color} ball it's been hit {HitNthTimes} and its not in good shape.");
		}
		else
		{
			Console.WriteLine($"The {Color} ball it's been hit {HitNthTimes} and its in good shape.");
		}
	}
}
