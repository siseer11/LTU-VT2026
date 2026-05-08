using System;

namespace MyShop.Inventory.Utils;

public class ConsoleUtils
{
	public static void Log(string msg, ConsoleColor? color = null)
	{
		if (color is ConsoleColor pureColor)
		{
			Console.ForegroundColor = pureColor;
			Console.WriteLine(msg);
			Console.ResetColor();
		}
		else
		{
			Console.WriteLine(msg);
		}

	}
}
