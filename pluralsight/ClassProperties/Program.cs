using ClassProperties;

Person Billy = new("Billy", 30);

Console.WriteLine($"{Billy.Name} is {Billy.Age}");

// We can read the OnlyUpdateFromWhitin, since the get is public
// But we can not set it since is private
Console.WriteLine(Billy.OnlyUpdateFromWhitin);

// While we can read and set Name and Age
Billy.Name = "Billy B";
Billy.Age += 1;