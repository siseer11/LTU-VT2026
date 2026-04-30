using System;

namespace WorkingWithNamespaces.Football;

public class Player
{
	private readonly string def = "This is a football player";

	public void WritePlayerDetails()
	{
		Console.WriteLine(def);
	}

}
