namespace _8_Delegates_Events;

public class OneArrow
{

	public OneArrow()
	{
		Console.WriteLine(DoMathWithEven(10, x => x + 1));
		Console.WriteLine(DoMathWithEven(9, x => x + 1));
		Console.WriteLine(DoMathWithEven(9, x => x - 1));
		Console.WriteLine(DoMathWithEven(9, x => x / 2));
	}

	public static int DoMathWithEven(int value, Func<int, int> Calc)
	{
		if (value % 2 != 0)
			value += 1;

		return Calc.Invoke(value);
	}
}
