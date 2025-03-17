using System;

class Game
{
  // Private fields
  private Parser parser;
  private Player player;

  // Constructor
  public Game()
  {
    parser = new Parser();
    player = new Player();
    CreateRooms();
  }

  // Initialise the Rooms (and the Items)
  private void CreateRooms()
  {
    // Create the rooms
    Room outside = new Room("outside the main entrance of the university");
    Room theatre = new Room("in a lecture theatre");
    Room pub = new Room("in the campus pub");
    Room lab = new Room("in a computing lab");
    Room office = new Room("in the computing admin office");

    // Initialise room exits
    outside.AddExit("east", theatre);
    outside.AddExit("south", lab);
    outside.AddExit("west", pub);

    theatre.AddExit("west", outside);

    pub.AddExit("east", outside);

    lab.AddExit("north", outside);
    lab.AddExit("east", office);

    office.AddExit("west", lab);

    // Create your Items here
    Item knife = new Item(10, "A very big knife.");
    Item axe = new Item(20, "A very very big axe.");
    // And add them to the Rooms
    outside.Chest.Put("knife", knife);
    outside.Chest.Put("axe", axe);

    // Start game outside
    player.CurrentRoom = outside;
  }

  //  Main play routine. Loops until end of play.
  public void Play()
  {
    PrintWelcome();

    // Enter the main command loop. Here we repeatedly read commands and
    // execute them until the player wants to quit.
    bool finished = false;
    while (!finished && player.IsAlive() == true) 
    {
      Command command = parser.GetCommand();
      finished = ProcessCommand(command);
    }
    Console.WriteLine("Thank you for playing.");
    Console.WriteLine("Press [Enter] to continue.");
    Console.ReadLine();
  }

  // Print out the opening message for the player.
  private void PrintWelcome()
  {
    Console.WriteLine();
    Console.WriteLine("Welcome to Zuul!");
    Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
    Console.WriteLine("Type 'help' if you need help.");
    Console.WriteLine();
    Console.WriteLine(player.CurrentRoom.GetLongDescription());
  }

  // Given a command, process (that is: execute) the command.
  // If this command ends the game, it returns true.
  // Otherwise false is returned.
  private bool ProcessCommand(Command command)
  {
    bool wantToQuit = false;

    if (command.IsUnknown())
    {
      Console.WriteLine("I don't know what you mean...");
      return wantToQuit; // false
    }

    switch (command.CommandWord)
    {
      case "help":
        PrintHelp();
        break;
      case "go":
        GoRoom(command);
        break;
      case "quit":
        wantToQuit = true;
        break;
      case "look":
        Look();
        break;
      case "status":
        Status();
        break;
      case "take":
        Take(command);
        break;
      case "drop":
        Drop(command);
        break;
    }

    return wantToQuit;
  }

  // ######################################
  // implementations of user commands:
  // ######################################

  // Print out some help information.
  // Here we print the mission and a list of the command words.
  private void PrintHelp()
  {
    Console.WriteLine("You are lost. You are alone.");
    Console.WriteLine("You wander around at the university.");
    Console.WriteLine();
    // let the parser print the commands
    parser.PrintValidCommands();
  }

  // Try to go to one direction. If there is an exit, enter the new
  // room, otherwise print an error message.
  private void GoRoom(Command command)
  {
    if (!command.HasSecondWord())
    {
      // if there is no second word, we don't know where to go...
      Console.WriteLine("Go where?");
      return;
    }

    string direction = command.SecondWord;

    // Try to go to the next room.
    Room nextRoom = player.CurrentRoom.GetExit(direction);
    if (nextRoom == null)
    {
      Console.WriteLine("There is no door to " + direction + "!");
      return;
    }

    player.CurrentRoom = nextRoom;
    Console.WriteLine(player.CurrentRoom.GetLongDescription());
    player.Damage(10);
  }

  private void Take(Command command)
  {
    if(!command.HasSecondWord()) 
    {
      Console.WriteLine("What item?");
    }
    else
    {
      string itemName = command.SecondWord;
      player.TakeFromChest(itemName);
    }

  }

  private void Drop(Command command)
  {
    if(!command.HasSecondWord())
    {
      Console.WriteLine("What item?");
    }
    else
    {
      string itemName = command.SecondWord;
      player.DropToChest(itemName);
    }
     
  }


  private void Look()
  {
    Console.WriteLine($"{player.CurrentRoom.GetLongDescription()}");
    Console.WriteLine("-----------------");

    Console.WriteLine(player.CurrentRoom.Chest.Show());
  }

  private void Status()
  {
    Console.WriteLine($"You're health is {player.health}/100");
    Console.WriteLine("-----------------");
    Console.WriteLine(player.backpack.Show());
  }
}


