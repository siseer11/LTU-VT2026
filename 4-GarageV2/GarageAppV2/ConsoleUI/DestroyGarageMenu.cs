using System;
using GarageAppV2.FileData;
using GarageAppV2.Shared;

namespace GarageAppV2.ConsoleUI;

public class DestroyGarageMenu : Menu
{
	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🅿️ Garage", "(destroy the garage 💥)"], '-');

		bool userConfirms = ConsoleUtils.YesOrNoUserConfirmation("\n\n⚠️  Are you sure you want to destroy the garage?\n");

		if (userConfirms)
		{
			bool successfull = AppStore.Garage.DeleteGarage(); ;

			if (successfull)
			{
				Console.WriteLine("\n");
				Console.WriteLine("🚧 Taking vehicles out");
				Thread.Sleep(500);
				Console.WriteLine("🚧 Demolishing garage... BOOM 💥!");
				Thread.Sleep(500);
				ConsoleUtils.LogSuccess("Garage demolished successfully!");
				Console.WriteLine("Press any key to go back to main menu.");
				Console.ReadKey(true);
				NavigateToMainMenu();
			}
			else
			{
				ConsoleUtils.LogError("Something went wrong while destroying the garage!");

				Console.WriteLine("Press any key to go back.");
				Console.ReadKey(true);
				NavigateToGarageMenu();
			}


		}
		else
		{
			Console.WriteLine("Good! The garage survives again! 👍\nPress anyting to go back!");
			Console.ReadKey(true);
			NavigateToGarageMenu();

		}
	}
}

// 🚧