class Inventory
{
  //Fields
  private int maxWeight;
  private Dictionary<string, Item> items;

  //Constructors
  public Inventory(int maxWeight)
  {
    this.maxWeight = maxWeight;
    this.items = new Dictionary<string, Item>();
  }

  //Methods
  public bool Put(string itemName, Item item)
  {
    //TODO implement:
    //Check the weight of the Item and check for enough space in the inventory
    if (item.Weight <= GetFreeWeight())
    {
      items.Add(itemName, item);
      return true;
    }
    //Does the item fit?
    //Put Item in the items Dictionary
    //Return true/false for success/failure

    return false;
  }

  public Item Get(string itemName)
  {
    if (!items.ContainsKey(itemName))
    {
      return null;
    }

    Item item = items[itemName];
    items.Remove(itemName);
    return item;
  }

  public bool CheckIfItemIsInInventory(string itemName)
  {
    foreach (KeyValuePair<string, Item> item in items)
    {
      if (item.Key == itemName)
      {
        return true;
      }
    }

    return false;

  }

  public bool CheckIfItemIsAvailible(string itemName)
  {
    string[] oneTimeItems = {"medkit", "entranceKey"};

    foreach (KeyValuePair<string, Item> item in items)
    {
      if (item.Key == itemName && oneTimeItems.Contains(itemName))
      {
        items.Remove(itemName);
        return true;
      }
      else if (item.Key == itemName)
      {
        return true;
      }
    }

    return false;
  }


  public int GetTotalWeight()
  {
    int total = 0;

    foreach(Item item in items.Values)
    {
      total += item.Weight;
    }

    return total;
  }

  public int GetFreeWeight()
  {
    return maxWeight - GetTotalWeight();
  }

  public List<string> ListItems()
  {
    return new List<string>(items.Keys);
  }

  public string Show()
  {
    string allItems = "";

    foreach (var itemName in ListItems())
    {
      allItems += itemName + " ";
    }

    return "There is nothing found in the Inventory";

  }

}
