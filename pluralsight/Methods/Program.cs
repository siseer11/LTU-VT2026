/*
	Pretty much exactly like the JS functions
*/

using Methods;

static int SumOf2(int a, int b)
{
	return a + b;
}
Console.WriteLine(SumOf2(10, 2));

static void LogSumOf2(int a, int b)
{
	Console.WriteLine(a + b);
}
LogSumOf2(11, 1);



int sumOf3 = MyMath.Add3Numbers(10, 1, 2);
Console.WriteLine($"Sum of 3 numbers, {sumOf3} , coming from another file, wow wow!");