using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armour;
    public Stat health;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        TakeDamage(10);
    //    }
    //}

    public void TakeDamage(int damage)
    {
        damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        //Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int health)
    {
        health += _health.GetValue();
        health = Mathf.Clamp(health, 0, maxHealth);
        if (currentHealth <= maxHealth)
        {
            currentHealth += health;
        }
    }

    public virtual void Die()
    {
        //die in some way
        //ment to be overriden
    }
}
