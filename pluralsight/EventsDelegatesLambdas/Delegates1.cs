using System;

namespace EventsDelegatesLambdas;

public delegate string SendEmailHandler(string txt, int id);
public delegate int WorkPerformedHandler(int hours, string job);

public class Delegates1
{

	public Delegates1()
	{
		var del1 = new WorkPerformedHandler(WriteBugs);
		var del2 = new WorkPerformedHandler(FixBugs);

		del1 += del2;

		Console.WriteLine(del1(1, "C#"));
	}


	static string SendImportantEmail(string txt, int id)
	{
		return $"Your (IMPORTANT) email with id: {id} was sent!";
	}

	static string SendNotImportantEmail(string txt, int id)
	{
		return $"Your (Not important) email with id: {id} was sent!";
	}

	static int WriteBugs(int hours, string job)
	{
		Console.WriteLine($"Wrote bugs on: {job}");
		return hours + 1;
	}

	static int FixBugs(int hours, string job)
	{
		Console.WriteLine($"Fixed bugs on: {job}");
		return hours + 10;
	}

}
