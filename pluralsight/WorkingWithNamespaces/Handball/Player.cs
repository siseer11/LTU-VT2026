using System;

namespace WorkingWithNamespaces.Handball;

public class Player
{
	private readonly string def = "This is a handball player";

	public void WritePlayerDetails()
	{
		Console.WriteLine(def);
	}

}
