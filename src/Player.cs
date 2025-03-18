class Player
{
  public Room CurrentRoom { get; set; }

  //Private fields
  private const int _maxHealth = 100;
  public Inventory backpack;

  //Public Fields
  public int health;


  public Player() 
  {
    CurrentRoom = null;
    health = 100;

    backpack = new Inventory(25);
  }


  //Methods
  public void Damage(int amount)
  {
    health -= amount;
  }

  public void Heal(int amount) 
  {
    health += amount;
  }

  public bool IsAlive()
  {
    if (health <= 0) {
      return false;
    }

    return true;
  }

  public bool TakeFromChest(string itemName)
  {
    Item item = CurrentRoom.Chest.Get(itemName); 

    if (item != null)
    {
      Console.WriteLine(item);
      if(backpack.Put(itemName, item) == true)
      {
        Console.WriteLine($"The {itemName} is put in to your backpack.");
        return true;
      }

      CurrentRoom.Chest.Put(itemName, item);
      Console.WriteLine($"The {itemName} does not fit in your backpack.");
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

    if (CurrentRoom.Chest.Put(itemName, item) == true)
    {
      Console.WriteLine($"The {itemName} is droped in the room");
      return true;
    }

    return false;
  }

  public string Use(string itemName)
  {
    return null;
  }
}
