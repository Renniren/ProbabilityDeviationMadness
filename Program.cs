using System;
using System.Collections.Generic;
using CenterSpace.Free;
using System.Linq;

Console.WindowWidth = 75;
Console.WindowHeight = 30;
Console.BufferWidth = 75;

//Shorthand for Console.WriteLine and Clear. Not entirely necessary, just makes output a little easier.
void print(string what) { Console.WriteLine(what+"\n"); }
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

		print("Standard Deviation: " + deviation.ToString());
		print("Store result? This will overwrite the previously-saved deviation.");

		yes = YesOrNo(Console.ReadLine());
		if (yes)
        {
			storedDeviation = deviation;
			print("Stored " + deviation.ToString() + ".");
			Console.Title = "Probability/Distributions Calculator - " + deviation.ToString();
		}

        print("Is that all?");
		yes = YesOrNo(Console.ReadLine());
		if (!yes)
        {
			goto start;
        }
		else
        {
			break;
        }


	case 2:

		break;

	case 3:
		goto reset;
		break;

	case 4: break;
}

end:
return 0;

