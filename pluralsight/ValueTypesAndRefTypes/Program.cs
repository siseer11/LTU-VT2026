/*
	Ref types are a bit like arrays in JS, so if you pass an array to a function OR make let b = arr1; Making changes to that array
	without making a copy of it, will change the initial one as well.

	All the "default" value types (such as int/double.... NOT STRINGS) are stored directly on the Stack while for ex an Object has a ref
	stored on the Stack but the value of the ref is on the Heap ( so it will behave like the array case in JS )
*/

using System.Text;
using ValueTypesAndRefTypes;

int a = 10;
int b = a;
b = 20; // => a = 10, b = 20;


Car car1 = new(1993, "Honda");
Car car2 = car1;

car2.brand = "Toyota"; // both car1 and car2 brand will be updated, due to both car1 && car2 ref the same object in the memory

car1.LogDetails();
car2.LogDetails();





// Passing ref as a param. 
int x = 1;
int w = 1;
int c = 1; // The ref must be initialized

Console.WriteLine($"value of c before is: {c}");
void addNumbers(int a, int b, ref int c)
{
	c += 10; // this will update the actual value of c, since c is passed as a "ref" and not a "copy"

	Console.WriteLine(a + b + c);
}

addNumbers(x, w, ref c);

Console.WriteLine($"value of c after is: {c}");

// Using out, Same as ref but with out the value does not have to be initialized
string firstLetter;
string restOfString = "bcde";

void createString(string a, out string q)
{
	q = "now it has a value";
	Console.WriteLine($"This is the amazing string: {q}-{a}");
}

createString(restOfString, out firstLetter);
Console.WriteLine(firstLetter);




// Strings are Ref too, but a string is imutable so a new ref to the new string will be created all the time
string o = "my happy string";
string k = o;
k += ", not anymore!"; // k will be "my happy string, not anymore!" but since a new ref was created, only k will point to it so the first string will keep its initial value/ref

/*
 because strings are always creating copies (since they are immutable), if working with "hard/intense" string manipulation
 is worth/recomanded to use the StringBuilder()
*/
Car myNewCar = new Car(2026, "Mazda");
StringBuilder builder = new();
builder.Append("Car brand:");
builder.AppendLine($"\t{myNewCar.brand}");
builder.Append("Year:");
builder.Append($"\t\t{myNewCar.year}");
string result = builder.ToString();
Console.WriteLine(result);




/*
	Enums
*/
Languages myFavouriteLanguage = Languages.Romanian;
Console.WriteLine(myFavouriteLanguage);


/*
	Struct - almost like classes, but for less intense/heavy stuff (The tutorial guy said its rarely used by him)
*/
WorkTask task;
task.hours = 8;
task.workDone = "c#";
task.LogWorkDone();

enum Languages
{
	Romanian,
	Swedish,
	English
};

internal struct WorkTask
{
	public int hours;
	public string workDone;

	public void LogWorkDone()
	{
		Console.WriteLine($"It took {hours} to do the {workDone}");
	}
}
