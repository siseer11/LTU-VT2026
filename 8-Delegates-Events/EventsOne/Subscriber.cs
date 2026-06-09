using System;

namespace _8_Delegates_Events.EventsOne;

public class Subscriber
{
	public event Action<string>? DoSomethingWhenEven;

	public void CheckInt(int value)
	{
		if (value % 2 == 0)
		{
			DoSomethingWhenEven?.Invoke($"YeY, we found an even one: {value}");
		}
	}
}

