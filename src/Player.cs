class Player
{
  public Room CurrentRoom { get; set; }

  //Private fields
  private const int _maxHealth = 100;
  private Inventory backpack;

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
    Item item = CurrentRoom.Chest.RemoveItem(itemName); 

    if(backpack.GetItem(itemName, item) == true)
    {
      Console.WriteLine($"The {itemName} is put in youre backpack.");
      return true;
    }

    CurrentRoom.Chest.GetItem(itemName, item);
    Console.WriteLine($"The {itemName} does not fit in youre backpack.");


    return false; 
  }

  public bool DropToChest(string itemName)
  {
    
    Item item = backpack.RemoveItem(itemName);

    if (CurrentRoom.Chest.GetItem(itemName, item) == true)
    {
      Console.WriteLine($"The {itemName} is droped in the room");
      return true;
    }

    return false;
  }
}
