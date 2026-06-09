namespace _8_Delegates_Events;

public delegate string CheckNumber(int value);

public class Four
{

	public Four()
	{
		var del = new CheckNumber(IsEven);
		del += IsOdd;

		del(10);
	}

	public static string IsEven(int x)
	{
		if (x % 2 == 0)
		{
			Console.WriteLine("IS EVEN");
			return "EVEN";
		}

		return "Not even";
	}

	public static string IsOdd(int x)
	{
		if (x % 2 == 1)
		{
			Console.WriteLine("IS ODD");
			return "ODD";
		}

		return "Not odd";
	}

}
