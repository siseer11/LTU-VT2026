using System;
using System.Text;

namespace GarageAppV1.ConsoleUI;

public partial class ConsoleUtils
{

	/*
	* Function that takes a text and returns a string of the wished length
	* usefull for tables, so every column stays aligned, keeping the same size top to bottom
	*/
	private static string ReturnValueOfSize(string txt, int size, int paddingRight)
	{
		string trimmed = txt.Length > size ? $"{txt[..(size - 2)]}.." : txt;

		return $"{trimmed}{new string(' ', paddingRight + (size - trimmed.Length))}";
	}

	public static void RenderTable<T>((T id, int columnGap, int columnWidht)[] columnSettings, List<Dictionary<T, string>> rows, bool? showLinesInBetween)
	{
		int tableWidht = 0;
		foreach (var col in columnSettings)
			tableWidht += col.columnGap + col.columnWidht;

		for (int i = 0; i < rows.Count; i++)
		{
			var rowData = rows[i];
			StringBuilder rowString = new();

			for (int j = 0; j < columnSettings.Length; j++)
			{
				(T columnId, int columnGap, int columnWidht) = columnSettings[j];
				rowString.Append(ReturnValueOfSize(rowData[columnId], columnWidht, columnGap));
			}

			Console.WriteLine(rowString.ToString());

			if (showLinesInBetween == true)
			{
				if (i == 0) // The table header, render line between header and content
					LogColor(new string('-', tableWidht), ConsoleColor.Gray);
				else
					LogColor(new string('-', tableWidht), ConsoleColor.DarkGray);
			}
		}
	}

}
