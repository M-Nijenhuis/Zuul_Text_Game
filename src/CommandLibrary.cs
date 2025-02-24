using System.Collections.Generic;

class CommandLibrary
{
	// A List that holds all valid command words
	private readonly List<string> validCommands;

	// Constructor - initialise the command words.
	public CommandLibrary()
	{
		validCommands = new List<string>();

		validCommands.Add("help");
		validCommands.Add("go");
		validCommands.Add("quit");
	}

	// Check whether a given string is a valid command word.
	// Return true if it is, false if it isn't.
	public bool IsValidCommandWord(string instring)
	{
		return validCommands.Contains(instring);
	}

	// returns a list of valid command words as a comma separated string.
	public string GetCommandsString()
	{
		return String.Join(", ", validCommands);
	}
}
