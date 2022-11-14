using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int totalHealth;
    public HealthBar healthBar;
    private GameMaster gm;



    // Start is called before the first frame update
    void Start()
    {

        currentHealth = totalHealth;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        healthBar = GameObject.FindObjectOfType(typeof(HealthBar)) as HealthBar;
        healthBar.SetMaxHealth(totalHealth);
        healthBar.SetHealth(currentHealth);
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
            gm.highscore = FindObjectOfType<HighScore>().progressScore;
            gm.timer = FindObjectOfType<Timer>().currentTime;
            FindObjectOfType<AudioManager>().Play("Dead");
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        healthBar.SetHealth(currentHealth);
    }

    public void HealPlayer(int playerHeal)
    {

        currentHealth = currentHealth + playerHeal;

        if (currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
        }
        healthBar.SetHealth(currentHealth);
    }



}



