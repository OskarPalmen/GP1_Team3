using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (currentHealth <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
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



