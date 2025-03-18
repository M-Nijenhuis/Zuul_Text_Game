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
    Room outside = new Room("outside the main entrance of the garage");
    Room garageHall = new Room("in a the garage hall");
    Room breakRoom = new Room("in the break room");
    Room toolsRoom = new Room("in a tool room");
    Room storageRoom = new Room("in the storage room");
    Room smallOffice = new Room("in a small office");
    Room garage = new Room("in the garage");
    Room sewer = new Room("in the sewer under the road");
    Room furtherSewer = new Room("futher in the sewer");

    // Initialise room exits
    outside.AddExit("north", garageHall);
    outside.AddExit("down", sewer);
    
    sewer.AddExit("up", outside);
    sewer.AddExit("north", furtherSewer);

    furtherSewer.AddExit("up", garage);

    garageHall.AddExit("west", breakRoom);
    garageHall.AddExit("east", smallOffice);
    garageHall.AddExit("north", garage);
    garageHall.AddExit("south", outside);

    breakRoom.AddExit("east", garageHall);
    smallOffice.AddExit("west", garageHall);

    garage.AddExit("south", garageHall);
    garage.AddExit("west", toolsRoom);
    garage.AddExit("east", storageRoom);

    toolsRoom.AddExit("east", garage);
    storageRoom.AddExit("west", garage);


    // Create your Items here
    Item knife = new Item(10, "A very big knife.");
    Item axe = new Item(5, "A very very big axe.");
    Item entranceKey = new Item(2, "The key for the garage hall");
    Item storageKey = new Item(2, "The key for the storage room");
    Item medkit = new Item(5, "A medkit to get your health fixed");

    // And add them to the Rooms
    sewer.Chest.Put("knife", knife);
    toolsRoom.Chest.Put("axe", axe);
    outside.Chest.Put("entranceKey", entranceKey);
    breakRoom.Chest.Put("storageKey", storageKey);
    breakRoom.Chest.Put("medkit", medkit);

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
      case "use":
        UseItem(command);
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

  private void UseItem(Command command)
  {
    if (command.HasSecondWord()) 
    {
      player.Use(command.SecondWord);
    }
    else 
    {
      Console.WriteLine("Use what?");
    }
  }


  private void Look()
  {
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine($"{player.CurrentRoom.GetLongDescription()}");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("----------------------");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("This items are in the room: " + string.Join(", ", player.CurrentRoom.Chest.ListItems()));
    Console.ResetColor();
  }

  private void Status()
  {
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"Your health is {player.health}/100");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("----------------------");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Your inventory: " + string.Join(", ", player.backpack.ListItems()));
    Console.ResetColor();
  }
}


