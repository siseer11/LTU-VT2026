
using Inheritance;

Animal genericAnimal = new("Base Animal", 120, "Blue");
Dog Zorel = new("Zorel", 12, "Brown", "Os");
SmallDog Lache = new("Lache", 1, "Brown", "Os");

Console.WriteLine($"The dog {Zorel.Name}, is {Zorel.Age} and his fav toy is: {Zorel.FavouriteToy}. A dog has {Dog.numberOfLegs} legs. The dog goes: {Dog.Sound()}");
Console.WriteLine($"The dog {Lache.Name}, is {Lache.Age} and his fav toy is: {Lache.FavouriteToy}. A dog has {Dog.numberOfLegs} legs. The dog goes: {SmallDog.Sound()}");
Console.WriteLine($"{genericAnimal.Name}, {genericAnimal.Age}, {genericAnimal.Color}");