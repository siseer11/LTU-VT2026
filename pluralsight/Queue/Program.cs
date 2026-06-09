Queue<(string id, int age)> myQue = new();

myQue.Enqueue(("P1", 32));
myQue.Enqueue(("P2", 12));
myQue.Enqueue(("P3", 120));
myQue.Enqueue(("P4", 85));
myQue.Enqueue(("P5", 210));


Console.WriteLine($"myQue.Count: {myQue.Count} before removing first");
var firstInQue = myQue.Dequeue();
Console.WriteLine($"First one in que id: {firstInQue.id} with a value of {firstInQue.age}");
Console.WriteLine($"myQue.Count: {myQue.Count} after removing first");

var secondInQue = myQue.Peek();
Console.WriteLine($"Second one in que id: {secondInQue.id} with a value of {secondInQue.age}");
Console.WriteLine($"myQue.Count: {myQue.Count} after PEEKING second");



Dictionary<string, string> myObj = new();

myObj.Add("key1", "value1");
// myObj.Add("key1", "value11");

myObj["key1"] = "value2";
myObj["key3"] = "value3";

Console.WriteLine(myObj.GetValueOrDefault("key4"));