using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int totalHealth;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DamagePlayer(int playerDamage)
    {
        currentHealth = currentHealth - playerDamage;
    }

    public void HealPlayer(int playerHeal)
    {

        currentHealth = currentHealth + playerHeal;

        if (currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
        }
    }



}



