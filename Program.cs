using System;
using System.Collections.Generic;
using CenterSpace.Free;
using System.Linq;

Console.WindowWidth = 75;
Console.WindowHeight = 30;
Console.BufferWidth = 75;

//Shorthand for Console.WriteLine and Clear. Not entirely necessary, just makes output a little easier.
void print(string? what) { Console.WriteLine(what+"\n"); }
void clear() { Console.Clear(); }
bool YesOrNo(string command)
{
	if (command.Equals("n", StringComparison.CurrentCultureIgnoreCase) || command.Equals("no", StringComparison.CurrentCultureIgnoreCase))
	{
		return false;
	}

	if (command.Equals("y", StringComparison.CurrentCultureIgnoreCase) || command.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
	{
		return true;
	}

	return false;
}
//If only C# had macros...

reset:
int pick;
float result;
float storedMean = 0;
float storedDeviation = 0;
NormalDist Distribution;
List<float> dataParsed = new();

Console.Title = "Probability/Distributions Calculator";

start:
clear();
print("Select operation:");
print("1. Standard Deviation");
print("2. Normal Distribution");
print("3: Reset");
print("4. Quit");

int.TryParse(Console.ReadLine(), out pick);

switch (pick)
{
	case 1:
		print("Enter data:");
		string[] data = Console.ReadLine().Split(',', ' ');
		
		foreach (string item in data)
		{
			if (float.TryParse(item, out result))
			{
				dataParsed.Add(result);
			}
		}

		//Found this on Stack Overflow.
		float average = dataParsed.Average();
		float SquareSum = dataParsed.Select(val => (val - average) * (val - average)).Sum();
		float deviation = (float)Math.Sqrt(SquareSum / dataParsed.Count);
		bool yes;

		print("Standard Deviation: " + deviation);
		print("Mean: " + average);
		print("Store result? This will overwrite the previously-saved data.");

		yes = YesOrNo(Console.ReadLine());
		if (yes)
		{
			storedDeviation = deviation;
			print("Stored " + deviation + ".");
			storedMean = average;
			Console.Title = "Probability/Distributions Calculator - D:" + deviation + " M: " + average;
		}

		goto end_prompt;

	case 2:
		print("Use stored variables?");
		yes = YesOrNo(Console.ReadLine());
		if (yes)
        {
			Distribution = new(storedMean, storedDeviation);
			print("Mean: " + Distribution.Mean);
			print("Variance: " + Distribution.Variance);
        }
		else
        {
			goto end_prompt;
        }

		print("Calculate PDF and CDF?");
		yes = YesOrNo(Console.ReadLine());

		if(yes)
        {
			print("Enter value for PDF:");
			float.TryParse(Console.ReadLine(), out float entered);
			print("PDF: " + Distribution.PDF(entered));

			print("Enter value for CDF:");
			float.TryParse(Console.ReadLine(), out float entered2);
			print("CDF: " + Distribution.CDF(entered2));
			goto end_prompt;
		}
		else
        {
			goto end_prompt;
		}

	case 3:
		goto reset;

	case 4: break;
}

end:
return 0;

end_prompt:
print("Is that all?");
bool yes2 = YesOrNo(Console.ReadLine());
if (!yes2)
{
	goto start;
}
else
{
	goto end;
}