using UnitTests;

Ball redBall = new(color: "red", inGoodShape: true);

for (int i = 0; i < 10; i++)
{
	redBall.HitBall();
}