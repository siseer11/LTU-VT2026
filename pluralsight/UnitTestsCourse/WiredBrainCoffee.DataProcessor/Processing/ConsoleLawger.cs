using System;

namespace WiredBrainCoffee.DataProcessor.Processing;

public class ConsoleLawger
{
	private readonly TextWriter _textWriter;

	public ConsoleLawger() : this(Console.Out) { }

	public ConsoleLawger(TextWriter textWriter)
	{
		_textWriter = textWriter;
	}

	public void Write(string text)
	{
		_textWriter.WriteLine(text);
	}

}
