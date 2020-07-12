using UnityEngine;

//this script will be responsable for keeping track of all ingame character stats, player, enemy, etc.
//made in such a way that both player and enemy can derive from it, Possible hp bonus items may need to
//be done directly on PlayerStats.cs and then that calls the methods from its base class
public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    //by making it {get, private set} we are allowing scripts to get the value, so for UI display.
    //but we are also saying that we can only set the value from within this script
    // and or anything that derives from this script
    public int currentHealth { get; private set; }

    //here we are calling upon the Stat.cs script where we can create the data for each stat
    //public Stat damage uses the entire script to create the data/value of the stat
    public Stat damage;
    public Stat armour;
    public Stat health; //----i dont think this is being applied anywhere----

    public event System.Action<int, int> OnHealthChanged;

    void Awake()
    {
        //here we can set the health but cannot do that from any other script
        //so for hp pick ups need to be applying from (this).cs or PlayerStats.cs
        //but get its value like stat from another script!
        currentHealth = maxHealth;
    }

    //void Update() //to test to taking damage
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        TakeDamage(10);
    //    }
    //}

    
    public void TakeDamage(int damage)
    {
        //here we are taking the armour stat from the Stat.cs value and doing a small cal.
        //damage is -= to the GetValue(); for armour
        damage -= armour.GetValue();
        //now the issue if we left it like that is if the armour value is larger then the damage value
        //well we will gain health, so we need to clamp the damage value to never go below 0
        //here we say damage = to the clamp (damage, 0, int.MaxValue);
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        //Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        //when the players health reaches 0 character will die
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //here we might have different ways to die depending on the character i.e. player or enemy
    //so within the other scripts we can just override this method
    public virtual void Die()
    {
        //die in some way
        //ment to be overriden
    }
}
