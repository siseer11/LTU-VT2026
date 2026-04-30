using System;

namespace WorkingWithNamespaces.Basketball;

public class Player
{
	private readonly string def = "This is a basketball player";

	public void WritePlayerDetails()
	{
		Console.WriteLine(def);
	}

}
