namespace _8_Delegates_Events;

public delegate int MathDelegate(int a, int b);

public class Two
{

	public Two()
	{
		// Använd delegat-variabeln för att skriva ut resultatet för första metoden.
		var AddValue = new MathDelegate(Add);
		Console.WriteLine(AddValue(10, 1));

		// Ge de övriga metoderna som nytt värde till delegat-variabeln så att det gamla värdet försvinner.
		AddValue = new MathDelegate(AddAddAdd);

		// Skapa nu i stället en multicast delegat som kör alla metoder på en gång.
		AddValue = new MathDelegate(Add);
		AddValue += AddAdd;
		AddValue += AddAddAdd;
		Console.WriteLine(AddValue(10, 1));

		// Ta bort en av metoderna så att den inte körs.
		AddValue -= AddAddAdd;
		Console.WriteLine(AddValue!(10, 1));

		// Fråga: Vilken metod körs först?
		// The one that is first in the chain?
	}

	public static int Add(int a, int b) => a + b;
	public static int AddAdd(int a, int b) => a + b + a + b;
	public static int AddAddAdd(int a, int b) => a + b + a + b + a + b;
}
