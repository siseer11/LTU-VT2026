using Interface;

IAnimal randomAnimal = new Animal("Bonky");
IAnimal myDog = new Dog("Bmx", "black");

randomAnimal.DoSound();
Console.WriteLine(randomAnimal.NameWithSomethingAtTheEnd('!'));

myDog.DoSound();
Console.WriteLine(myDog.NameWithSomethingAtTheEnd('#'));