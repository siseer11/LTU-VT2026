List<int> myList = [10, 20];

void GetItemAtIndex(int index)
{
	Console.WriteLine($"This is the value: {myList[index]}");
}
;

try
{
	Console.WriteLine("Introduce a num, of which element you want to see: ");
	int idx = int.Parse(Console.ReadLine());
	GetItemAtIndex(idx);

}
catch (ArgumentOutOfRangeException iex)
{
	Console.ForegroundColor = ConsoleColor.DarkRed;
	Console.WriteLine($"The value must be between 0-{myList.Count - 1}");
	Console.WriteLine(iex.Message);
}
catch (Exception ex)
{
	Console.ForegroundColor = ConsoleColor.DarkRed;
	Console.WriteLine(ex.Message);
}
finally
{
	Console.ForegroundColor = ConsoleColor.Magenta;
	Console.WriteLine("No matter what you come back home, son! x_X");
	Console.ResetColor();

}

