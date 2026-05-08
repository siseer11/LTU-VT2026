using System;
using _2_FlowControl.Features;

namespace _2_FlowControl;

public class App
{
	public static bool appRunning = true;

	public static void StopApp()
	{
		appRunning = false;
	}

	public void Run()
	{
		MainMenu mainmenu = new();

		while (appRunning)
		{
			mainmenu.Run();
		}

		Console.Clear();
		Console.WriteLine("\n   -- App Closed --\nThanks for using our app!\n");
	}
}
