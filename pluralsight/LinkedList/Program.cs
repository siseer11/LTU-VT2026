LinkedList<string> strings = new();


strings.AddLast("x");
strings.AddLast("x1");
strings.AddLast("x2");
strings.AddLast("x3");
strings.AddLast("x4");


Console.WriteLine(strings.Last?.Previous?.Value);
Console.WriteLine(strings.First?.Next?.Value);



// Get the 4th element in the list
LinkedListNode<string> linkedListNode = strings.First!;

for (int i = 0; i < 3; i++)
	linkedListNode = linkedListNode.Next!;

Console.WriteLine(linkedListNode.Value);


// isert before/after a node
strings.AddBefore(linkedListNode, "before");
strings.AddAfter(linkedListNode, "after");