using GarageAppV2;

// FORCE the terminal to use UTF-8 encoding (Fixes Windows "??" bugs)
System.Console.OutputEncoding = System.Text.Encoding.UTF8;

App app = new();

app.Run();

Console.CursorVisible = true;
Console.Clear();
Console.WriteLine("Thanks for using our app! See you next time 👋!");
