using GarageAppV1;

App app = new();

app.Run();

Console.CursorVisible = true;
Console.Clear();
Console.WriteLine("Thanks for using our app! See you next time 👋!");

/*
try
{
	int garageSize = ConsoleUtils.GetValidInt(
		label: $"How many cars should it fit (1-{Garage.MaxCapacity}) : ",
		errorMsg: $"Invalid input, please enter a value between 1-{Garage.MaxCapacity}",
		maxValue: Garage.MaxCapacity,
		minValue: 1
	);

	Garage myBigGarage = new(garageSize);
	ConsoleUtils.LogSuccess($"A garage for {myBigGarage.GarageCapacity} vehicles was built, enjoy it!");
}
catch (ArgumentOutOfRangeException exception)
{
	ConsoleUtils.LogError("App could not be started, garage can not be built!");
	ConsoleUtils.LogError(exception.Message);
}
catch (Exception e)
{
	ConsoleUtils.LogError("App could not be started, garage can not be built!");
	ConsoleUtils.LogError(e.Message);
}
*/