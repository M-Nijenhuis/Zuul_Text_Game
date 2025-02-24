using System;

class Parser
{
	// Holds all valid command words
	private readonly CommandLibrary commandLibrary; 

	// Constructor
	public Parser()
	{
		commandLibrary = new CommandLibrary();
	}

	// Ask and interpret the user input. Return a Command object.
	public Command GetCommand()
	{
		Console.Write("> "); // print prompt

		string word1 = null;
		string word2 = null;

		// string.Split() returns an array
		string[] words = Console.ReadLine().Split(' ');
		if (words.Length > 0) { word1 = words[0]; }
		if (words.Length > 1) { word2 = words[1]; }

		// Now check whether this word is known. If so, create a command with it.
		if (commandLibrary.IsValidCommandWord(word1)) {
			return new Command(word1, word2);
		}

		// If not, create a "null" command (for unknown command).
		return new Command(null, null);
	}

	// Prints a list of valid command words from commandLibrary.
	public void PrintValidCommands()
	{
		Console.WriteLine("Your command words are:");
		Console.WriteLine(commandLibrary.GetCommandsString());
	}
}
