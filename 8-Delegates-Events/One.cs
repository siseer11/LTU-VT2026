
namespace _8_Delegates_Events;

public delegate int UpdateIntHandler(int value);

public class One
{

	public One()
	{
		var del1 = new UpdateIntHandler(Add);
		var del2 = new UpdateIntHandler(Multiply);
		var del3 = new UpdateIntHandler(Remove);

		Console.WriteLine(del1(10));
		Console.WriteLine(del2(2));
		Console.WriteLine(del3(10));
	}


	public static int Add(int value)
	{
		return value + 10;
	}
	public static int Multiply(int value)
	{
		return value * 2;
	}

	public static int Remove(int value)
	{
		return value - 5;
	}
}
