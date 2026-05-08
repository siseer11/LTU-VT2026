using StaticUsage;

Person billy = new("Billy", 54);
Person bob = new("Bob", 41);

Console.WriteLine("Tax before change:");
billy.LogTax();
bob.LogTax();

Person.tax = 2.5;
Console.WriteLine("\n\nTax after change:");
billy.LogTax();
bob.LogTax();


Dog myDog = new("Zorel");

int? myAge = null;
Console.WriteLine(myAge.HasValue ? "IT does" : "my age is null");

Console.WriteLine(myDog.name);

List<int> myList = [10, 20];

record Dog(string name);


