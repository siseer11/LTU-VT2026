using Polymorphism;

Animal baseAnimal = new("base", 15);
Animal myDog = new Dog("Doggo", 1);
Cat myCat = new("Catty", 2);
Duck randomDuck = new("Ducky", 5, "yellow");

List<Animal> animalsList = [baseAnimal, myDog, myCat, randomDuck];

foreach (Animal animal in animalsList)
{
	animal.MakeSound();
}