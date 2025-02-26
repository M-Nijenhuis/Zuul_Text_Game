class Player
{
  public Room CurrentRoom { get; set; }

  //Fields
  private const int _maxHealth = 100;
  public int health;


  public Player() 
  {
    CurrentRoom = null;
    health = 100;
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
}
