class Player
{
  public Room CurrentRoom { get; set; }

  //Private fields
  private const int _maxHealth = 100;
  public int backpackSpace { get; private set; }
  public Inventory backpack { get; set; }

  //Public Fields
  public int health;


  public Player()
  {
    CurrentRoom = null;
    health = 100;

    backpackSpace = 20;
    backpack = new Inventory(backpackSpace);
  }


  //Methods
  public void Damage(int amount)
  {
    health -= amount;
  }

  public void Heal(int amount)
  {
    health += amount;
    if (health > 100)
    {
      health = _maxHealth;
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Your health is now {health}/{_maxHealth}");
    Console.ResetColor();
  }

  public bool IsAlive()
  {
    if (health <= 0)
    {
      return false;
    }

    return true;
  }

  public bool TakeFromChest(string itemName)
  {
    Item item = CurrentRoom.Chest.Get(itemName);

    if (item != null)
    {
      if (backpack.CheckIfItemIsInInventory(itemName) == true)
      {
        Console.WriteLine("You can only have 1 of these items in your backpack");
      }
      else if (backpack.Put(itemName, item) == true)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"The {itemName} is put in to your backpack.");
        Console.ResetColor();
        return true;
      }

      CurrentRoom.Chest.Put(itemName, item);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"The {itemName} does not fit in your backpack.");
      Console.ResetColor();
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("The thing you want to pick up does not exist");
      Console.ResetColor();
    }

    return false;
  }

  public bool DropToChest(string itemName)
  {
    Item item = backpack.Get(itemName);
    if (CurrentRoom.Chest.CheckIfItemIsAvailible(itemName))
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("This item cannot be droped in this room");
      Console.ResetColor();
    }
    else if (item != null)
    {
      if (CurrentRoom.Chest.Put(itemName, item) == true)
      {
        Console.WriteLine($"The {itemName} is droped in the room");
        return true;
      }
    }
    else
    {
      Console.WriteLine($"{itemName} is not in your backpack");
    }

    return false;

  }

  public string Use(string itemName, string useDirection)
  {

    if(backpack.CheckIfItemIsAvailible(itemName) == true)
    {
      switch (itemName)
      {
        case "knife":
          UseKnife();
          return itemName;
        case "axe":
          UseAxe();
          return itemName;
        case "medkit":
          UseMetkit(itemName);
          return itemName;
        case "whisky":
          UseWhisky(itemName);
          return itemName;
        case "key":
          if (useDirection != null)
          {
            UseKey(itemName, useDirection);
            return itemName;
          }
          else
          {
            Console.WriteLine("what direction");
          }
          return null;
      }
    }
    else
    {
      Console.WriteLine("THere is no item that is caled dat");
    }

    return null;
  }

  private void UseKey(string itemName, string useDirection)
  {
    Room nextRoom = CurrentRoom.GetExit(useDirection);
    if (nextRoom != null && nextRoom.isLocked == false)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("That room is not locked");
      Console.ResetColor();
    }
    else if (nextRoom != null)
    {
      backpack.Get(itemName);
      nextRoom.isLocked = false;
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"Used the key to the {useDirection}");
      Console.ResetColor();
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("There is no room in that direction");
      Console.ResetColor();
    }
  }

  private void UseKnife()
  {
    Damage(40);
    Console.WriteLine("You stabed yourself! Seek a medkit fast!");
  }

  private void UseAxe()
  {
    Console.WriteLine("balbalablba");
  }

  private void UseMetkit(string itemName)
  {
    backpack.Get(itemName);
    Heal(40);
  }

  private void UseWhisky(string itemName)
  {
    backpack.Get(itemName);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("You feel very happy now!");
    Console.ResetColor();
    Heal(100);
  }
}










