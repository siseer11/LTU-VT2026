/*
	Switch is almost like in JS, but you get to use "< >...." is not only for equality
*/

/*
Console.WriteLine("Type your salary");

int salary = int.Parse(Console.ReadLine());

switch (salary)
{
	case 4000:
		Console.WriteLine("Nothing beats it!");
		break;
	case > 3000:
		Console.WriteLine("You can buy a nice bike!");
		break;
	case < 500:
	case > 2000:
		Console.WriteLine("Get a cheap bike, sorry!");
		break;
	default:
		Console.WriteLine("Unknown salary range!");
		break;
}
*/

Console.WriteLine("Enter a number, press 4 to exit:");

int userInput = int.Parse(Console.ReadLine());

while (userInput != 4)
{
	switch (userInput)
	{
		case 1:
			Console.WriteLine("Menu 1");
			break;
		case 2:
			Console.WriteLine("Menu 2, might be the best");
			break;
		case 3:
			Console.WriteLine("Menu 3, is the best");
			break;
		default:
			Console.WriteLine("Unknown");
			break;
	}
	Console.WriteLine("Enter a number, press 4 to exit:");
	userInput = int.Parse(Console.ReadLine());
}
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("🏂 Done! Time for 🚲!");