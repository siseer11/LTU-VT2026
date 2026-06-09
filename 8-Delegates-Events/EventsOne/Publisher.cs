using System;

namespace _8_Delegates_Events.EventsOne;

public class Publisher
{
	public Publisher()
	{
		var subscriber = new Subscriber();

		subscriber.DoSomethingWhenEven += msg =>
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(msg);
			Console.ResetColor();
		};

		for (int i = 0; i < 5; i++)
			subscriber.CheckInt(i);

	}


}
