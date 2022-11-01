using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamagePlayer(int playerDamage)
    {
        currentHealth = currentHealth - playerDamage;
    }

    public void HealPlayer (int restorePlayerHealth)
    {

        currentHealth = currentHealth + restorePlayerHealth;

        if (currentHealth> maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

    
}
