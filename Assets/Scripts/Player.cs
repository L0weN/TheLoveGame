using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxMana = 100;
    public int currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;
    private GameObject player;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentMana = maxMana;
        manaBar.SetMana(currentMana);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        player.GetComponent<PlayerMovement>().SetHealth(currentHealth);
    }
    public void Heal(int heal)
    {
        if(currentHealth < 100)
        {
            currentHealth += heal;
            healthBar.SetHealth(currentHealth);
            player.GetComponent<PlayerMovement>().SetHealth(currentHealth);
        }
        else
        {
            currentHealth = 100;
            healthBar.SetHealth(currentHealth);
            player.GetComponent<PlayerMovement>().SetHealth(currentHealth);
        }        
    }
    public void ManaReduction(int mana)
    {
        currentMana -= mana;
        if (currentMana <= 0)
            currentMana = 0;
        manaBar.SetMana(currentMana);
        player.GetComponent<PlayerMovement>().SetMana(currentMana);
    }

    public void ManaRegeneration(int mana)
    {
        if(currentMana < 100)
        {
            currentMana += mana;
            manaBar.SetMana(currentMana);
            player.GetComponent<PlayerMovement>().SetMana(currentMana);
        }
        else
        {
            currentMana = 100;
            manaBar.SetMana(currentMana);
            player.GetComponent<PlayerMovement>().SetMana(currentMana);
        }
    }
}
